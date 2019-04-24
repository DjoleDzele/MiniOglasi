using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace MiniOglasi.Models.NekretninaOglasModels
{
    [Table("RezimOglasNekretnina")]
    public class RezimOglasaNekretnine
    {
        public int Id { get; set; }
        public string Rezim { get; set; }

        public ICollection<NekretninaOglas> NekretninaOglasi { get; set; }
    }
}