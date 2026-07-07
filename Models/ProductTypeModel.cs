using System.ComponentModel.DataAnnotations;

namespace VisionNaranja.Models
{
    public class ProductTypeModel
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; } = string.Empty;
    }
}
