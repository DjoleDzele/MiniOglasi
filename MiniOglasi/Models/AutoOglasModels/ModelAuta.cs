using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MiniOglasi.Models.AutoOglasModels
{
    [Table("ModeliAuta")]
    public class ModelAuta
    {
        public int Id { get; set; }

        [Required]
        public string AutoModel { get; set; }

        [ForeignKey("MarkaAuta")]
        public int MarkaAutaId { get; set; }

        public MarkaAuta MarkaAuta { get; set; }
    }
}