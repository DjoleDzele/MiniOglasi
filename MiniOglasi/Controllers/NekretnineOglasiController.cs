using Microsoft.AspNet.Identity;
using MiniOglasi.Models;
using MiniOglasi.Models.NekretninaOglasModels;
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
    public class NekretnineOglasiController : Controller
    {
        private readonly ApplicationDbContext dbContext;

        public NekretnineOglasiController()
        {
            dbContext = new ApplicationDbContext();
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult Index(
            int? page,
            int vrstaUsluge = 0,
            int tipGradnje = 0,
            int tipNekretnine = 0,
            int lokacijaNekretnine = 0,
            int minKvadratura = 0,
            int maxKvadratura = 0,
            int minBrojSoba = 0,
            int maxBrojSoba = 0,
            //int stanje = 0,
            int minCena = 0,
            int maxCena = 0,
            int sortiranje = 0)
        {
            var nekretnineOglasi = dbContext.Oglasi
                .OfType<NekretninaOglas>()
                .Include(o => o.Grad)
                .Include(o => o.TipNekretnine)
                .Include(o => o.TipGradnje)
                .Include(o => o.RezimOglasaNekretnine)
                .Include(o => o.Valuta)
                .Include(o => o.Slike);

            if (sortiranje != 0)
            {
                switch (sortiranje)
                {
                    case 1:
                        nekretnineOglasi = nekretnineOglasi.OrderByDescending(x => x.ValutaId == 1 ? x.Cena : x.Cena * 120);
                        break;

                    case 2:
                        nekretnineOglasi = nekretnineOglasi.OrderBy(x => x.ValutaId == 1 ? x.Cena : x.Cena * 120);
                        break;

                    case 3:
                        nekretnineOglasi = nekretnineOglasi.OrderByDescending(x => x.DatumPostavljanja);
                        break;

                    case 4:
                        nekretnineOglasi = nekretnineOglasi.OrderBy(x => x.DatumPostavljanja);
                        break;
                }
            }
            if (minCena != 0)
            {
                nekretnineOglasi = nekretnineOglasi.Where(x => x.ValutaId == 1 ? x.Cena >= minCena : x.Cena * 120 >= minCena);
            }
            if (maxCena != 0)
            {
                nekretnineOglasi = nekretnineOglasi.Where(x => x.ValutaId == 1 ? x.Cena <= maxCena : x.Cena * 120 <= maxCena);
            }
            //if (stanje != 0)
            //{
            //    nekretnineOglasi = nekretnineOglasi.Where(x => x.StanjeId == stanje);
            //}
            if (maxBrojSoba != 0)
            {
                nekretnineOglasi = nekretnineOglasi.Where(x => x.BrojSoba <= maxBrojSoba);
            }
            if (minBrojSoba != 0)
            {
                nekretnineOglasi = nekretnineOglasi.Where(x => x.BrojSoba >= minBrojSoba);
            }
            if (maxKvadratura != 0)
            {
                nekretnineOglasi = nekretnineOglasi.Where(x => x.Kvadratura <= maxKvadratura);
            }
            if (minKvadratura != 0)
            {
                nekretnineOglasi = nekretnineOglasi.Where(x => x.Kvadratura >= minKvadratura);
            }
            if (lokacijaNekretnine != 0)
            {
                nekretnineOglasi = nekretnineOglasi.Where(x => x.GradId == lokacijaNekretnine);
            }
            if (tipNekretnine != 0)
            {
                nekretnineOglasi = nekretnineOglasi.Where(x => x.TipNekretnineId == tipNekretnine);
            }
            if (vrstaUsluge != 0)
            {
                nekretnineOglasi = nekretnineOglasi.Where(x => x.RezimOglasaNekretnineId == vrstaUsluge);
            }
            if (tipGradnje != 0)
            {
                nekretnineOglasi = nekretnineOglasi.Where(x => x.TipGradnjeId == tipGradnje);
            }

            //var stanja = dbContext.Stanja.ToList();
            var tipoviNekretnine = dbContext.TipoviNekretnina.ToList();
            var gradovi = dbContext.Gradovi.ToList();
            var rezimiNekretnina = dbContext.RezimiOglasaNekretnine.ToList();
            var tipoviGradnje = dbContext.TipoviGradnje.ToList();

            OglasIndexViewModel nekretnineOglasIndexViewModel = new OglasIndexViewModel(VrstaOglasa.Nekretnina)
            {
                Oglasi = nekretnineOglasi.ToList().ToPagedList(page ?? 1, 5),
                TipoviNekretnine = tipoviNekretnine,
                Gradovi = gradovi,
                RezimiOglasaNekretnina = rezimiNekretnina,
                TipoviGradnje = tipoviGradnje
            };

            return View("IndexOglasa", nekretnineOglasIndexViewModel);
        }

        public ActionResult Create()
        {
            var nekretninaOglasViewModel = NapraviNekretninuOglasViewModel();

            return View("NekretninaOglasForm", nekretninaOglasViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SaveOglas(NekretninaOglasViewModel newNekretninaOglasViewModel, List<HttpPostedFileBase> uploadedImages = null)
        {
            //Za slucaj da mora da se vrati na formu zbog modelstate not valid**************************************
            NekretninaOglas nekretninaOglasUBazi = dbContext.Oglasi
                                                .OfType<NekretninaOglas>()
                                                .Include(o => o.Slike)
                                                .SingleOrDefault(o => o.Id == newNekretninaOglasViewModel.NekretninaOglas.Id);

            //var stanja = dbContext.Stanja;
            var valute = dbContext.Valute;
            var tipoviNekretnine = dbContext.TipoviNekretnina;
            var gradovi = dbContext.Gradovi;
            var rezimiNekretnina = dbContext.RezimiOglasaNekretnine;
            var tipoviGradnje = dbContext.TipoviGradnje;

            //newNekretninaOglasViewModel.Stanja = stanja.ToList();
            newNekretninaOglasViewModel.Valute = valute.ToList();
            newNekretninaOglasViewModel.TipoviNekretnina = tipoviNekretnine.ToList();
            newNekretninaOglasViewModel.Gradovi = gradovi.ToList();
            newNekretninaOglasViewModel.TipoviGradnje = tipoviGradnje.ToList();
            newNekretninaOglasViewModel.RezimiOglasaNekretnina = rezimiNekretnina.ToList();

            if (newNekretninaOglasViewModel.NekretninaOglas.Id != 0)
            {
                newNekretninaOglasViewModel.NekretninaOglas.Slike = nekretninaOglasUBazi.Slike;
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
                return View("NekretninaOglasForm", newNekretninaOglasViewModel);
            }

            if (!ModelState.IsValid)
            {
                ViewBag.Greska = "Proverite unesene podatke";

                return View("NekretninaOglasForm", newNekretninaOglasViewModel);
            }
            else
            {
                string userId = User.Identity.GetUserId();

                if (newNekretninaOglasViewModel.NekretninaOglas.Id != 0)
                {
                    PopuniNekretninuOglas(newNekretninaOglasViewModel, nekretninaOglasUBazi);

                    if (PomocnaKlasa.DaLiDodajeViseOdPetSlika(uploadedImages, nekretninaOglasUBazi))
                    {
                        ViewBag.Greska = "Maksimalno 5 slika po oglasu!";

                        return View("NekretninaOglasForm", newNekretninaOglasViewModel);
                    }

                    string oglasId = nekretninaOglasUBazi.Id.ToString();
                    PomocnaKlasa.DodajSlikeOglasu(userId, oglasId, nekretninaOglasUBazi, uploadedImages);
                }
                else
                {
                    NekretninaOglas newNekretninaOglas = PopuniNekretninuOglas(newNekretninaOglasViewModel);

                    dbContext.Oglasi.Add(newNekretninaOglas);
                    dbContext.SaveChanges();

                    string oglasId = newNekretninaOglas.Id.ToString();

                    PomocnaKlasa.DodajSlikeOglasu(userId, oglasId, newNekretninaOglas, uploadedImages);
                }

                dbContext.SaveChanges();

                return RedirectToAction("Index");
            }
        }

        [AllowAnonymous]
        public ActionResult Details(int id)
        {
            var nekretninaOglas = dbContext.Oglasi
                .OfType<NekretninaOglas>()
                .Include(o => o.Grad)
                .Include(o => o.TipNekretnine)
                .Include(o => o.Slike)
                //.Include(o => o.Stanje)
                .Include(o => o.Valuta)
                .Include(o => o.RezimOglasaNekretnine)
                .Include(o => o.TipGradnje)
                .Include(o => o.UserAutorOglasa)
                .Include(o => o.UserAutorOglasa.Grad)
                .SingleOrDefault(o => o.Id == id);

            return View("DetaljiOglasa", nekretninaOglas);
        }

        public ActionResult Edit(int id)
        {
            var stariNekretninaOglas = dbContext.Oglasi.
                                    OfType<NekretninaOglas>().
                                    Include(o => o.Slike).
                                    SingleOrDefault(o => o.Id == id);

            if (stariNekretninaOglas == null)
            {
                return HttpNotFound();
            }
            else
            {
                var nekretninaOglasViewModel = NapraviNekretninuOglasViewModel(stariNekretninaOglas);

                return View("NekretninaOglasForm", nekretninaOglasViewModel);
            }
        }

        //**********************************************************************************

        [NonAction]
        public NekretninaOglasViewModel NapraviNekretninuOglasViewModel(NekretninaOglas oglasIzBaze = null)
        {
            //var stanja = dbContext.Stanja;
            var valute = dbContext.Valute;
            var tipoviNekretnine = dbContext.TipoviNekretnina;
            var gradovi = dbContext.Gradovi;
            var rezimiNekretnina = dbContext.RezimiOglasaNekretnine;
            var tipoviGradnje = dbContext.TipoviGradnje;

            return new NekretninaOglasViewModel()
            {
                NekretninaOglas = oglasIzBaze ?? new NekretninaOglas(),
                //Stanja = stanja.ToList(),
                Valute = valute.ToList(),
                TipoviNekretnina = tipoviNekretnine.ToList(),
                Gradovi = gradovi.ToList(),
                RezimiOglasaNekretnina = rezimiNekretnina.ToList(),
                TipoviGradnje = tipoviGradnje.ToList(),
            };
        }

        [NonAction]
        public NekretninaOglas PopuniNekretninuOglas(NekretninaOglasViewModel nekretninaOglasViewModel, NekretninaOglas nekretninaOglasZaIzmenu = null)
        {
            if (nekretninaOglasZaIzmenu == null)
            {
                nekretninaOglasZaIzmenu = new NekretninaOglas();
            }
            // >> ZAJEDNICKI >>>
            nekretninaOglasZaIzmenu.NaslovOglasa = nekretninaOglasViewModel.NekretninaOglas.NaslovOglasa;
            nekretninaOglasZaIzmenu.OpisOglasa = nekretninaOglasViewModel.NekretninaOglas.OpisOglasa;
            nekretninaOglasZaIzmenu.Cena = nekretninaOglasViewModel.NekretninaOglas.Cena;
            nekretninaOglasZaIzmenu.ValutaId = nekretninaOglasViewModel.NekretninaOglas.ValutaId;
            //nekretninaOglasZaIzmenu.StanjeId = nekretninaOglasViewModel.NekretninaOglas.StanjeId;

            nekretninaOglasZaIzmenu.DatumPostavljanja = nekretninaOglasViewModel.NekretninaOglas.DatumPostavljanja == default(DateTime)
                ? DateTime.Now
                : nekretninaOglasViewModel.NekretninaOglas.DatumPostavljanja;

            if (nekretninaOglasZaIzmenu.Slike == null)
            {
                nekretninaOglasZaIzmenu.Slike = new Collection<Slika>();
            }

            nekretninaOglasZaIzmenu.UserAutorOglasaId = nekretninaOglasViewModel.NekretninaOglas.UserAutorOglasaId ?? User.Identity.GetUserId();
            // <<< ZAJEDNICKI <<<

            nekretninaOglasZaIzmenu.TipGradnjeId = nekretninaOglasViewModel.NekretninaOglas.TipGradnjeId;
            nekretninaOglasZaIzmenu.RezimOglasaNekretnineId = nekretninaOglasViewModel.NekretninaOglas.RezimOglasaNekretnineId;
            nekretninaOglasZaIzmenu.TipNekretnineId = nekretninaOglasViewModel.NekretninaOglas.TipNekretnineId;
            nekretninaOglasZaIzmenu.GradId = nekretninaOglasViewModel.NekretninaOglas.GradId;
            nekretninaOglasZaIzmenu.Kvadratura = nekretninaOglasViewModel.NekretninaOglas.Kvadratura;
            nekretninaOglasZaIzmenu.BrojSoba = nekretninaOglasViewModel.NekretninaOglas.BrojSoba;
            nekretninaOglasZaIzmenu.Internet = nekretninaOglasViewModel.NekretninaOglas.Internet;
            nekretninaOglasZaIzmenu.Grejanje = nekretninaOglasViewModel.NekretninaOglas.Grejanje;
            nekretninaOglasZaIzmenu.Kablovska = nekretninaOglasViewModel.NekretninaOglas.Kablovska;
            nekretninaOglasZaIzmenu.Terasa = nekretninaOglasViewModel.NekretninaOglas.Terasa;

            return nekretninaOglasZaIzmenu;
        }
    }
}