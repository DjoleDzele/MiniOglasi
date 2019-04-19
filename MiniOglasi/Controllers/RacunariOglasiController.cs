using Microsoft.AspNet.Identity;
using MiniOglasi.Models;
using MiniOglasi.Models.RacunarOglasModels;
using MiniOglasi.ViewModels;
using PagedList;
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
        public ActionResult Index(
            int? page,
            int maxProcesorJezgara = 0,
            int minProcesorJezgara = 0,
            int maxProcesorBrzina = 0,
            int minProcesorBrzina = 0,
            int maxRamMemorija = 0,
            int minRamMemorija = 0,
            int maxHddMemorija = 0,
            int minHddMemorija = 0,
            int maxGrafickaMemorija = 0,
            int minGrafickaMemorija = 0,
            int markaGraficke = 0,
            int tipRacunara = 0,
            int markaRacunara = 0,
            int stanje = 0,
            int minCena = 0,
            int maxCena = 0,
            int sortiranje = 0)
        {
            var racunarOglasi = dbContext.Oglasi
                .OfType<RacunarOglas>()
                .Include(o => o.MarkaRacunara)
                .Include(o => o.MarkaGrafickeKarte)
                .Include(o => o.TipRacunara)
                .Include(o => o.Valuta)
                .Include(o => o.Slike);

            if (sortiranje != 0)
            {
                switch (sortiranje)
                {
                    case 1:
                        racunarOglasi = racunarOglasi.OrderByDescending(x => x.ValutaId == 1 ? x.Cena : x.Cena * 120);
                        break;

                    case 2:
                        racunarOglasi = racunarOglasi.OrderBy(x => x.ValutaId == 1 ? x.Cena : x.Cena * 120);
                        break;

                    case 3:
                        racunarOglasi = racunarOglasi.OrderByDescending(x => x.DatumPostavljanja);
                        break;

                    case 4:
                        racunarOglasi = racunarOglasi.OrderBy(x => x.DatumPostavljanja);
                        break;
                }
            }
            if (minCena != 0)
            {
                racunarOglasi = racunarOglasi.Where(x => x.ValutaId == 1 ? x.Cena >= minCena : x.Cena * 120 >= minCena);
            }
            if (maxCena != 0)
            {
                racunarOglasi = racunarOglasi.Where(x => x.ValutaId == 1 ? x.Cena <= maxCena : x.Cena * 120 <= maxCena);
            }
            if (stanje != 0)
            {
                racunarOglasi = racunarOglasi.Where(x => x.StanjeId == stanje);
            }
            if (markaRacunara != 0)
            {
                racunarOglasi = racunarOglasi.Where(x => x.MarkaRacunaraId == markaRacunara);
            }
            if (tipRacunara != 0)
            {
                racunarOglasi = racunarOglasi.Where(x => x.TipRacunaraId == tipRacunara);
            }
            if (markaGraficke != 0)
            {
                racunarOglasi = racunarOglasi.Where(x => x.MarkaGrafickeKarteId == markaGraficke);
            }
            if (maxGrafickaMemorija != 0)
            {
                racunarOglasi = racunarOglasi.Where(x => x.GrafickaMemorija <= maxGrafickaMemorija);
            }
            if (minGrafickaMemorija != 0)
            {
                racunarOglasi = racunarOglasi.Where(x => x.GrafickaMemorija >= minGrafickaMemorija);
            }
            if (maxHddMemorija != 0)
            {
                racunarOglasi = racunarOglasi.Where(x => x.HddMemorija <= maxHddMemorija);
            }
            if (minHddMemorija != 0)
            {
                racunarOglasi = racunarOglasi.Where(x => x.HddMemorija >= minHddMemorija);
            }
            if (maxRamMemorija != 0)
            {
                racunarOglasi = racunarOglasi.Where(x => x.RamMemorija <= maxRamMemorija);
            }
            if (minRamMemorija != 0)
            {
                racunarOglasi = racunarOglasi.Where(x => x.RamMemorija >= minRamMemorija);
            }
            if (maxProcesorBrzina != 0)
            {
                racunarOglasi = racunarOglasi.Where(x => x.ProcesorBrzina <= maxProcesorBrzina);
            }
            if (minProcesorBrzina != 0)
            {
                racunarOglasi = racunarOglasi.Where(x => x.ProcesorBrzina >= minProcesorBrzina);
            }
            if (maxProcesorJezgara != 0)
            {
                racunarOglasi = racunarOglasi.Where(x => x.ProcesorJezgara <= maxProcesorJezgara);
            }
            if (minProcesorJezgara != 0)
            {
                racunarOglasi = racunarOglasi.Where(x => x.ProcesorJezgara >= minProcesorJezgara);
            }

            var stanja = dbContext.Stanja.ToList();
            var tipoviRacunara = dbContext.TipoviRacunara.ToList();
            var markeRacunara = dbContext.MarkeRacunara.ToList();
            var markeGraficke = dbContext.GrafickeKarte.ToList();

            OglasIndexViewModel racunarOglasIndexViewModel = new OglasIndexViewModel(VrstaOglasa.Racunar)
            {
                Oglasi = racunarOglasi.ToList().ToPagedList(page ?? 1, 5),
                Stanja = stanja,
                TipoviRacunara = tipoviRacunara,
                MarkeRacunara = markeRacunara,
                GrafickaKartaMarke = markeGraficke
            };

            return View("IndexOglasa", racunarOglasIndexViewModel);
        }

        public ActionResult Create()
        {
            var racunarOglasViewModel = NapraviRacunarOglasViewModel();

            return View("RacunarOglasForm", racunarOglasViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SaveOglas(RacunarOglasViewModel newRacunarOglasViewModel, List<HttpPostedFileBase> uploadedImages = null)
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

            racunarOglasZaIzmenu.DatumPostavljanja = racunarOglasViewModel.RacunarOglas.DatumPostavljanja == default(DateTime)
                ? DateTime.Now
                : racunarOglasViewModel.RacunarOglas.DatumPostavljanja;

            if (racunarOglasZaIzmenu.Slike == null)
            {
                racunarOglasZaIzmenu.Slike = new Collection<Slika>();
            }

            racunarOglasZaIzmenu.UserAutorOglasaId = racunarOglasViewModel.RacunarOglas.UserAutorOglasaId ?? User.Identity.GetUserId();
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

            return racunarOglasZaIzmenu;
        }
    }
}