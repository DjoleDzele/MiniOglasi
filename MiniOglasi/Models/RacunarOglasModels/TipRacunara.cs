using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace MiniOglasi.Models.RacunarOglasModels
{
    [Table("TipoviRacunara")]
    public class TipRacunara
    {
        public int Id { get; set; }
        public string Tip { get; set; }

        public ICollection<RacunarOglas> Racunari { get; set; }
    }
}