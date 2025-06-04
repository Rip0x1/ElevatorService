using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace ElevatorServiceApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ElevatorsController : ControllerBase
    {
        private readonly string _connectionString;

        public ElevatorsController(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        [HttpGet]
        public async Task<IActionResult> GetElevators()
        {
            var elevators = new List<object>();
            try
            {
                using (var connection = new MySqlConnection(_connectionString))
                {
                    await connection.OpenAsync();
                    var query = "SELECT * FROM Elevators";
                    using (var command = new MySqlCommand(query, connection))
                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            elevators.Add(new
                            {
                                ElevatorId = reader.GetInt32("ElevatorId"),
                                ClientId = reader.GetInt32("ClientId"),
                                SerialNumber = reader.GetString("SerialNumber"),
                                Model = reader.IsDBNull(reader.GetOrdinal("Model")) ? null : reader.GetString("Model"),
                                InstallationDate = reader.GetDateTime("InstallationDate"),
                                LastServiceDate = reader.IsDBNull(reader.GetOrdinal("LastServiceDate")) ? (DateTime?)null : reader.GetDateTime("LastServiceDate"),
                                Status = reader.GetString("Status")
                            });
                        }
                    }
                }
                return Ok(elevators);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Ошибка: {ex.Message}");
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetElevator(int id)
        {
            try
            {
                using (var connection = new MySqlConnection(_connectionString))
                {
                    await connection.OpenAsync();
                    var query = "SELECT * FROM Elevators WHERE ElevatorId = @Id";
                    using (var command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Id", id);
                        using (var reader = await command.ExecuteReaderAsync())
                        {
                            if (await reader.ReadAsync())
                            {
                                return Ok(new
                                {
                                    ElevatorId = reader.GetInt32("ElevatorId"),
                                    ClientId = reader.GetInt32("ClientId"),
                                    SerialNumber = reader.GetString("SerialNumber"),
                                    Model = reader.IsDBNull(reader.GetOrdinal("Model")) ? null : reader.GetString("Model"),
                                    InstallationDate = reader.GetDateTime("InstallationDate"),
                                    LastServiceDate = reader.IsDBNull(reader.GetOrdinal("LastServiceDate")) ? (DateTime?)null : reader.GetDateTime("LastServiceDate"),
                                    Status = reader.GetString("Status")
                                });
                            }
                            return NotFound();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Ошибка: {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateElevator([FromBody] Elevator elevator)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                using (var connection = new MySqlConnection(_connectionString))
                {
                    await connection.OpenAsync();
                    var query = @"INSERT INTO Elevators (ClientId, SerialNumber, Model, InstallationDate, LastServiceDate, Status)
                                VALUES (@ClientId, @SerialNumber, @Model, @InstallationDate, @LastServiceDate, @Status);
                                SELECT LAST_INSERT_ID();";
                    using (var command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@ClientId", elevator.ClientId);
                        command.Parameters.AddWithValue("@SerialNumber", elevator.SerialNumber);
                        command.Parameters.AddWithValue("@Model", (object)elevator.Model ?? DBNull.Value);
                        command.Parameters.AddWithValue("@InstallationDate", elevator.InstallationDate);
                        command.Parameters.AddWithValue("@LastServiceDate", (object)elevator.LastServiceDate ?? DBNull.Value);
                        command.Parameters.AddWithValue("@Status", elevator.Status ?? "Active");
                        var newId = Convert.ToInt32(await command.ExecuteScalarAsync());
                        elevator.ElevatorId = newId;
                    }
                }
                return CreatedAtAction(nameof(GetElevator), new { id = elevator.ElevatorId }, elevator);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Ошибка: {ex.Message}");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateElevator(int id, [FromBody] Elevator elevator)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                using (var connection = new MySqlConnection(_connectionString))
                {
                    await connection.OpenAsync();
                    var query = @"UPDATE Elevators SET ClientId = @ClientId, SerialNumber = @SerialNumber, Model = @Model,
                                InstallationDate = @InstallationDate, LastServiceDate = @LastServiceDate, Status = @Status
                                WHERE ElevatorId = @Id";
                    using (var command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Id", id);
                        command.Parameters.AddWithValue("@ClientId", elevator.ClientId);
                        command.Parameters.AddWithValue("@SerialNumber", elevator.SerialNumber);
                        command.Parameters.AddWithValue("@Model", (object)elevator.Model ?? DBNull.Value);
                        command.Parameters.AddWithValue("@InstallationDate", elevator.InstallationDate);
                        command.Parameters.AddWithValue("@LastServiceDate", (object)elevator.LastServiceDate ?? DBNull.Value);
                        command.Parameters.AddWithValue("@Status", elevator.Status ?? "Active");
                        int rowsAffected = await command.ExecuteNonQueryAsync();
                        if (rowsAffected > 0)
                            return NoContent();
                        return NotFound();
                    }
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Ошибка: {ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteElevator(int id)
        {
            try
            {
                using (var connection = new MySqlConnection(_connectionString))
                {
                    await connection.OpenAsync();
                    // Проверка и удаление связанных записей в ServiceRequests
                    var checkRequestsQuery = "SELECT COUNT(*) FROM ServiceRequests WHERE ElevatorId = @Id";
                    using (var checkCommand = new MySqlCommand(checkRequestsQuery, connection))
                    {
                        checkCommand.Parameters.AddWithValue("@Id", id);
                        var requestCount = Convert.ToInt32(await checkCommand.ExecuteScalarAsync());
                        if (requestCount > 0)
                        {
                            var deleteRequestsQuery = "DELETE FROM ServiceRequests WHERE ElevatorId = @Id";
                            using (var deleteCommand = new MySqlCommand(deleteRequestsQuery, connection))
                            {
                                deleteCommand.Parameters.AddWithValue("@Id", id);
                                await deleteCommand.ExecuteNonQueryAsync();
                            }
                        }
                    }
                    // Удаление основной записи
                    var deleteQuery = "DELETE FROM Elevators WHERE ElevatorId = @Id";
                    using (var deleteCommand = new MySqlCommand(deleteQuery, connection))
                    {
                        deleteCommand.Parameters.AddWithValue("@Id", id);
                        int rowsAffected = await deleteCommand.ExecuteNonQueryAsync();
                        if (rowsAffected > 0)
                            return NoContent();
                        return NotFound();
                    }
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Ошибка: {ex.Message}");
            }
        }
    }

    public class Elevator
    {
        public int ElevatorId { get; set; }
        public int ClientId { get; set; }
        public string? SerialNumber { get; set; }
        public string? Model { get; set; }
        public DateTime InstallationDate { get; set; }
        public DateTime? LastServiceDate { get; set; }
        public string? Status { get; set; }
    }
}