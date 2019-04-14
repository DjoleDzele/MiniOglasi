using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MiniOglasi.Models.RacunarOglasModels
{
    public class RacunarOglas : Oglas
    {
        [ForeignKey("MarkaRacunara")]
        public int? MarkaRacunaraId { get; set; }

        public MarkaRacunara MarkaRacunara { get; set; }

        [ForeignKey("TipRacunara")]
        public int? TipRacunaraId { get; set; }

        public TipRacunara TipRacunara { get; set; }

        [ForeignKey("MarkaGrafickeKarte")]
        public int? MarkaGrafickeKarteId { get; set; }

        public GrafickaKartaMarka MarkaGrafickeKarte { get; set; }

        [Range(0, 16000)]
        [Display(Name = "Graficka karta memorija / MB")]
        public int GrafickaMemorija { get; set; }

        [Range(0, 10000)]
        [Display(Name = "HDD Memorija / GB")]
        public int HddMemorija { get; set; }

        [Range(0, 128)]
        [Display(Name = "Ram Memorija / GB")]
        public int RamMemorija { get; set; }

        [Range(0, 100)]
        [Display(Name = "Brzina CPU / GHz")]
        public int ProcesorBrzina { get; set; }

        [Range(1, 16)]
        [Display(Name = "Broj CPU jezgara")]
        public int ProcesorJezgara { get; set; }

        public bool Monitor { get; set; }

        public bool Tastatura { get; set; }

        public bool Mis { get; set; }
    }
}