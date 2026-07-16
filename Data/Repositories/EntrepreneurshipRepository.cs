using Dapper;
using VisionNaranja.ViewModels.Entrepreneurship;

namespace VisionNaranja.Data.Repositories
{
    public class EntrepreneurshipRepository
    {
        private readonly DbConnectionFactory _dbConnectionFactory;

        public EntrepreneurshipRepository(DbConnectionFactory dbConnectionFactory)
        {
            _dbConnectionFactory = dbConnectionFactory;
        }

        public async Task<EntrepreneurshipViewModel?> GetByIdAsync(int id)
        {
            const string sql = @"
                SELECT
	                id AS Id,
                    name AS Name,
                    description AS Description,
                    logo_path AS LogoPath
                FROM entrepreneurships
                WHERE id = @Id;
            ";

            using var connection = _dbConnectionFactory.Create();

            return await connection.QueryFirstOrDefaultAsync<EntrepreneurshipViewModel>(sql, new { Id = id });
        }

        public async Task<IEnumerable<EntrepreneurshipViewModel>> GetAllAsync()
        {
            const string sql = @"
                SELECT
	                id AS Id,
                    name AS Name,
                    description AS Description,
                    logo_path AS LogoPath
                FROM entrepreneurships
            ";

            using var connection = _dbConnectionFactory.Create();

            return await connection.QueryAsync<EntrepreneurshipViewModel>(sql);
        }
    }
}
