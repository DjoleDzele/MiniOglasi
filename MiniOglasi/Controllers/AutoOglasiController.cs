using Microsoft.AspNet.Identity;
using MiniOglasi.Models;
using MiniOglasi.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.IO;
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

        public ActionResult Index()
        {
            var autoOglasi = dbContext.Oglasi
                .OfType<AutoOglas>()
                .Include(o => o.MarkaAuta)
                .Include(o => o.ModelAuta)
                .Include(o => o.Valuta)
                .Include(o => o.Slike);

            return View(autoOglasi);
        }

        public ActionResult Create()
        {
            var markeAuta = dbContext.MarkeAuta;
            //var modeliAuta = dbContext.ModeliAuta;
            var stanja = dbContext.Stanja;
            var valute = dbContext.Valute;

            AutoOglasViewModel autoOglasViewModel = new AutoOglasViewModel()
            {
                AutoOglas = new AutoOglas(),
                MarkeAuta = markeAuta.ToList(),
                //ModeliAuta = modeliAuta.ToList(),
                Stanja = stanja.ToList(),
                Valute = valute.ToList()
            };

            return View("AutoOglasForm", autoOglasViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SaveAutoOglas(AutoOglasViewModel newAutoOglasViewModel, List<HttpPostedFileBase> uploadedImages = null)
        {
            //Za slucaj da mora da se vrati na formu zbog modelstate not valid
            AutoOglas autoOglasUBazi = dbContext.Oglasi
                                                .OfType<AutoOglas>()
                                                .Include(o => o.Slike)
                                                .SingleOrDefault(o => o.Id == newAutoOglasViewModel.AutoOglas.Id);
            var markeAuta = dbContext.MarkeAuta;
            //var modeliAuta = dbContext.ModeliAuta;
            var stanja = dbContext.Stanja;
            var valute = dbContext.Valute;

            newAutoOglasViewModel.MarkeAuta = markeAuta.ToList();
            //newAutoOglasViewModel.ModeliAuta = modeliAuta.ToList();
            newAutoOglasViewModel.Stanja = stanja.ToList();
            newAutoOglasViewModel.Valute = valute.ToList();
            if (newAutoOglasViewModel.AutoOglas.Id != 0)
            {
                newAutoOglasViewModel.AutoOglas.Slike = autoOglasUBazi.Slike;
            }
            //Za slucaj da mora da se vrati na formu zbog modelstate not valid

            if (uploadedImages?.Any(x => x != null) == true)
            {
                if (uploadedImages.Count > 5)
                {
                    ViewBag.Greska = "Maksimalno 5 slika po oglasu!";

                    return View("AutoOglasForm", newAutoOglasViewModel);
                }

                foreach (HttpPostedFileBase slika in uploadedImages)
                {
                    if (!PomocnaKlasa.JeLiTacanFormatFajla(slika))
                    {
                        ViewBag.Greska = "Izaberite samo slike!";

                        return View("AutoOglasForm", newAutoOglasViewModel);
                    }

                    if (slika.ContentLength > 500 * 1024)
                    {
                        ViewBag.Greska = "Svaki fajl mora biti manji od 500kb!";

                        return View("AutoOglasForm", newAutoOglasViewModel);
                    }
                }
            }

            if (!ModelState.IsValid)
            {
                ViewBag.Greska = "Proverite unesene podatke";

                return View("AutoOglasForm", newAutoOglasViewModel);
            }
            else
            {
                if (newAutoOglasViewModel.AutoOglas.Id != 0)
                {
                    PopuniAutoOglas(newAutoOglasViewModel, autoOglasUBazi);

                    string userId = User.Identity.GetUserId();
                    string oglasId = autoOglasUBazi.Id.ToString();
                    string punaPutanjaFolderaZaSlikeOglasa = Path.Combine(Server.MapPath(PomocnaKlasa.ImagesFolder), userId, oglasId);

                    if (uploadedImages?.Any(x => x != null) == true)
                    {
                        if (uploadedImages.Count > 5 || (autoOglasUBazi.Slike?.Count + uploadedImages.Count > 5))
                        {
                            ViewBag.Greska = "Maksimalno 5 slika po oglasu!";

                            return View("AutoOglasForm", newAutoOglasViewModel);
                        }

                        foreach (var slika in uploadedImages)
                        {
                            Slika novaSlikaZaBazu = PomocnaKlasa.SacuvajSlikuIDodajPutanju(slika, userId, oglasId, punaPutanjaFolderaZaSlikeOglasa);
                            autoOglasUBazi.Slike.Add(novaSlikaZaBazu);
                        }
                    }
                }
                else
                {
                    AutoOglas newAutoOglas = PopuniAutoOglas(newAutoOglasViewModel);

                    dbContext.Oglasi.Add(newAutoOglas);
                    dbContext.SaveChanges();

                    string userId = User.Identity.GetUserId();
                    string oglasId = newAutoOglas.Id.ToString();
                    string punaPutanjaFolderaZaSlikeOglasa = Path.Combine(Server.MapPath(PomocnaKlasa.ImagesFolder), userId, oglasId);

                    Directory.CreateDirectory(punaPutanjaFolderaZaSlikeOglasa);

                    if (uploadedImages?.Any(x => x != null) == true)
                    {
                        foreach (HttpPostedFileBase slika in uploadedImages)
                        {
                            Slika novaSlikaZaBazu = PomocnaKlasa.SacuvajSlikuIDodajPutanju(slika, userId, oglasId, punaPutanjaFolderaZaSlikeOglasa);

                            newAutoOglas.Slike.Add(novaSlikaZaBazu);
                        }
                    }
                }

                dbContext.SaveChanges();

                return RedirectToAction("Index");
            }
        }

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
                .SingleOrDefault(o => o.Id == id);

            return View(autoOglas);
        }

        public ActionResult Delete(int id)
        {
            var autoOglasZaBrisanje = dbContext.Oglasi.Find(id);
            if (autoOglasZaBrisanje == null)
            {
                return HttpNotFound();
            }
            else
            {
                dbContext.Oglasi.Remove(autoOglasZaBrisanje);
                dbContext.SaveChanges();

                Directory.Delete(Server.MapPath(Path.Combine(PomocnaKlasa.ImagesFolder, User.Identity.GetUserId(), id.ToString())), true);
            }
            return RedirectToAction("Index");
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
                var markeAuta = dbContext.MarkeAuta;
                //var modeliAuta = dbContext.ModeliAuta;
                var stanja = dbContext.Stanja;
                var valute = dbContext.Valute;

                AutoOglasViewModel autoOglasViewModel = new AutoOglasViewModel()
                {
                    AutoOglas = stariAutoOglas,
                    MarkeAuta = markeAuta.ToList(),
                    //ModeliAuta = modeliAuta.ToList(),
                    Stanja = stanja.ToList(),
                    Valute = valute.ToList()
                };

                return View("AutoOglasForm", autoOglasViewModel);
            }
        }

        //**********************************************************************************

        [NonAction]
        public AutoOglas PopuniAutoOglas(AutoOglasViewModel autoOglasViewModel, AutoOglas autoOglasZaIzmenu = null)
        {
            if (autoOglasZaIzmenu == null)
            {
                autoOglasZaIzmenu = new AutoOglas();
            }

            autoOglasZaIzmenu.Cena = autoOglasViewModel.AutoOglas.Cena;
            autoOglasZaIzmenu.Godiste = autoOglasViewModel.AutoOglas.Godiste;
            autoOglasZaIzmenu.Kilometraza = autoOglasViewModel.AutoOglas.Kilometraza;
            autoOglasZaIzmenu.KonjskeSnage = autoOglasViewModel.AutoOglas.KonjskeSnage;
            autoOglasZaIzmenu.Kubikaza = autoOglasViewModel.AutoOglas.Kubikaza;
            autoOglasZaIzmenu.MarkaAutaId = autoOglasViewModel.AutoOglas.MarkaAutaId;
            autoOglasZaIzmenu.ModelAutaId = autoOglasViewModel.AutoOglas.ModelAutaId;
            autoOglasZaIzmenu.NaslovOglasa = autoOglasViewModel.AutoOglas.NaslovOglasa;
            autoOglasZaIzmenu.OpisOglasa = autoOglasViewModel.AutoOglas.OpisOglasa;
            autoOglasZaIzmenu.StanjeId = autoOglasViewModel.AutoOglas.StanjeId;
            autoOglasZaIzmenu.ValutaId = autoOglasViewModel.AutoOglas.ValutaId;

            autoOglasZaIzmenu.DatumPostavljanja = autoOglasViewModel.AutoOglas.DatumPostavljanja == default(DateTime)
                ? DateTime.Now
                : autoOglasViewModel.AutoOglas.DatumPostavljanja;

            if (autoOglasZaIzmenu.Slike == null)
            {
                autoOglasZaIzmenu.Slike = new Collection<Slika>();
            }

            autoOglasZaIzmenu.UserAutorOglasaId = autoOglasViewModel.AutoOglas.UserAutorOglasaId ?? User.Identity.GetUserId();

            return autoOglasZaIzmenu;
        }
    }
}