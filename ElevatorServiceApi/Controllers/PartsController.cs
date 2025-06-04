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
    public class PartsController : ControllerBase
    {
        private readonly string _connectionString;

        public PartsController(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        [HttpGet]
        public async Task<IActionResult> GetParts()
        {
            var parts = new List<object>();
            try
            {
                using (var connection = new MySqlConnection(_connectionString))
                {
                    await connection.OpenAsync();
                    var query = "SELECT * FROM Parts";
                    using (var command = new MySqlCommand(query, connection))
                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            parts.Add(new
                            {
                                PartId = reader.GetInt32("PartId"),
                                PartName = reader.GetString("PartName"),
                                PartNumber = reader.IsDBNull(reader.GetOrdinal("PartNumber")) ? null : reader.GetString("PartNumber"),
                                QuantityInStock = reader.GetInt32("QuantityInStock"),
                                Price = reader.GetDecimal("Price")
                            });
                        }
                    }
                }
                return Ok(parts);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Ошибка: {ex.Message}");
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetPart(int id)
        {
            try
            {
                using (var connection = new MySqlConnection(_connectionString))
                {
                    await connection.OpenAsync();
                    var query = "SELECT * FROM Parts WHERE PartId = @Id";
                    using (var command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Id", id);
                        using (var reader = await command.ExecuteReaderAsync())
                        {
                            if (await reader.ReadAsync())
                            {
                                return Ok(new
                                {
                                    PartId = reader.GetInt32("PartId"),
                                    PartName = reader.GetString("PartName"),
                                    PartNumber = reader.IsDBNull(reader.GetOrdinal("PartNumber")) ? null : reader.GetString("PartNumber"),
                                    QuantityInStock = reader.GetInt32("QuantityInStock"),
                                    Price = reader.GetDecimal("Price")
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
        public async Task<IActionResult> CreatePart([FromBody] Part part)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                using (var connection = new MySqlConnection(_connectionString))
                {
                    await connection.OpenAsync();
                    var query = @"INSERT INTO Parts (PartName, PartNumber, QuantityInStock, Price)
                                VALUES (@PartName, @PartNumber, @QuantityInStock, @Price);
                                SELECT LAST_INSERT_ID();";
                    using (var command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@PartName", part.PartName);
                        command.Parameters.AddWithValue("@PartNumber", (object)part.PartNumber ?? DBNull.Value);
                        command.Parameters.AddWithValue("@QuantityInStock", part.QuantityInStock);
                        command.Parameters.AddWithValue("@Price", part.Price);
                        var newId = Convert.ToInt32(await command.ExecuteScalarAsync());
                        part.PartId = newId;
                    }
                }
                return CreatedAtAction(nameof(GetPart), new { id = part.PartId }, part);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Ошибка: {ex.Message}");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePart(int id, [FromBody] Part part)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                using (var connection = new MySqlConnection(_connectionString))
                {
                    await connection.OpenAsync();
                    var query = @"UPDATE Parts SET PartName = @PartName, PartNumber = @PartNumber,
                                QuantityInStock = @QuantityInStock, Price = @Price WHERE PartId = @Id";
                    using (var command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Id", id);
                        command.Parameters.AddWithValue("@PartName", part.PartName);
                        command.Parameters.AddWithValue("@PartNumber", (object)part.PartNumber ?? DBNull.Value);
                        command.Parameters.AddWithValue("@QuantityInStock", part.QuantityInStock);
                        command.Parameters.AddWithValue("@Price", part.Price);
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
        public async Task<IActionResult> DeletePart(int id)
        {
            try
            {
                using (var connection = new MySqlConnection(_connectionString))
                {
                    await connection.OpenAsync();
                    var checkLogsQuery = "SELECT COUNT(*) FROM ServiceLogs WHERE PartsUsed LIKE @IdPattern";
                    using (var checkCommand = new MySqlCommand(checkLogsQuery, connection))
                    {
                        checkCommand.Parameters.AddWithValue("@IdPattern", $"%{id}%");
                        var logCount = Convert.ToInt32(await checkCommand.ExecuteScalarAsync());
                        if (logCount > 0)
                        {
                            var updateLogsQuery = "UPDATE ServiceLogs SET PartsUsed = NULL WHERE PartsUsed LIKE @IdPattern";
                            using (var updateCommand = new MySqlCommand(updateLogsQuery, connection))
                            {
                                updateCommand.Parameters.AddWithValue("@IdPattern", $"%{id}%");
                                await updateCommand.ExecuteNonQueryAsync();
                            }
                        }
                    }
                    var deleteQuery = "DELETE FROM Parts WHERE PartId = @Id";
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

    public class Part
    {
        public int PartId { get; set; }
        public string? PartName { get; set; }
        public string? PartNumber { get; set; }
        public int QuantityInStock { get; set; }
        public decimal Price { get; set; }
    }
}