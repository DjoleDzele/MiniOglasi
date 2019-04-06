using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace MiniOglasi.Models.AutoOglasModels
{
    [Table("Stanja")]
    public class Stanje
    {
        public int Id { get; set; }
        public string StanjePredmetaOglasa { get; set; }

        public ICollection<Oglas> Oglasi { get; set; }
    }
}