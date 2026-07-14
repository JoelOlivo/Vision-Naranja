using Dapper;
using VisionNaranja.ViewModels.ProductMedia;

namespace VisionNaranja.Data.Repositories
{
    public class ProductMediaRepository (DbConnectionFactory dbConnectionFactory)
    {
        private readonly DbConnectionFactory _dbConnectionFactory = dbConnectionFactory;

        public async Task<IEnumerable<GetProductMediaViewModel>> GetByProductIdAsync(int productId)
        {
            const string sql = @"
                SELECT
                    pm.id AS Id,
                    pm.file_name AS FileName,
                    pm.media_path AS MediaPath,
                    pm.is_primary AS IsPrimary
                FROM product_media pm
                WHERE pm.product_id = @ProductId;
            ";

            using var connection = _dbConnectionFactory.Create();

            return await connection.QueryAsync<GetProductMediaViewModel>(sql, new { ProductId = productId });
        }

        public async Task<bool> AddAsync(AddProductMediaViewModel productMedia)
        {
            const string sql = @"
                INSERT INTO product_media
	                (file_name, media_path, is_primary, product_id) 
                VALUES 
	                (@FileName, @MediaPath, @IsPrimary, @ProductId);
            ";

            using var connection = _dbConnectionFactory.Create();

            return await connection.ExecuteAsync(sql, productMedia) > 0;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            const string sql = @"
                DELETE FROM product_media
                WHERE id = @Id;
            ";

            using var connection = _dbConnectionFactory.Create();

            return await connection.ExecuteAsync(sql, new { Id = id }) > 0;
        }

        public async Task<bool> DeleteByProductIdAsync(int productId)
        {
            const string sql = @"
                DELETE FROM product_media
                WHERE product_id = @ProductId;
            ";

            using var connection = _dbConnectionFactory.Create();

            return await connection.ExecuteAsync(sql, new { ProductId = productId }) > 0;
        }
    }
}
