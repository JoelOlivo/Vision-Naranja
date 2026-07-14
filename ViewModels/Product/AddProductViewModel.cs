namespace VisionNaranja.ViewModels.Product
{
    public class AddProductViewModel
    {
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public int ProductTypeId { get; set; }
        public int EntrepreneurshipId { get; set; }

        //Files
        public IEnumerable<IFormFile> Files { get; set; } = [];
    }
}