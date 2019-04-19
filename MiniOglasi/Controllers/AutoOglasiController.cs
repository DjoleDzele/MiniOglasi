using Microsoft.AspNet.Identity;
using MiniOglasi.Models;
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
    public class AutoOglasiController : Controller
    {
        private readonly ApplicationDbContext dbContext;

        public AutoOglasiController()
        {
            dbContext = new ApplicationDbContext();
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult Index(
            int? page,
            int izborGodista1 = 0,
            int izborGodista2 = 0,
            int minKubikaza = 0,
            int maxKubikaza = 0,
            int minKonjskihSnaga = 0,
            int maxKonjskihSnaga = 0,
            int maxKilometraza = 0,
            int modelAuta = 0,
            int markaAuta = 0,
            int stanje = 0,
            int minCena = 0,
            int maxCena = 0,
            int sortiranje = 0)
        {
            var autoOglasi = dbContext.Oglasi
                .OfType<AutoOglas>()
                .Include(o => o.MarkaAuta)
                .Include(o => o.ModelAuta)
                .Include(o => o.Valuta)
                .Include(o => o.Slike);

            if (sortiranje != 0)
            {
                switch (sortiranje)
                {
                    case 1:
                        autoOglasi = autoOglasi.OrderByDescending(x => x.ValutaId == 1 ? x.Cena : x.Cena * 120);
                        break;

                    case 2:
                        autoOglasi = autoOglasi.OrderBy(x => x.ValutaId == 1 ? x.Cena : x.Cena * 120);
                        break;

                    case 3:
                        autoOglasi = autoOglasi.OrderByDescending(x => x.DatumPostavljanja);
                        break;

                    case 4:
                        autoOglasi = autoOglasi.OrderBy(x => x.DatumPostavljanja);
                        break;
                }
            }
            if (minCena != 0)
            {
                autoOglasi = autoOglasi.Where(x => x.ValutaId == 1 ? x.Cena >= minCena : x.Cena * 120 >= minCena);
            }
            if (maxCena != 0)
            {
                autoOglasi = autoOglasi.Where(x => x.ValutaId == 1 ? x.Cena <= maxCena : x.Cena * 120 <= maxCena);
            }
            if (stanje != 0)
            {
                autoOglasi = autoOglasi.Where(x => x.StanjeId == stanje);
            }
            if (markaAuta != 0)
            {
                autoOglasi = autoOglasi.Where(x => x.MarkaAutaId == markaAuta);
            }
            if (modelAuta != 0)
            {
                autoOglasi = autoOglasi.Where(x => x.ModelAutaId == modelAuta);
            }
            if (maxKilometraza != 0)
            {
                autoOglasi = autoOglasi.Where(x => x.Kilometraza <= maxKilometraza);
            }
            if (minKonjskihSnaga != 0)
            {
                autoOglasi = autoOglasi.Where(x => x.KonjskeSnage >= minKonjskihSnaga);
            }
            if (maxKonjskihSnaga != 0)
            {
                autoOglasi = autoOglasi.Where(x => x.KonjskeSnage <= maxKonjskihSnaga);
            }
            if (minKubikaza != 0)
            {
                autoOglasi = autoOglasi.Where(x => x.Kubikaza >= minKubikaza);
            }
            if (maxKubikaza != 0)
            {
                autoOglasi = autoOglasi.Where(x => x.Kubikaza <= maxKubikaza);
            }
            if (izborGodista1 != 0)
            {
                autoOglasi = autoOglasi.Where(x => x.Godiste >= izborGodista1);
            }
            if (izborGodista2 != 0)
            {
                autoOglasi = autoOglasi.Where(x => x.Godiste <= izborGodista2);
            }

            var markeAuta = dbContext.MarkeAuta.ToList();
            var stanja = dbContext.Stanja.ToList();

            OglasIndexViewModel autoOglasIndexViewModel = new OglasIndexViewModel(VrstaOglasa.Auto)
            {
                Oglasi = autoOglasi.ToList().ToPagedList(page ?? 1, 5),
                MarkeAuta = markeAuta,
                Stanja = stanja
            };

            return View("IndexOglasa", autoOglasIndexViewModel);
        }

        public ActionResult Create()
        {
            var autoOglasViewModel = NapraviAutoOglasViewModel();

            return View("AutoOglasForm", autoOglasViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SaveOglas(AutoOglasViewModel newAutoOglasViewModel, List<HttpPostedFileBase> uploadedImages = null)
        {
            //Za slucaj da mora da se vrati na formu zbog modelstate not valid**************************************
            AutoOglas autoOglasUBazi = dbContext.Oglasi
                                                .OfType<AutoOglas>()
                                                .Include(o => o.Slike)
                                                .SingleOrDefault(o => o.Id == newAutoOglasViewModel.AutoOglas.Id);

            var markeAuta = dbContext.MarkeAuta;
            var stanja = dbContext.Stanja;
            var valute = dbContext.Valute;

            newAutoOglasViewModel.MarkeAuta = markeAuta.ToList();
            newAutoOglasViewModel.Stanja = stanja.ToList();
            newAutoOglasViewModel.Valute = valute.ToList();

            if (newAutoOglasViewModel.AutoOglas.Id != 0)
            {
                newAutoOglasViewModel.AutoOglas.Slike = autoOglasUBazi.Slike;
            }
            //Za slucaj da mora da se vrati na formu zbog modelstate not valid**************************************

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
                return View("AutoOglasForm", newAutoOglasViewModel);
            }

            if (!ModelState.IsValid)
            {
                ViewBag.Greska = "Proverite unesene podatke";

                return View("AutoOglasForm", newAutoOglasViewModel);
            }
            else
            {
                string userId = User.Identity.GetUserId();

                if (newAutoOglasViewModel.AutoOglas.Id != 0)
                {
                    PopuniAutoOglas(newAutoOglasViewModel, autoOglasUBazi);

                    if (PomocnaKlasa.DaLiDodajeViseOdPetSlika(uploadedImages, autoOglasUBazi))
                    {
                        ViewBag.Greska = "Maksimalno 5 slika po oglasu!";

                        return View("AutoOglasForm", newAutoOglasViewModel);
                    }

                    string oglasId = autoOglasUBazi.Id.ToString();
                    PomocnaKlasa.DodajSlikeOglasu(userId, oglasId, autoOglasUBazi, uploadedImages);
                }
                else
                {
                    AutoOglas newAutoOglas = PopuniAutoOglas(newAutoOglasViewModel);

                    dbContext.Oglasi.Add(newAutoOglas);
                    dbContext.SaveChanges();

                    string oglasId = newAutoOglas.Id.ToString();

                    PomocnaKlasa.DodajSlikeOglasu(userId, oglasId, newAutoOglas, uploadedImages);
                }

                dbContext.SaveChanges();

                return RedirectToAction("Index");
            }
        }

        [AllowAnonymous]
        public ActionResult Details(int id)
        {
            var autoOglas = dbContext.Oglasi
                .OfType<AutoOglas>()
                .Include(o => o.MarkaAuta)
                .Include(o => o.ModelAuta)
                .Include(o => o.Slike)
                .Include(o => o.Stanje)
                .Include(o => o.Valuta)
                .Include(o => o.UserAutorOglasa)
                .Include(o => o.UserAutorOglasa.Grad)
                .SingleOrDefault(o => o.Id == id);

            return View("DetaljiOglasa", autoOglas);
        }

        public ActionResult Edit(int id)
        {
            var stariAutoOglas = dbContext.Oglasi.
                                    OfType<AutoOglas>().
                                    Include(o => o.Slike).
                                    SingleOrDefault(o => o.Id == id);

            if (stariAutoOglas == null)
            {
                return HttpNotFound();
            }
            else
            {
                var autoOglasViewModel = NapraviAutoOglasViewModel(stariAutoOglas);

                return View("AutoOglasForm", autoOglasViewModel);
            }
        }

        //**********************************************************************************

        [NonAction]
        public AutoOglasViewModel NapraviAutoOglasViewModel(AutoOglas oglasIzBaze = null)
        {
            var markeAuta = dbContext.MarkeAuta;
            var stanja = dbContext.Stanja;
            var valute = dbContext.Valute;

            return new AutoOglasViewModel()
            {
                AutoOglas = oglasIzBaze ?? new AutoOglas(),
                MarkeAuta = markeAuta.ToList(),
                Stanja = stanja.ToList(),
                Valute = valute.ToList()
            };
        }

        [NonAction]
        public AutoOglas PopuniAutoOglas(AutoOglasViewModel autoOglasViewModel, AutoOglas autoOglasZaIzmenu = null)
        {
            if (autoOglasZaIzmenu == null)
            {
                autoOglasZaIzmenu = new AutoOglas();
            }
            // >> ZAJEDNICKI >>>
            autoOglasZaIzmenu.NaslovOglasa = autoOglasViewModel.AutoOglas.NaslovOglasa;
            autoOglasZaIzmenu.OpisOglasa = autoOglasViewModel.AutoOglas.OpisOglasa;
            autoOglasZaIzmenu.Cena = autoOglasViewModel.AutoOglas.Cena;
            autoOglasZaIzmenu.ValutaId = autoOglasViewModel.AutoOglas.ValutaId;
            autoOglasZaIzmenu.StanjeId = autoOglasViewModel.AutoOglas.StanjeId;

            autoOglasZaIzmenu.DatumPostavljanja = autoOglasViewModel.AutoOglas.DatumPostavljanja == default(DateTime)
                ? DateTime.Now
                : autoOglasViewModel.AutoOglas.DatumPostavljanja;

            if (autoOglasZaIzmenu.Slike == null)
            {
                autoOglasZaIzmenu.Slike = new Collection<Slika>();
            }

            autoOglasZaIzmenu.UserAutorOglasaId = autoOglasViewModel.AutoOglas.UserAutorOglasaId ?? User.Identity.GetUserId();

            // <<< ZAJEDNICKI <<<
            autoOglasZaIzmenu.Godiste = autoOglasViewModel.AutoOglas.Godiste;
            autoOglasZaIzmenu.Kilometraza = autoOglasViewModel.AutoOglas.Kilometraza;
            autoOglasZaIzmenu.KonjskeSnage = autoOglasViewModel.AutoOglas.KonjskeSnage;
            autoOglasZaIzmenu.Kubikaza = autoOglasViewModel.AutoOglas.Kubikaza;
            autoOglasZaIzmenu.MarkaAutaId = autoOglasViewModel.AutoOglas.MarkaAutaId;
            autoOglasZaIzmenu.ModelAutaId = autoOglasViewModel.AutoOglas.ModelAutaId;

            return autoOglasZaIzmenu;
        }
    }
}