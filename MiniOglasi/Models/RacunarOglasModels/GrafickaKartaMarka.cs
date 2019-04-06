using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace MiniOglasi.Models.RacunarOglasModels
{
    [Table("GrafickeKarteModeli")]
    public class GrafickaKartaMarka
    {
        public int Id { get; set; }
        public string MarkaGraficke { get; set; }

        public ICollection<RacunarOglas> Racunari { get; set; }
    }
}