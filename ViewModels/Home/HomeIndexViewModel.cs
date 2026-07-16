using VisionNaranja.ViewModels.Product;

namespace VisionNaranja.ViewModels.Home
{
    public class HomeIndexViewModel
    {
        public IEnumerable<ProductViewModel> Products { get; set; } = [];
    }
}