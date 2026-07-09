using System.ComponentModel.DataAnnotations;

namespace VisionNaranja.Models
{
    public class ProductMediaModel
    {
        public int Id { get; set; }

        [Required]
        public string FileName { get; set; } = string.Empty;

        [Required]
        public string MediaPath { get; set; } = string.Empty;

        public bool IsPrimary { get; set; }

        //Fk
        [Required]
        public int ProductId { get; set; }
    }
}
