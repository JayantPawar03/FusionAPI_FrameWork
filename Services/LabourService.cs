using FusionAPI_Framework.Interfaces;
using FusionAPI_Framework.Models;
using Microsoft.Data.SqlClient;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FusionAPI_Framework.Services
{
    public class LabourService : ILabourService
    {
        private readonly string _connectionString;

        public LabourService(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("apicon");
        }

        public async Task<List<Labour>> GetAllLabour()
        {
            var labours = new List<Labour>();

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                string query = "SELECT * FROM Labour";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    using (SqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            labours.Add(new Labour
                            {
                                Id = reader.GetInt32(0),
                                Name = reader.IsDBNull(1) ? null : reader.GetString(1),
                                Email = reader.IsDBNull(2) ? null : reader.GetString(2),
                                Phone = reader.IsDBNull(3) ? null : reader.GetString(3),
                                Department = reader.IsDBNull(4) ? null : reader.GetString(4),
                                Isactive = reader.IsDBNull(5) ? null : reader.GetBoolean(5)
                            });
                        }
                    }
                }
            }

            return labours;
        }

        public async Task<Labour> GetLabourId(int id)
        {
            Labour labour = null;

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                string query = "SELECT * FROM Labour WHERE Id = @Id";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", id);

                    using (SqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            labour = new Labour
                            {
                                Id = reader.GetInt32(0),
                                Name = reader.IsDBNull(1) ? null : reader.GetString(1),
                                Email = reader.IsDBNull(2) ? null : reader.GetString(2),
                                Phone = reader.IsDBNull(3) ? null : reader.GetString(3),
                                Department = reader.IsDBNull(4) ? null : reader.GetString(4),
                                Isactive = reader.IsDBNull(5) ? null : reader.GetBoolean(5)
                            };
                        }
                    }
                }
            }

            return labour;
        }

        public async Task AddLabour(Labour labour)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                string query = "INSERT INTO Labour (Name, Email, Phone, Department, Isactive) VALUES (@Name, @Email, @Phone, @Department, @Isactive)";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Name", labour.Name ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@Email", labour.Email ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@Phone", labour.Phone ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@Department", labour.Department ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@Isactive", labour.Isactive ?? (object)DBNull.Value);

                    await command.ExecuteNonQueryAsync();
                }
            }
        }

        public async Task UpdateLabour(Labour labour)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                string query = "UPDATE Labour SET Name = @Name, Email = @Email, Phone = @Phone, Department = @Department, Isactive = @Isactive WHERE Id = @Id";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Name", labour.Name ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@Email", labour.Email ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@Phone", labour.Phone ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@Department", labour.Department ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@Isactive", labour.Isactive ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@Id", labour.Id);

                    await command.ExecuteNonQueryAsync();
                }
            }
        }

        public async Task DeleteLabour(int id)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                string query = "DELETE FROM Labour WHERE Id = @Id";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", id);

                    await command.ExecuteNonQueryAsync();
                }
            }
        }
    }
}
