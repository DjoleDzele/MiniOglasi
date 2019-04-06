using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace MiniOglasi.Models.AutoOglasModels
{
    [Table("MarkeAuta")]
    public class MarkaAuta
    {
        public int Id { get; set; }
        public string Marka { get; set; }

        public ICollection<AutoOglas> OglasiAuto { get; set; }

        public ICollection<ModelAuta> ModeliAuta { get; set; }
    }
}