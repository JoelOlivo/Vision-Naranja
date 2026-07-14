namespace VisionNaranja.ViewModels.Product
{
    public class UpdateProductViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        public double Price { get; set; }
    }
}