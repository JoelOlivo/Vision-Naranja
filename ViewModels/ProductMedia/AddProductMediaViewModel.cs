namespace VisionNaranja.ViewModels.ProductMedia
{
    public record AddProductMediaViewModel
    (        
        string FileName, 
        string MediaPath, 
        bool IsPrimary, 
        int ProductId
    );
}