using System.ComponentModel.DataAnnotations.Schema;

namespace MiniOglasi.Models
{
    [Table("Slike")]
    public class Slika
    {
        public int Id { get; set; }
        public string PathDoSlike { get; set; }

        [ForeignKey("Oglas")]
        public int OglasId { get; set; }

        public Oglas Oglas { get; set; }
    }
}