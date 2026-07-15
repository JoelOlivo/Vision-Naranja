using System.ComponentModel.DataAnnotations;

namespace VisionNaranja.Models
{
    public class EntrepreneurshipModel
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; } = string.Empty;

        public string? Description { get; set; }
        public string? LogoPath { get; set; }

        //Fk
        public int EntrepreneurId { get; set; }
    }
}
