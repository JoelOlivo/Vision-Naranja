namespace VisionNaranja.Models
{
    public class EntrepreneurModel
    {
        public int Id { get; set; }
        public string FullName { get; set; } = string.Empty;
        public string NationalId { get; set; } = string.Empty;
        public string CellPhone { get; set; } = string.Empty;
        public string ProfilePhotoPath { get; set; } = string.Empty;

        //Fk
        public int UserId { get; set; }
    }
}
