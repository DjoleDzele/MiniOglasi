using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MiniOglasi.Models.AutoOglasModels
{
    [Table("ModeliAuta")]
    public class ModelAuta
    {
        public int Id { get; set; }
        public string Model { get; set; }

        [Required]
        [ForeignKey("Marka")]
        public int MarkaAutaId { get; set; }

        public MarkaAuta Marka { get; set; }
    }
}