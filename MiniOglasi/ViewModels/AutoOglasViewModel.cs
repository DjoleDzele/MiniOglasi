using MiniOglasi.Models;
using MiniOglasi.Models.AutoOglasModels;
using System.Collections.Generic;

namespace MiniOglasi.ViewModels
{
    public class AutoOglasViewModel
    {
        public ICollection<Stanje> Stanja { get; set; }

        public ICollection<Valuta> Valute { get; set; }

        public AutoOglas AutoOglas { get; set; }

        public ICollection<MarkaAuta> MarkeAuta { get; set; }
    }
}