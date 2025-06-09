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
    public class ClientsController : ControllerBase
    {
        private readonly string _connectionString;

        public ClientsController(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        [HttpGet]
        public async Task<IActionResult> GetClients()
        {
            var clients = new List<object>();
            try
            {
                using (var connection = new MySqlConnection(_connectionString))
                {
                    await connection.OpenAsync();
                    var query = "SELECT * FROM Clients";
                    using (var command = new MySqlCommand(query, connection))
                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            clients.Add(new
                            {
                                ClientId = reader.GetInt32("ClientId"),
                                CompanyName = reader.GetString("CompanyName"),
                                ContactPerson = reader.IsDBNull(reader.GetOrdinal("ContactPerson")) ? null : reader.GetString("ContactPerson"),
                                Phone = reader.IsDBNull(reader.GetOrdinal("Phone")) ? null : reader.GetString("Phone"),
                                Email = reader.IsDBNull(reader.GetOrdinal("Email")) ? null : reader.GetString("Email"),
                                Address = reader.IsDBNull(reader.GetOrdinal("Address")) ? null : reader.GetString("Address")
                            });
                        }
                    }
                }
                return Ok(clients);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Ошибка: {ex.Message}");
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetClient(int id)
        {
            try
            {
                using (var connection = new MySqlConnection(_connectionString))
                {
                    await connection.OpenAsync();
                    var query = "SELECT * FROM Clients WHERE ClientId = @Id";
                    using (var command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Id", id);
                        using (var reader = await command.ExecuteReaderAsync())
                        {
                            if (await reader.ReadAsync())
                            {
                                return Ok(new
                                {
                                    ClientId = reader.GetInt32("ClientId"),
                                    CompanyName = reader.GetString("CompanyName"),
                                    ContactPerson = reader.IsDBNull(reader.GetOrdinal("ContactPerson")) ? null : reader.GetString("ContactPerson"),
                                    Phone = reader.IsDBNull(reader.GetOrdinal("Phone")) ? null : reader.GetString("Phone"),
                                    Email = reader.IsDBNull(reader.GetOrdinal("Email")) ? null : reader.GetString("Email"),
                                    Address = reader.IsDBNull(reader.GetOrdinal("Address")) ? null : reader.GetString("Address")
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
        public async Task<IActionResult> CreateClient([FromBody] Client client)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                using (var connection = new MySqlConnection(_connectionString))
                {
                    await connection.OpenAsync();
                    var query = @"INSERT INTO Clients (CompanyName, ContactPerson, Phone, Email, Address)
                                VALUES (@CompanyName, @ContactPerson, @Phone, @Email, @Address);
                                SELECT LAST_INSERT_ID();";
                    using (var command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@CompanyName", client.CompanyName);
                        command.Parameters.AddWithValue("@ContactPerson", (object)client.ContactPerson ?? DBNull.Value);
                        command.Parameters.AddWithValue("@Phone", (object)client.Phone ?? DBNull.Value);
                        command.Parameters.AddWithValue("@Email", (object)client.Email ?? DBNull.Value);
                        command.Parameters.AddWithValue("@Address", (object)client.Address ?? DBNull.Value);
                        var newId = Convert.ToInt32(await command.ExecuteScalarAsync());
                        client.ClientId = newId;
                    }
                }
                return CreatedAtAction(nameof(GetClient), new { id = client.ClientId }, client);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Ошибка: {ex.Message}");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateClient(int id, [FromBody] Client client)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                using (var connection = new MySqlConnection(_connectionString))
                {
                    await connection.OpenAsync();
                    var query = @"UPDATE Clients SET CompanyName = @CompanyName, ContactPerson = @ContactPerson, Phone = @Phone,
                                Email = @Email, Address = @Address WHERE ClientId = @Id";
                    using (var command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Id", id);
                        command.Parameters.AddWithValue("@CompanyName", client.CompanyName);
                        command.Parameters.AddWithValue("@ContactPerson", (object)client.ContactPerson ?? DBNull.Value);
                        command.Parameters.AddWithValue("@Phone", (object)client.Phone ?? DBNull.Value);
                        command.Parameters.AddWithValue("@Email", (object)client.Email ?? DBNull.Value);
                        command.Parameters.AddWithValue("@Address", (object)client.Address ?? DBNull.Value);
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
        public async Task<IActionResult> DeleteClient(int id)
        {
            try
            {
                using (var connection = new MySqlConnection(_connectionString))
                {
                    await connection.OpenAsync();
                    // Проверка и удаление связанных записей в Elevators
                    var checkElevatorsQuery = "SELECT COUNT(*) FROM Elevators WHERE ClientId = @Id";
                    using (var checkCommand = new MySqlCommand(checkElevatorsQuery, connection))
                    {
                        checkCommand.Parameters.AddWithValue("@Id", id);
                        var elevatorCount = Convert.ToInt32(await checkCommand.ExecuteScalarAsync());
                        if (elevatorCount > 0)
                        {
                            var deleteElevatorsQuery = "DELETE FROM Elevators WHERE ClientId = @Id";
                            using (var deleteCommand = new MySqlCommand(deleteElevatorsQuery, connection))
                            {
                                deleteCommand.Parameters.AddWithValue("@Id", id);
                                await deleteCommand.ExecuteNonQueryAsync();
                            }
                        }
                    }
                    // Удаление основной записи
                    var deleteQuery = "DELETE FROM Clients WHERE ClientId = @Id";
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

    public class Client
    {
        public int ClientId { get; set; }
        public string? CompanyName { get; set; }
        public string? ContactPerson { get; set; }
        public string? Phone { get; set; }
        public string? Email { get; set; }
        public string? Address { get; set; }
    }
}