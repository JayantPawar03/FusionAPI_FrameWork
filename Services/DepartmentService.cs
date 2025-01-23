
using FusionAPI_Framework.Interfaces;
using FusionAPI_Framework.Models;
using Microsoft.Data.SqlClient;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace FusionAPI_Framework.Services
{
    public class DepartmentService : IDepartmentService
    {
        private readonly string _connectionString;

        public DepartmentService(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("apicon");
        }

        public async Task<List<Department>> GetAllDepartment()
        {
            var departments = new List<Department>();

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                //string query = "SELECT * FROM Departments";
                using (SqlCommand command = new SqlCommand("ManageDepartments", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@operation", "GET_ALL");
                    using (SqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            departments.Add(new Department
                            {
                                Id = reader.GetInt32(0),
                                Name = reader.IsDBNull(1) ? null : reader.GetString(1)
                            });
                        }
                    }
                }
            }
            return departments;
        }

        public async Task<Department> GetDepartmentById(int id)
        {
            Department department = null;

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                //string query = "SELECT * FROM Departments WHERE Id = @Id";
                using (SqlCommand command = new SqlCommand("ManageDepartments", connection))
                {
                       command.CommandType=CommandType.StoredProcedure;
                       command.Parameters.AddWithValue("@operation", "GET_BY_ID");
                       command.Parameters.AddWithValue("@Id", id);
                    using (SqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            department = new Department
                            {
                                Id = reader.GetInt32(0),
                                Name = reader.IsDBNull(1) ? null : reader.GetString(1)
                            };
                        }
                    }
                }
            }
            return department;
        }

        public async Task AddDepartment(Department department)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                //string query = "INSERT INTO Departments (Name) VALUES (@Name)";
                using (SqlCommand command = new SqlCommand("ManageDepartments", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@Operation", "ADD");
                    command.Parameters.AddWithValue("@Name", department.Name ?? (object)DBNull.Value);

                    await command.ExecuteNonQueryAsync();
                }
            }
        }

        public async Task UpdateDepartmentById(Department department)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                //string query = "UPDATE Departments SET Name = @Name WHERE Id = @Id";
                using (SqlCommand command = new SqlCommand("ManageDepartments", connection))
                {
                    command.CommandType=CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@Name", department.Name ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@Id", department.Id);

                    await command.ExecuteNonQueryAsync();
                }
            }
        }

        public async Task DeleteDepartment(int id)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                //string query = "DELETE FROM Departments WHERE Id = @Id";
                using (SqlCommand command = new SqlCommand("ManageDepartments", connection))
                {
                    command.CommandType= CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@opeartion", "UPDATE");
                    command.Parameters.AddWithValue("@Id",id);
                    await command.ExecuteNonQueryAsync();
                }
            }
        }
    }
}