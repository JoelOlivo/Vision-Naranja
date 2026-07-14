namespace VisionNaranja.ViewModels.ProductMedia
{
    public class GetProductMediaViewModel
    {
        public int Id { get; set; }
        public string FileName { get; set; } = string.Empty;
        public string MediaPath { get; set; } = string.Empty;
        public bool IsPrimary { get; set; }
    }
}