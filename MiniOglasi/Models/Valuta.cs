using System.ComponentModel.DataAnnotations.Schema;

namespace MiniOglasi.Models
{
    [Table("Valute")]
    public class Valuta
    {
        public int Id { get; set; }
        public string ImeValute { get; set; }
    }
}