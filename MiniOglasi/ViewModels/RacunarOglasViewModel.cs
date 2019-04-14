using MiniOglasi.Models;
using MiniOglasi.Models.RacunarOglasModels;
using System.Collections.Generic;

namespace MiniOglasi.ViewModels
{
    public class RacunarOglasViewModel
    {
        public ICollection<Stanje> Stanja { get; set; }

        public ICollection<Valuta> Valute { get; set; }

        public RacunarOglas RacunarOglas { get; set; }

        public ICollection<MarkaRacunara> MarkeRacunara { get; set; }

        public ICollection<GrafickaKartaMarka> GrafickaKartaMarke { get; set; }

        public ICollection<TipRacunara> TipoviRacunara { get; set; }
    }
}