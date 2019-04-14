using Microsoft.AspNet.Identity;
using MiniOglasi.Models;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;

namespace MiniOglasi.Controllers
{
    public class KorisnikController : Controller
    {
        private ApplicationDbContext dbContext;

        public KorisnikController()
        {
            dbContext = new ApplicationDbContext();
        }

        public ActionResult MojiOglasi()
        {
            string userId = User.Identity.GetUserId();
            var mojiOglasi = dbContext.Oglasi
                .Where(o => o.UserAutorOglasaId == userId)
                .Include(o => o.Slike)
                .ToList();

            ViewBag.Title = "Moji Oglasi";
            return View("KorisnikoviOglasi", mojiOglasi);
        }

        public ActionResult OmiljeniOglasi()
        {
            string userId = User.Identity.GetUserId();
            var omiljeniOglasi = dbContext.OmiljeniOglasiPoKorisniku
                .Where(om => om.KorisnikKomeJeOglasOmiljenId == userId)
                .Select(om => om.OmiljeniOglas)
                .Include(om => om.Slike)
                .ToList();

            ViewBag.Title = "Omiljeni Oglasi";
            return View("KorisnikoviOglasi", omiljeniOglasi);
        }

        public bool DaLiJeOmiljeniOglas(int idOglasa)
        {
            string userId = User.Identity.GetUserId();
            return dbContext.OmiljeniOglasiPoKorisniku
                .Include(og => og.OmiljeniOglas.Slike)
                .Any(og => og.OmiljeniOglasId == idOglasa && og.KorisnikKomeJeOglasOmiljenId == userId);
        }

        public ActionResult RegulisiKorisnike()
        {
            var korisniciLista = dbContext.Users.ToList();
            return View(korisniciLista);
        }
    }
}