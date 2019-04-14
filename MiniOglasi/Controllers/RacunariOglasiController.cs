using Microsoft.AspNet.Identity;
using MiniOglasi.Models;
using MiniOglasi.Models.RacunarOglasModels;
using MiniOglasi.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MiniOglasi.Controllers
{
    public class RacunariOglasiController : Controller
    {
        private readonly ApplicationDbContext dbContext;

        public RacunariOglasiController()
        {
            dbContext = new ApplicationDbContext();
        }

        [AllowAnonymous]
        public ActionResult Index()
        {
            var racunarOglasi = dbContext.Oglasi
                .OfType<RacunarOglas>()
                .Include(o => o.MarkaRacunara)
                .Include(o => o.MarkaGrafickeKarte)
                .Include(o => o.TipRacunara)
                .Include(o => o.Valuta)
                .Include(o => o.Slike);

            return View("IndexOglasa", racunarOglasi);
        }

        public ActionResult Create()
        {
            var racunarOglasViewModel = NapraviRacunarOglasViewModel();

            return View("RacunarOglasForm", racunarOglasViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SaveRacunarOglas(RacunarOglasViewModel newRacunarOglasViewModel, List<HttpPostedFileBase> uploadedImages = null)
        {
            //Za slucaj da mora da se vrati na formu ako je not valid*********************************************
            RacunarOglas racunarOglasUBazi = dbContext.Oglasi
                                                .OfType<RacunarOglas>()
                                                .Include(o => o.Slike)
                                                .SingleOrDefault(o => o.Id == newRacunarOglasViewModel.RacunarOglas.Id);

            var markeRacunara = dbContext.MarkeRacunara;
            var grafickeKarte = dbContext.GrafickeKarte;
            var tipoviRacunara = dbContext.TipoviRacunara;
            var stanja = dbContext.Stanja;
            var valute = dbContext.Valute;

            newRacunarOglasViewModel.GrafickaKartaMarke = grafickeKarte.ToList();
            newRacunarOglasViewModel.MarkeRacunara = markeRacunara.ToList();
            newRacunarOglasViewModel.TipoviRacunara = tipoviRacunara.ToList();
            newRacunarOglasViewModel.Stanja = stanja.ToList();
            newRacunarOglasViewModel.Valute = valute.ToList();

            if (newRacunarOglasViewModel.RacunarOglas.Id != 0)
            {
                newRacunarOglasViewModel.RacunarOglas.Slike = racunarOglasUBazi.Slike;
            }
            //Za slucaj da mora da se vrati na formu ako je not valid*********************************************

            PomocnaKlasa.TipoviGreskeUploadSlika tipGreskeUploadSlika = PomocnaKlasa.ProveriValidnostUploadovanihSlika(uploadedImages);

            switch (tipGreskeUploadSlika)
            {
                case PomocnaKlasa.TipoviGreskeUploadSlika.MaxPetSlikaPoOglasu:
                    ViewBag.Greska = "Maksimalno 5 slika po oglasu!";
                    break;

                case PomocnaKlasa.TipoviGreskeUploadSlika.PogresanFormatSlika:
                    ViewBag.Greska = "Izaberite samo slike!";
                    break;

                case PomocnaKlasa.TipoviGreskeUploadSlika.PrevelikaSlika:
                    ViewBag.Greska = "Svaki fajl mora biti manji od 500kb!";
                    break;
            }
            if (tipGreskeUploadSlika != PomocnaKlasa.TipoviGreskeUploadSlika.NemaGreske)
            {
                return View("RacunarOglasForm", newRacunarOglasViewModel);
            }

            if (!ModelState.IsValid)
            {
                ViewBag.Greska = "Proverite unesene podatke";

                return View("RacunarOglasForm", newRacunarOglasViewModel);
            }
            else
            {
                string userId = User.Identity.GetUserId();

                if (newRacunarOglasViewModel.RacunarOglas.Id != 0)
                {
                    PopuniRacunarOglas(newRacunarOglasViewModel, racunarOglasUBazi);

                    if (PomocnaKlasa.DaLiDodajeViseOdPetSlika(uploadedImages, racunarOglasUBazi))
                    {
                        ViewBag.Greska = "Maksimalno 5 slika po oglasu!";

                        return View("RacunarOglasForm", newRacunarOglasViewModel);
                    }

                    string oglasId = racunarOglasUBazi.Id.ToString();
                    PomocnaKlasa.DodajSlikeOglasu(userId, oglasId, racunarOglasUBazi, uploadedImages);
                }
                else
                {
                    RacunarOglas newRacunarOglas = PopuniRacunarOglas(newRacunarOglasViewModel);

                    dbContext.Oglasi.Add(newRacunarOglas);
                    dbContext.SaveChanges();

                    string oglasId = newRacunarOglas.Id.ToString();

                    PomocnaKlasa.DodajSlikeOglasu(userId, oglasId, newRacunarOglas, uploadedImages);
                }

                dbContext.SaveChanges();

                return RedirectToAction("Index");
            }
        }

        [AllowAnonymous]
        public ActionResult Details(int id)
        {
            var racunarOglas = dbContext.Oglasi
                .OfType<RacunarOglas>()
                .Include(o => o.MarkaGrafickeKarte)
                .Include(o => o.TipRacunara)
                .Include(o => o.MarkaRacunara)
                .Include(o => o.Slike)
                .Include(o => o.Stanje)
                .Include(o => o.Valuta)
                .Include(o => o.UserAutorOglasa)
                .SingleOrDefault(o => o.Id == id);

            return View("DetaljiOglasa", racunarOglas);
        }

        public ActionResult Edit(int id)
        {
            var stariRacunarOglas = dbContext.Oglasi.
                                    OfType<RacunarOglas>().
                                    Include(o => o.Slike).
                                    SingleOrDefault(o => o.Id == id);
            if (stariRacunarOglas == null)
            {
                return HttpNotFound();
            }
            else
            {
                var racunarOglasViewModel = NapraviRacunarOglasViewModel(stariRacunarOglas);

                return View("RacunarOglasForm", racunarOglasViewModel);
            }
        }

        //**********************************************************************************

        [NonAction]
        public RacunarOglasViewModel NapraviRacunarOglasViewModel(RacunarOglas oglasIzBaze = null)
        {
            var markeRacunara = dbContext.MarkeRacunara;
            var grafickeKarte = dbContext.GrafickeKarte;
            var tipoviRacunara = dbContext.TipoviRacunara;
            var stanja = dbContext.Stanja;
            var valute = dbContext.Valute;

            return new RacunarOglasViewModel()
            {
                RacunarOglas = oglasIzBaze ?? new RacunarOglas(),
                MarkeRacunara = markeRacunara.ToList(),
                TipoviRacunara = tipoviRacunara.ToList(),
                GrafickaKartaMarke = grafickeKarte.ToList(),
                Stanja = stanja.ToList(),
                Valute = valute.ToList()
            };
        }

        [NonAction]
        public RacunarOglas PopuniRacunarOglas(RacunarOglasViewModel racunarOglasViewModel, RacunarOglas racunarOglasZaIzmenu = null)
        {
            if (racunarOglasZaIzmenu == null)
            {
                racunarOglasZaIzmenu = new RacunarOglas();
            }
            // >> ZAJEDNICKI >>>
            racunarOglasZaIzmenu.NaslovOglasa = racunarOglasViewModel.RacunarOglas.NaslovOglasa;
            racunarOglasZaIzmenu.OpisOglasa = racunarOglasViewModel.RacunarOglas.OpisOglasa;
            racunarOglasZaIzmenu.Cena = racunarOglasViewModel.RacunarOglas.Cena;
            racunarOglasZaIzmenu.ValutaId = racunarOglasViewModel.RacunarOglas.ValutaId;
            racunarOglasZaIzmenu.StanjeId = racunarOglasViewModel.RacunarOglas.StanjeId;
            // <<< ZAJEDNICKI <<<
            racunarOglasZaIzmenu.MarkaRacunaraId = racunarOglasViewModel.RacunarOglas.MarkaRacunaraId;
            racunarOglasZaIzmenu.TipRacunaraId = racunarOglasViewModel.RacunarOglas.TipRacunaraId;
            racunarOglasZaIzmenu.MarkaGrafickeKarteId = racunarOglasViewModel.RacunarOglas.MarkaGrafickeKarteId;
            racunarOglasZaIzmenu.GrafickaMemorija = racunarOglasViewModel.RacunarOglas.GrafickaMemorija;
            racunarOglasZaIzmenu.HddMemorija = racunarOglasViewModel.RacunarOglas.HddMemorija;
            racunarOglasZaIzmenu.RamMemorija = racunarOglasViewModel.RacunarOglas.RamMemorija;
            racunarOglasZaIzmenu.ProcesorBrzina = racunarOglasViewModel.RacunarOglas.ProcesorBrzina;
            racunarOglasZaIzmenu.ProcesorJezgara = racunarOglasViewModel.RacunarOglas.ProcesorJezgara;
            racunarOglasZaIzmenu.Monitor = racunarOglasViewModel.RacunarOglas.Monitor;
            racunarOglasZaIzmenu.Tastatura = racunarOglasViewModel.RacunarOglas.Tastatura;
            racunarOglasZaIzmenu.Mis = racunarOglasViewModel.RacunarOglas.Mis;

            racunarOglasZaIzmenu.DatumPostavljanja = racunarOglasViewModel.RacunarOglas.DatumPostavljanja == default(DateTime)
                ? DateTime.Now
                : racunarOglasViewModel.RacunarOglas.DatumPostavljanja;

            if (racunarOglasZaIzmenu.Slike == null)
            {
                racunarOglasZaIzmenu.Slike = new Collection<Slika>();
            }

            racunarOglasZaIzmenu.UserAutorOglasaId = racunarOglasViewModel.RacunarOglas.UserAutorOglasaId ?? User.Identity.GetUserId();

            return racunarOglasZaIzmenu;
        }
    }
}