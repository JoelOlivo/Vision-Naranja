using Dapper;
using VisionNaranja.ViewModels.Product;

namespace VisionNaranja.Data.Repositories
{
    public class ProductRepository
    {
        private readonly DbConnectionFactory _dbConnectionFactory;

        public ProductRepository(DbConnectionFactory dbConnectionFactory)
        {
            _dbConnectionFactory = dbConnectionFactory;
        }

        public async Task<IEnumerable<GetProductViewModel>> GetAllByEntrepreneurshipAsync(int entrepreneurshipId)
        {
            const string sql = @"
                SELECT
                    p.id AS Id,
                    p.name AS Name,
                    p.description AS Description,
                    p.price AS Price
                FROM products p
                WHERE p.entrepreneurship_id = @EntrepreneurshipId;
            ";

            using var connection = _dbConnectionFactory.Create();

            return await connection.QueryAsync<GetProductViewModel>(sql, new { EntrepreneurshipId = entrepreneurshipId });
        }

        public async Task<GetProductViewModel?> GetByIdAsync(int id)
        {
            const string sql = @"
                SELECT
                    p.id AS Id,
                    p.name AS Name,
                    p.description AS Description,
                    p.price AS Price
                FROM products p
                WHERE p.id = @Id;
            ";

            using var connection = _dbConnectionFactory.Create();

            return await connection.QueryFirstOrDefaultAsync<GetProductViewModel>(sql, new { Id = id });
        }

        public async Task<int> AddAsync(AddProductViewModel product)
        {
            const string sql = @"
                INSERT INTO products 
                    (name, description, price, product_type_id, entrepreneurship_id) 
                VALUES 
                    (@Name, @Description, @Price, @ProductTypeId, @EntrepreneurshipId);

                SELECT LAST_INSERT_ID();
            ";

            using var connection = _dbConnectionFactory.Create();

            return await connection.ExecuteScalarAsync<int>(sql, product);
        }

        public async Task<bool> UpdateAsync(UpdateProductViewModel product)
        {
            const string sql = @"
                UPDATE products SET 
	                name = @Name,
	                description = @Description,
                    price = @Price
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
