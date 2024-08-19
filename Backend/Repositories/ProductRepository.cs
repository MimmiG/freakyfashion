using Dapper;
using Microsoft.Data.SqlClient;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using FreakyFashionAPI.Models;

namespace FreakyFashionAPI.Repositories
{
    public class ProductRepository
    {
        private readonly string _connectionString;

        public ProductRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<IEnumerable<Product>> GetProductsAsync()
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                var sqlQuery = "SELECT * FROM Products";
                return await db.QueryAsync<Product>(sqlQuery);
            }
        }

        public async Task<Product> GetProductByIdAsync(int id)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                var sqlQuery = "SELECT * FROM Products WHERE Id = @Id";
                return await db.QueryFirstOrDefaultAsync<Product>(sqlQuery, new { Id = id });
            }
        }

        public async Task<int> AddProductAsync(Product product)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                var sqlQuery = "INSERT INTO Products (Name, Description, Price, ImageUrl) VALUES (@Name, @Description, @Price, @ImageUrl)";
                return await db.ExecuteAsync(sqlQuery, product);
            }
        }

        public async Task<int> UpdateProductAsync(Product product)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                var sqlQuery = "UPDATE Products SET Name = @Name, Description = @Description, Price = @Price, ImageUrl = @ImageUrl WHERE Id = @Id";
                return await db.ExecuteAsync(sqlQuery, product);
            }
        }

        public async Task<int> DeleteProductAsync(int id)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                var sqlQuery = "DELETE FROM Products WHERE Id = @Id";
                return await db.ExecuteAsync(sqlQuery, new { Id = id });
            }
        }
    }
}