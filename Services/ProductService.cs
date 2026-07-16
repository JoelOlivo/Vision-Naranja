using VisionNaranja.Data.Repositories;
using VisionNaranja.Services.FileStorage;
using VisionNaranja.Services.Storage;
using VisionNaranja.ViewModels.Product;
using VisionNaranja.ViewModels.ProductMedia;

namespace VisionNaranja.Services
{
    public class ProductService
    {
        private readonly ProductRepository _repository;
        private readonly ProductMediaRepository _productMediaRepository;
        private readonly FileStorageService _fileStorageService;
        private readonly EntrepreneurshipRepository _entrepreneurshipRepository;

        public ProductService(
            ProductRepository repository, 
            ProductMediaRepository productMediaRepository, 
            FileStorageService fileStorageService,
            EntrepreneurshipRepository entrepreneurshipRepository)
        {
            _repository = repository;
            _productMediaRepository = productMediaRepository;
            _fileStorageService = fileStorageService;
            _entrepreneurshipRepository = entrepreneurshipRepository;
        }

        public async Task<ProductIndexViewModel> GetAllForIndexAsync()
        {
            ProductIndexViewModel viewModel = new()
            {
                Products = await _repository.GetAllDetailsAsync(),
                Entrepreneurships = await _entrepreneurshipRepository.GetAllAsync()
            };

            return viewModel;
        }

        public async Task<IEnumerable<ProductViewModel>> GetAllAsync()
        {
            return await _repository.GetAllDetailsAsync();
        }

        public async Task<IEnumerable<GetProductViewModel>> GetAllByEntrepreneurshipAsync(int entrepreneurshipId)
        {
            return await _repository.GetAllByEntrepreneurshipAsync(entrepreneurshipId);
        }

        public async Task<GetProductViewModel?> GetByIdAsync(int id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task<bool> AddAsync(AddProductViewModel product, IEnumerable<IFormFile> files)
        {
            int productId = await _repository.AddAsync(product);

            if (productId <= 0)
                return false;

            List<FileStorageModel> storedFiles = await _fileStorageService.SaveAsync(
                files,
                FileStorageFolder.Products,
                productId
            );

            bool isPrimary = true;

            foreach (var file in storedFiles)
            {
                await _productMediaRepository.AddAsync(new AddProductMediaViewModel
                (
                    file.FileName,
                    file.RelativePath,
                    isPrimary,
                    productId
                ));

                isPrimary = false;
            }

            return true;
        }

        public async Task<bool> UpdateAsync(UpdateProductViewModel model)
        { 
            return await _repository.UpdateAsync(model);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            bool mediasDeleted = await _productMediaRepository.DeleteByProductIdAsync(id);

            if (!mediasDeleted)
                return false;

            bool productDeleted = await _repository.DeleteAsync(id);

            if (!productDeleted)
                return false;

            _fileStorageService.DeleteDirectory(FileStorageFolder.Products, id);

            return true;
        }
    }
}