using System.ComponentModel.DataAnnotations;

namespace VisionNaranja.Models
{
    public class ProductModel
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; } = string.Empty;

        public string? Description { get; set; }

        [Required]
        public int ProductTypeId { get; set; }

        [Required]
        public int EntrepreneurId { get; set; }
    }
}
