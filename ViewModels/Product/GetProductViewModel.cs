using VisionNaranja.ViewModels.ProductMedia;

namespace VisionNaranja.ViewModels.Product
{
    public class GetProductViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        public decimal? Price { get; set; }
        public IEnumerable<GetProductMediaViewModel> Media { get; set; } = [];
    }
}