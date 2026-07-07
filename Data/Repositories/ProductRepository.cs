using Dapper;
using VisionNaranja.Models;
using VisionNaranja.ViewModels;

namespace VisionNaranja.Data.Repositories
{
    public class ProductRepository
    {
        private readonly DbConnectionFactory _dbConnectionFactory;

        public ProductRepository(DbConnectionFactory dbConnectionFactory)
        {
            _dbConnectionFactory = dbConnectionFactory;
        }

        public async Task<IEnumerable<ProductViewModel>> GetAllAsync()
        {
            const string sql = @"
                SELECT
                    p.id AS Id,
                    p.name AS Name,
                    p.description AS Description,
                    p.price AS Price,
                    p.product_type_id AS ProductTypeId,
                    p.entrepreneurship_id AS EntrepreneurshipId
                FROM products p;
            ";

            using var connection = _dbConnectionFactory.Create();

            return await connection.QueryAsync<ProductViewModel>(sql);
        }

        public async Task<bool> AddAsync(ProductModel product)
        {
            const string sql = @"
                INSERT INTO products 
                    (name, description, price, product_type_id, entrepreneurship_id) 
                VALUES 
                    (@Name, @Description, @Price, @ProductTypeId, @EntrepreneurshipId);
            ";

            using var connection = _dbConnectionFactory.Create();

            return await connection.ExecuteAsync(sql, product) > 0;
        }

        public async Task<bool> UpdateAsync(ProductModel product)
        {
            const string sql = @"
                UPDATE products SET 
	                name = @Name,
	                description = @Description,
                    price = @Price,
                WHERE id = @Id;
            ";

            using var connection = _dbConnectionFactory.Create();

            return await connection.ExecuteAsync(sql, product) > 0;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            const string sql = @"
                DELETE FROM products WHERE id = @Id;
            ";

            using var connection = _dbConnectionFactory.Create();

            return await connection.ExecuteAsync(sql, new { Id = id }) > 0;
        }

        public async Task<bool> ExistsAsync(int id)
        {
            const string sql = @"
                SELECT EXISTS(
                    SELECT 1 FROM products WHERE id = @Id
                );
            ";

            using var connection = _dbConnectionFactory.Create();

            return await connection.ExecuteScalarAsync<bool>(sql, new { Id = id });
        }
    }
}
