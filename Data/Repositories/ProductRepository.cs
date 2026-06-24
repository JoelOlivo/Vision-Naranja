using Dapper;
using VisionNaranja.Models;

namespace VisionNaranja.Data.Repositories
{
    public class ProductRepository
    {
        private readonly DbConnectionFactory _dbConnectionFactory;

        public ProductRepository(DbConnectionFactory dbConnectionFactory)
        {
            _dbConnectionFactory = dbConnectionFactory;
        }

        public async Task<IEnumerable<ProductModel>> GetAllAsync()
        {
            const string sql = @"
                SELECT
                    id AS Id,
                    name AS Name,
                    description AS Description,
                    product_type_id AS ProductTypeId,
                    entrepreneur_id AS EntrepreneurId
                FROM products
            ";

            using var connection = _dbConnectionFactory.Create();

            return await connection.QueryAsync<ProductModel>(sql);
        }
    }
}
