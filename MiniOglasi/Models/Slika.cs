using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MiniOglasi.Models
{
    [Table("Slike")]
    public class Slika
    {
        public int Id { get; set; }

        public byte[] ImageData { get; set; }

        public string ImageMimeType { get; set; }

        [Required]
        [ForeignKey("Oglas")]
        public int OglasId { get; set; }

        public Oglas Oglas { get; set; }
    }
}