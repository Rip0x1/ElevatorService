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
    public class ServiceRequestsController : ControllerBase
    {
        private readonly string _connectionString;

        public ServiceRequestsController(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        [HttpGet]
        public async Task<IActionResult> GetServiceRequests()
        {
            var requests = new List<object>();
            try
            {
                using (var connection = new MySqlConnection(_connectionString))
                {
                    await connection.OpenAsync();
                    var query = "SELECT * FROM ServiceRequests";
                    using (var command = new MySqlCommand(query, connection))
                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            requests.Add(new
                            {
                                RequestId = reader.GetInt32("RequestId"),
                                ElevatorId = reader.GetInt32("ElevatorId"),
                                ClientId = reader.GetInt32("ClientId"),
                                EmployeeId = reader.GetInt32("EmployeeId"),
                                RequestDate = reader.GetDateTime("RequestDate"),
                                Description = reader.IsDBNull(reader.GetOrdinal("Description")) ? null : reader.GetString("Description"),
                                Status = reader.GetString("Status"),
                                Priority = reader.GetString("Priority")
                            });
                        }
                    }
                }
                return Ok(requests);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Ошибка: {ex.Message}");
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetServiceRequest(int id)
        {
            try
            {
                using (var connection = new MySqlConnection(_connectionString))
                {
                    await connection.OpenAsync();
                    var query = "SELECT * FROM ServiceRequests WHERE RequestId = @Id";
                    using (var command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Id", id);
                        using (var reader = await command.ExecuteReaderAsync())
                        {
                            if (await reader.ReadAsync())
                            {
                                return Ok(new
                                {
                                    RequestId = reader.GetInt32("RequestId"),
                                    ElevatorId = reader.GetInt32("ElevatorId"),
                                    ClientId = reader.GetInt32("ClientId"),
                                    EmployeeId = reader.GetInt32("EmployeeId"),
                                    RequestDate = reader.GetDateTime("RequestDate"),
                                    Description = reader.IsDBNull(reader.GetOrdinal("Description")) ? null : reader.GetString("Description"),
                                    Status = reader.GetString("Status"),
                                    Priority = reader.GetString("Priority")
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
        public async Task<IActionResult> CreateServiceRequest([FromBody] ServiceRequest request)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                using (var connection = new MySqlConnection(_connectionString))
                {
                    await connection.OpenAsync();
                    var query = @"INSERT INTO ServiceRequests (ElevatorId, ClientId, EmployeeId, RequestDate, Description, Status, Priority)
                                VALUES (@ElevatorId, @ClientId, @EmployeeId, @RequestDate, @Description, @Status, @Priority);
                                SELECT LAST_INSERT_ID();";
                    using (var command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@ElevatorId", request.ElevatorId);
                        command.Parameters.AddWithValue("@ClientId", request.ClientId);
                        command.Parameters.AddWithValue("@EmployeeId", request.EmployeeId);
                        command.Parameters.AddWithValue("@RequestDate", request.RequestDate);
                        command.Parameters.AddWithValue("@Description", (object)request.Description ?? DBNull.Value);
                        command.Parameters.AddWithValue("@Status", request.Status ?? "Open");
                        command.Parameters.AddWithValue("@Priority", request.Priority ?? "Medium");
                        var newId = Convert.ToInt32(await command.ExecuteScalarAsync());
                        request.RequestId = newId;
                    }
                }
                return CreatedAtAction(nameof(GetServiceRequest), new { id = request.RequestId }, request);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Ошибка: {ex.Message}");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateServiceRequest(int id, [FromBody] ServiceRequest request)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                using (var connection = new MySqlConnection(_connectionString))
                {
                    await connection.OpenAsync();
                    var query = @"UPDATE ServiceRequests SET ElevatorId = @ElevatorId, ClientId = @ClientId, EmployeeId = @EmployeeId,
                                RequestDate = @RequestDate, Description = @Description, Status = @Status, Priority = @Priority
                                WHERE RequestId = @Id";
                    using (var command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Id", id);
                        command.Parameters.AddWithValue("@ElevatorId", request.ElevatorId);
                        command.Parameters.AddWithValue("@ClientId", request.ClientId);
                        command.Parameters.AddWithValue("@EmployeeId", request.EmployeeId);
                        command.Parameters.AddWithValue("@RequestDate", request.RequestDate);
                        command.Parameters.AddWithValue("@Description", (object)request.Description ?? DBNull.Value);
                        command.Parameters.AddWithValue("@Status", request.Status ?? "Open");
                        command.Parameters.AddWithValue("@Priority", request.Priority ?? "Medium");
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
        public async Task<IActionResult> DeleteServiceRequest(int id)
        {
            try
            {
                using (var connection = new MySqlConnection(_connectionString))
                {
                    await connection.OpenAsync();
                    var checkLogsQuery = "SELECT COUNT(*) FROM ServiceLogs WHERE RequestId = @Id";
                    using (var checkCommand = new MySqlCommand(checkLogsQuery, connection))
                    {
                        checkCommand.Parameters.AddWithValue("@Id", id);
                        var logCount = Convert.ToInt32(await checkCommand.ExecuteScalarAsync());
                        if (logCount > 0)
                        {
                            var deleteLogsQuery = "DELETE FROM ServiceLogs WHERE RequestId = @Id";
                            using (var deleteCommand = new MySqlCommand(deleteLogsQuery, connection))
                            {
                                deleteCommand.Parameters.AddWithValue("@Id", id);
                                await deleteCommand.ExecuteNonQueryAsync();
                            }
                        }
                    }
                    var deleteQuery = "DELETE FROM ServiceRequests WHERE RequestId = @Id";
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

    public class ServiceRequest
    {
        public int RequestId { get; set; }
        public int ElevatorId { get; set; }
        public int ClientId { get; set; }
        public int EmployeeId { get; set; }
        public DateTime RequestDate { get; set; }
        public string? Description { get; set; }
        public string? Status { get; set; }
        public string? Priority { get; set; }
    }
}