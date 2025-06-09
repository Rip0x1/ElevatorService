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
    public class EmployeesController : ControllerBase
    {
        private readonly string _connectionString;

        public EmployeesController(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        [HttpGet]
        public async Task<IActionResult> GetEmployees()
        {
            var employees = new List<object>();
            try
            {
                using (var connection = new MySqlConnection(_connectionString))
                {
                    await connection.OpenAsync();
                    var query = "SELECT * FROM Employees";
                    using (var command = new MySqlCommand(query, connection))
                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            employees.Add(new
                            {
                                EmployeeId = reader.GetInt32("EmployeeId"),
                                FirstName = reader.GetString("FirstName"),
                                LastName = reader.GetString("LastName"),
                                Position = reader.IsDBNull(reader.GetOrdinal("Position")) ? null : reader.GetString("Position"),
                                Phone = reader.IsDBNull(reader.GetOrdinal("Phone")) ? null : reader.GetString("Phone"),
                                Email = reader.IsDBNull(reader.GetOrdinal("Email")) ? null : reader.GetString("Email")
                            });
                        }
                    }
                }
                return Ok(employees);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Ошибка: {ex.Message}");
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetEmployee(int id)
        {
            try
            {
                using (var connection = new MySqlConnection(_connectionString))
                {
                    await connection.OpenAsync();
                    var query = "SELECT * FROM Employees WHERE EmployeeId = @Id";
                    using (var command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Id", id);
                        using (var reader = await command.ExecuteReaderAsync())
                        {
                            if (await reader.ReadAsync())
                            {
                                return Ok(new
                                {
                                    EmployeeId = reader.GetInt32("EmployeeId"),
                                    FirstName = reader.GetString("FirstName"),
                                    LastName = reader.GetString("LastName"),
                                    Position = reader.IsDBNull(reader.GetOrdinal("Position")) ? null : reader.GetString("Position"),
                                    Phone = reader.IsDBNull(reader.GetOrdinal("Phone")) ? null : reader.GetString("Phone"),
                                    Email = reader.IsDBNull(reader.GetOrdinal("Email")) ? null : reader.GetString("Email")
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
        public async Task<IActionResult> CreateEmployee([FromBody] Employee employee)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                using (var connection = new MySqlConnection(_connectionString))
                {
                    await connection.OpenAsync();
                    var query = @"INSERT INTO Employees (FirstName, LastName, Position, Phone, Email)
                                VALUES (@FirstName, @LastName, @Position, @Phone, @Email);
                                SELECT LAST_INSERT_ID();";
                    using (var command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@FirstName", employee.FirstName);
                        command.Parameters.AddWithValue("@LastName", employee.LastName);
                        command.Parameters.AddWithValue("@Position", (object)employee.Position ?? DBNull.Value);
                        command.Parameters.AddWithValue("@Phone", (object)employee.Phone ?? DBNull.Value);
                        command.Parameters.AddWithValue("@Email", (object)employee.Email ?? DBNull.Value);
                        var newId = Convert.ToInt32(await command.ExecuteScalarAsync());
                        employee.EmployeeId = newId;
                    }
                }
                return CreatedAtAction(nameof(GetEmployee), new { id = employee.EmployeeId }, employee);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Ошибка: {ex.Message}");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateEmployee(int id, [FromBody] Employee employee)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                using (var connection = new MySqlConnection(_connectionString))
                {
                    await connection.OpenAsync();
                    var query = @"UPDATE Employees SET FirstName = @FirstName, LastName = @LastName, Position = @Position,
                                Phone = @Phone, Email = @Email WHERE EmployeeId = @Id";
                    using (var command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Id", id);
                        command.Parameters.AddWithValue("@FirstName", employee.FirstName);
                        command.Parameters.AddWithValue("@LastName", employee.LastName);
                        command.Parameters.AddWithValue("@Position", (object)employee.Position ?? DBNull.Value);
                        command.Parameters.AddWithValue("@Phone", (object)employee.Phone ?? DBNull.Value);
                        command.Parameters.AddWithValue("@Email", (object)employee.Email ?? DBNull.Value);
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
        public async Task<IActionResult> DeleteEmployee(int id)
        {
            try
            {
                using (var connection = new MySqlConnection(_connectionString))
                {
                    await connection.OpenAsync();
                    // Проверка и удаление связанных записей в ServiceRequests и ServiceLogs
                    var checkRequestsQuery = "SELECT COUNT(*) FROM ServiceRequests WHERE EmployeeId = @Id";
                    using (var checkCommand = new MySqlCommand(checkRequestsQuery, connection))
                    {
                        checkCommand.Parameters.AddWithValue("@Id", id);
                        var requestCount = Convert.ToInt32(await checkCommand.ExecuteScalarAsync());
                        if (requestCount > 0)
                        {
                            var deleteRequestsQuery = "DELETE FROM ServiceRequests WHERE EmployeeId = @Id";
                            using (var deleteCommand = new MySqlCommand(deleteRequestsQuery, connection))
                            {
                                deleteCommand.Parameters.AddWithValue("@Id", id);
                                await deleteCommand.ExecuteNonQueryAsync();
                            }
                        }
                    }
                    var checkLogsQuery = "SELECT COUNT(*) FROM ServiceLogs WHERE EmployeeId = @Id";
                    using (var checkCommand = new MySqlCommand(checkLogsQuery, connection))
                    {
                        checkCommand.Parameters.AddWithValue("@Id", id);
                        var logCount = Convert.ToInt32(await checkCommand.ExecuteScalarAsync());
                        if (logCount > 0)
                        {
                            var deleteLogsQuery = "DELETE FROM ServiceLogs WHERE EmployeeId = @Id";
                            using (var deleteCommand = new MySqlCommand(deleteLogsQuery, connection))
                            {
                                deleteCommand.Parameters.AddWithValue("@Id", id);
                                await deleteCommand.ExecuteNonQueryAsync();
                            }
                        }
                    }
                    // Удаление основной записи
                    var deleteQuery = "DELETE FROM Employees WHERE EmployeeId = @Id";
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

    public class Employee
    {
        public int EmployeeId { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Position { get; set; }
        public string? Phone { get; set; }
        public string? Email { get; set; }
    }
}