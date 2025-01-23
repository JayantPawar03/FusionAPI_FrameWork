using FusionAPI_Framework.Interfaces;
using FusionAPI_Framework.Models;
using Microsoft.Data.SqlClient;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FusionAPI_Framework.Services
{
    public class ProductService : IProduct
    {
        private readonly string _connectionString;

        public ProductService(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("apicon");
        }

        public async Task<List<Products>> GetAllProducts()
        {
            var products = new List<Products>();

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                string query = "SELECT * FROM Products";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    using (SqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            products.Add(new Products
                            {
                                id = reader.GetInt32(0),
                                Name = reader.IsDBNull(1) ? null : reader.GetString(1),
                                Description = reader.IsDBNull(2) ? null : reader.GetString(2),
                                Price = reader.IsDBNull(3) ? null : reader.GetDecimal(3),
                                Category = reader.IsDBNull(4) ? null : reader.GetString(4),
                                Stock = reader.IsDBNull(5) ? null : reader.GetInt32(5),
                                FKid = reader.GetInt32(6),  // Foreign Key for Department
                                FKlabourid = reader.IsDBNull(7) ? null : reader.GetInt32(7)  // Foreign Key for Labour
                            });
                        }
                    }
                }
            }

            return products;
        }

        public async Task<Products> GetProductById(int id)
        {
            Products product = null;

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                string query = "SELECT * FROM Products WHERE id = @Id";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", id);

                    using (SqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            product = new Products
                            {
                                id = reader.GetInt32(0),
                                Name = reader.IsDBNull(1) ? null : reader.GetString(1),
                                Description = reader.IsDBNull(2) ? null : reader.GetString(2),
                                Price = reader.IsDBNull(3) ? null : reader.GetDecimal(3),
                                Category = reader.IsDBNull(4) ? null : reader.GetString(4),
                                Stock = reader.IsDBNull(5) ? null : reader.GetInt32(5),
                                FKid = reader.GetInt32(6),  // Foreign Key for Department
                                FKlabourid = reader.IsDBNull(7) ? null : reader.GetInt32(7)  // Foreign Key for Labour
                            };
                        }
                    }
                }
            }

            return product;
        }

        public async Task AddProduct(Products product)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                string query = "INSERT INTO Products (Name, Description, Price, Category, Stock, FKid, FKlabourid) " +
                               "VALUES (@Name, @Description, @Price, @Category, @Stock, @FKid, @FKlabourid)";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Name", product.Name ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@Description", product.Description ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@Price", product.Price ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@Category", product.Category ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@Stock", product.Stock ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@FKid", product.FKid);  // Department Foreign Key
                    command.Parameters.AddWithValue("@FKlabourid", product.FKlabourid ?? (object)DBNull.Value);  // Labour Foreign Key

                    await command.ExecuteNonQueryAsync();
                }
            }
        }

        public async Task UpdateProduct(Products product)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                string query = "UPDATE Products SET Name = @Name, Description = @Description, Price = @Price, " +
                               "Category = @Category, Stock = @Stock, FKid = @FKid, FKlabourid = @FKlabourid WHERE id = @Id";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Name", product.Name ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@Description", product.Description ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@Price", product.Price ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@Category", product.Category ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@Stock", product.Stock ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@FKid", product.FKid);  // Department Foreign Key
                    command.Parameters.AddWithValue("@FKlabourid", product.FKlabourid ?? (object)DBNull.Value);  // Labour Foreign Key
                    command.Parameters.AddWithValue("@Id", product.id);

                    await command.ExecuteNonQueryAsync();
                }
            }
        }

        public async Task DeleteProduct(int id)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                string query = "DELETE FROM Products WHERE id = @Id";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", id);

                    await command.ExecuteNonQueryAsync();
                }
            }
        }
    }
}
