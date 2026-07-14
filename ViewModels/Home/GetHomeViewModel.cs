using VisionNaranja.ViewModels.Product;

namespace VisionNaranja.ViewModels.Home
{
    public class GetHomeViewModel
    {
        public IEnumerable<ProductViewModel> Products { get; set; } = [];
    }
}