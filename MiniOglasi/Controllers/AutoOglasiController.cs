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
                .Include(o => o.Slike);

            return View(autoOglasi);
        }

        public ActionResult CreateAutoOglas()
        {
            var markeAuta = dbContext.MarkeAuta.ToList();
            var modeliAuta = dbContext.ModeliAuta.ToList();
            var stanja = dbContext.Stanja.ToList();

            AutoOglasViewModel autoOglasViewModel = new AutoOglasViewModel()
            {
                AutoOglas = new AutoOglas(),
                MarkeAuta = markeAuta,
                ModeliAuta = modeliAuta,
                Stanja = stanja
            };

            return View(autoOglasViewModel);
        }

        [HttpPost]
        public ActionResult CreateAutoOglas(AutoOglasViewModel newAutoOglasViewModel, List<HttpPostedFileBase> uploadedImages = null)
        {
            var markeAuta = dbContext.MarkeAuta.ToList();
            var modeliAuta = dbContext.ModeliAuta.ToList();
            var stanja = dbContext.Stanja.ToList();

            newAutoOglasViewModel.MarkeAuta = markeAuta;
            newAutoOglasViewModel.ModeliAuta = modeliAuta;
            newAutoOglasViewModel.Stanja = stanja;

            if (uploadedImages.Any(x => x != null))
            {
                if (uploadedImages.Count > 5)
                {
                    ViewBag.Greska = "Maksimalno 5 slika po osobi!";

                    return View("CreateAutoOglas", newAutoOglasViewModel);
                }
                foreach (HttpPostedFileBase slika in uploadedImages)
                {
                    if (!PomocnaKlasa.JeLiTacanFormatFajla(slika))
                    {
                        ViewBag.Greska = "Izaberite samo slike!";

                        return View("CreateAutoOglas", newAutoOglasViewModel);
                    }

                    if (slika.ContentLength > 500 * 1024)
                    {
                        ViewBag.Greska = "Svaki fajl mora biti manji od 500kb!";

                        return View("CreateAutoOglas", newAutoOglasViewModel);
                    }
                }
            }

            AutoOglas newAutoOglas = new AutoOglas()
            {
                Cena = newAutoOglasViewModel.AutoOglas.Cena,
                DatumPostavljanja = DateTime.Now,
                Godiste = newAutoOglasViewModel.AutoOglas.Godiste,
                Kilometraza = newAutoOglasViewModel.AutoOglas.Kilometraza,
                KonjskeSnage = newAutoOglasViewModel.AutoOglas.KonjskeSnage,
                Kubikaza = newAutoOglasViewModel.AutoOglas.Kubikaza,
                MarkaAutaId = newAutoOglasViewModel.AutoOglas.MarkaAutaId,
                ModelAutaId = newAutoOglasViewModel.AutoOglas.ModelAutaId,
                NaslovOglasa = newAutoOglasViewModel.AutoOglas.NaslovOglasa,
                OpisOglasa = newAutoOglasViewModel.AutoOglas.OpisOglasa,
                StanjeId = newAutoOglasViewModel.AutoOglas.StanjeId,
                UserAutorOglasaId = User.Identity.GetUserId(),
                Slike = new Collection<Slika>()
            };

            dbContext.Oglasi.Add(newAutoOglas);
            dbContext.SaveChanges();

            string userId = User.Identity.GetUserId();
            string oglasId = newAutoOglas.Id.ToString();
            string punaPutanjaFolderaZaSlikeOglasa = Path.Combine(Server.MapPath(PomocnaKlasa.ImagesFolder), userId, oglasId);

            Directory.CreateDirectory(punaPutanjaFolderaZaSlikeOglasa);

            if (uploadedImages.Any(x => x != null))
            {
                foreach (HttpPostedFileBase slika in uploadedImages)
                {
                    Slika novaSlikaZaBazu = PomocnaKlasa.SacuvajSlikuIDodajPutanju(slika, userId, oglasId, punaPutanjaFolderaZaSlikeOglasa);

                    newAutoOglas.Slike.Add(novaSlikaZaBazu);
                }
            }

            dbContext.SaveChanges();

            return RedirectToAction("Index");
        }

        public ActionResult Details(int id)
        {
            var autoOglas = dbContext.Oglasi
                .OfType<AutoOglas>()
                .Include(o => o.MarkaAuta)
                .Include(o => o.ModelAuta)
                .Include(o => o.Slike)
                .Include(o => o.Stanje)
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
    }
}