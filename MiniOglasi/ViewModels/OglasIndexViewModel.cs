using MiniOglasi.Models;
using MiniOglasi.Models.AutoOglasModels;
using MiniOglasi.Models.NekretninaOglasModels;
using MiniOglasi.Models.RacunarOglasModels;
using PagedList;
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

        public ICollection<Stanje> Stanja { get; set; }

        public IPagedList<Oglas> Oglasi { get; set; }

        //AUTI
        public ICollection<MarkaAuta> MarkeAuta { get; set; }

        //RACUNARI
        public ICollection<TipRacunara> TipoviRacunara { get; set; }

        public ICollection<MarkaRacunara> MarkeRacunara { get; set; }

        public ICollection<GrafickaKartaMarka> GrafickaKartaMarke { get; set; }

        //NEKRETNINE
        public ICollection<TipNekretnine> TipoviNekretnine { get; set; }

        public ICollection<Grad> Gradovi { get; set; }
    }
}