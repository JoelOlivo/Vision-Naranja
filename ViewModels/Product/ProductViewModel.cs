using VisionNaranja.ViewModels.ProductMedia;

namespace VisionNaranja.ViewModels.Product
{
    public class ProductViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        public double? Price { get; set; }

        //ProductType
        public int ProductTypeId { get; set; }
        public string ProductTypeName { get; set; } = string.Empty;

        //Entrepreneurship
        public int EntrepreneurshipId { get; set; }
        public string EntrepreneurshipName { get; set; } = string.Empty;
        public string? EntrepreneurshipLogoPath { get; set; }

        //Entrepreneur
        public int EntrepreneurId { get; set; }
        public string EntrepreneurName { get; set; } = string.Empty;
        public string EntrepreneurCellPhoneNumber { get; set; } = string.Empty;
        public string? EntrepreneurProfilePhotoPath { get; set; }

        //Files
        public IEnumerable<GetProductMediaViewModel> Media { get; set; } = [];
    }
}