namespace VisionNaranja.ViewModels
{
    public class ProductMediaViewModel
    {
        public int Id { get; set; }
        public string FileName { get; set; } = string.Empty;
        public string MediaPath { get; set; } = string.Empty;
        public bool IsPrimary { get; set; }
        public int ProductId { get; set; }
    }
}