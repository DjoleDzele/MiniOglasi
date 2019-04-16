﻿using MiniOglasi.Models;
using MiniOglasi.Models.AutoOglasModels;
using MiniOglasi.Models.RacunarOglasModels;
using System.Collections.Generic;

namespace MiniOglasi.ViewModels
{
    public class OglasIndexViewModel
    {
        public OglasIndexViewModel(VrstaOglasa vrstaOglasa)
        {
            _vrstaOglasa = vrstaOglasa;
        }

        public VrstaOglasa _vrstaOglasa { get; set; }

        public IEnumerable<Oglas> Oglasi { get; set; }

        public ICollection<Stanje> Stanja { get; set; }

        //AUTI
        public ICollection<MarkaAuta> MarkeAuta { get; set; }

        //RACUNARI
        public ICollection<TipRacunara> TipoviRacunara { get; set; }
    }
}