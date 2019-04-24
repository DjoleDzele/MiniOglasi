using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace MiniOglasi.Models.NekretninaOglasModels
{
    [Table("TipoviGradnje")]
    public class TipGradnje
    {
        public int Id { get; set; }
        public string Tip { get; set; }

        public ICollection<NekretninaOglas> NekretninaOglasi { get; set; }
    }
}