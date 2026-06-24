namespace VisionNaranja.ViewModels
{
    public class ProductViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        public int ProductTypeId { get; set; }
        public string ProductTypeCode { get; set; } = string.Empty;
        public int EntrepreneurId { get; set; }
        public string EntrepreneurFullName { get; set; } = string.Empty;
    }
}
