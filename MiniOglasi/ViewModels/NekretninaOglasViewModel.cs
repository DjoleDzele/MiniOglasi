using MiniOglasi.Models;
using MiniOglasi.Models.NekretninaOglasModels;
using System.Collections.Generic;

namespace MiniOglasi.ViewModels
{
    public class NekretninaOglasViewModel
    {
        public ICollection<Stanje> Stanja { get; set; }

        public ICollection<Valuta> Valute { get; set; }

        public NekretninaOglas NekretninaOglas { get; set; }

        public ICollection<TipNekretnine> TipoviNekretnina { get; set; }

        public ICollection<Grad> Gradovi { get; set; }
    }
}