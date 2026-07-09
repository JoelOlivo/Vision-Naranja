using VisionNaranja.Services.FileStorage;

namespace VisionNaranja.Services.Storage
{
    public class FileStorageService (IConfiguration configuration)
    {
        private readonly IConfiguration _configuration = configuration;

        public async Task<List<FileStorageModel>> SaveAsync(
            IEnumerable<IFormFile> files,
            FileStorageFolder folder,
            int entityId)
        {
            string root = _configuration["Storage:RootPath"]!;

            string destination = Path.Combine(
                root,
                folder.ToString(),
                entityId.ToString());

            Directory.CreateDirectory(destination);

            List<FileStorageModel> storedFiles = [];

            foreach (var file in files)
            {
                if (file.Length == 0)
                    continue;

                string extension = Path.GetExtension(file.FileName);

                string fileName = $"{Guid.NewGuid()}{extension}";

                string fullPath = Path.Combine(destination, fileName);

                using FileStream stream = new(fullPath, FileMode.Create);

                await file.CopyToAsync(stream);

                storedFiles.Add(new FileStorageModel
                {
                    FileName = fileName,
                    RelativePath = $"/{folder}/{entityId}/{fileName}"
                });
            }

            return storedFiles;
        }

        public void Delete(string relativePath)
        {
            string root = _configuration["Storage:RootPath"]!;

            string fullPath = Path.Combine(
                root,
                relativePath.Replace('/', Path.DirectorySeparatorChar));

            if (File.Exists(fullPath))
            {
                File.Delete(fullPath);
            }
        }

        public void DeleteDirectory(FileStorageFolder folder, int entityId)
        {
            string root = _configuration["Storage:RootPath"]!;

            string directory = Path.Combine(
                root,
                folder.ToString(),
                entityId.ToString());

            if (Directory.Exists(directory))
            {
                Directory.Delete(directory, true);
            }
        }
    }
}
