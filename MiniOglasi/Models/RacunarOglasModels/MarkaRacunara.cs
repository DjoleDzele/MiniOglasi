using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace MiniOglasi.Models.RacunarOglasModels
{
    [Table("MarkeRacunara")]
    public class MarkaRacunara
    {
        public int Id { get; set; }
        public string Marka { get; set; }

        public ICollection<RacunarOglas> Racunari { get; set; }
    }
}