using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MiniOglasi.Models.NekretninaOglasModels
{
    public class NekretninaOglas : Oglas
    {
        [Required]
        [ForeignKey("TipGradnje")]
        [Display(Name = "Tip gradnje")]
        public int TipGradnjeId { get; set; }

        public TipGradnje TipGradnje { get; set; }

        [Required]
        [Display(Name = "Vrsta usluge")]
        [ForeignKey("RezimOglasaNekretnine")]
        public int RezimOglasaNekretnineId { get; set; }

        public RezimOglasaNekretnine RezimOglasaNekretnine { get; set; }

        [Required]
        [ForeignKey("TipNekretnine")]
        [Display(Name = "Tip nekretnine")]
        public int TipNekretnineId { get; set; }

        public TipNekretnine TipNekretnine { get; set; }

        [Required]
        [Display(Name = "Lokacija")]
        [ForeignKey("Grad")]
        public int GradId { get; set; }

        public Grad Grad { get; set; }

        [Range(5, 10000)]
        public int Kvadratura { get; set; }

        [Range(1, 1000)]
        [Display(Name = "Broj Soba")]
        public int BrojSoba { get; set; }

        public bool Internet { get; set; }

        public bool Grejanje { get; set; }

        public bool Kablovska { get; set; }

        public bool Terasa { get; set; }
    }
}