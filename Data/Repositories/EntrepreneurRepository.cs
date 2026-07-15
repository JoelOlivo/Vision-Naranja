using Dapper;
using VisionNaranja.ViewModels.Entrepreneur;

namespace VisionNaranja.Data.Repositories
{
    public class EntrepreneurRepository
    {
        private readonly DbConnectionFactory _dbConnectionFactory;

        public EntrepreneurRepository(DbConnectionFactory dbConnectionFactory)
        {
            _dbConnectionFactory = dbConnectionFactory;
        }

        public async Task<EntrepreneurViewModel?> GetByIdAsync(int id)
        {
            const string sql = @"
                SELECT
	                id AS Id,
	                full_name AS FullName,
	                national_id AS NationalId,
	                cell_phone_number AS CellPhoneNumber,
	                profile_photo_path AS ProfilePhotoPath
                FROM entrepreneurs
                WHERE id = @Id;
            ";

            using var connection = _dbConnectionFactory.Create();

            return await connection.QueryFirstOrDefaultAsync<EntrepreneurViewModel>(sql, new { Id = id });
        }
    }
}
