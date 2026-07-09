namespace VisionNaranja.ViewModels
{
    public class ProductViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        public double? Price { get; set; }
        public int ProductTypeId { get; set; }
        public int EntrepreneurshipId { get; set; }
        public IEnumerable<IFormFile> Files { get; set; } = [];
    }
}