using VisionNaranja.ViewModels.Entrepreneurship;

namespace VisionNaranja.ViewModels.Product
{
    public class ProductIndexViewModel
    {
        public IEnumerable<ProductViewModel> Products { get; set; } = [];
        public IEnumerable<EntrepreneurshipViewModel> Entrepreneurships { get; set; } = [];
    }
}