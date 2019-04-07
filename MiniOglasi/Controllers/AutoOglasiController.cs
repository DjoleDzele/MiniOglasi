using MiniOglasi.Models;
using MiniOglasi.ViewModels;
using System.Collections.Generic;
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

        public ActionResult Index()
        {
            var autoOglasi = dbContext.Oglasi
                .OfType<AutoOglas>()
                .Include(o => o.MarkaAuta);

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
            if (uploadedImages.Any(x => x != null))
            {
                if (uploadedImages.Count > 5)
                {
                    ViewBag.Greska = "Maksimalno 5 slika po osobi!";

                    return View("CreateAutoOglas", newAutoOglasViewModel);
                }
                foreach (HttpPostedFileBase slika in uploadedImages)
                {
                    if (PomocnaKlasa.JeLiFormatFajlaSlika(slika))
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
            if (!ModelState.IsValid)
            {
                return View(newAutoOglasViewModel);
            }
            else
            {
                AutoOglas newAutoOglas = new AutoOglas()
                {
                };

                if (uploadedImages.Any(x => x != null))
                {
                    foreach (HttpPostedFileBase slika in uploadedImages)
                    {
                        Slika novaSlika = new Slika()
                        {
                            ImageMimeType = slika.ContentType,
                            ImageData = new byte[slika.ContentLength]
                        };

                        slika.InputStream.Read(novaSlika.ImageData, 0, slika.ContentLength);

                        newAutoOglas.Slike.Add(novaSlika);
                    }
                }

                dbContext.Oglasi.Add(newAutoOglas);
                dbContext.SaveChanges();

                return RedirectToAction("Index");
            }
        }
    }
}