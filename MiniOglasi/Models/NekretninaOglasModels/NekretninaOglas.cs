using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MiniOglasi.Models.NekretninaOglasModels
{
    public class NekretninaOglas : Oglas
    {
        [Required]
        [ForeignKey("TipNekretnine")]
        public int TipNekretnineId { get; set; }

        public TipNekretnine TipNekretnine { get; set; }

        [Required]
        [ForeignKey("Grad")]
        public int GradId { get; set; }

        public Grad Grad { get; set; }

        public bool Internet { get; set; }

        public bool Grejanje { get; set; }

        public bool Kablovska { get; set; }

        public bool Terasa { get; set; }
    }
}