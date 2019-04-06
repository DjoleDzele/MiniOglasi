using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace MiniOglasi.Models.NekretninaOglasModels
{
    [Table("TipoviNekretnina")]
    public class TipNekretnine
    {
        public int Id { get; set; }
        public string Tip { get; set; }

        public ICollection<NekretninaOglas> NekretninaOglasi { get; set; }
    }
}