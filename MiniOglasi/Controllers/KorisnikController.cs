using Microsoft.AspNet.Identity;
using MiniOglasi.Models;
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
            return View();
        }

        public ActionResult OmiljeniOglasi()
        {
            return View();
        }

        public ActionResult DaLiJeOmiljeniOglas(int idOglasa)
        {
            string userId = User.Identity.GetUserId();
            bool omiljeniOglas = dbContext.OmiljeniOglasiPoKorisniku
                                    .Any(og => og.OmiljeniOglasId == idOglasa
                                    && og.KorisnikKomeJeOglasOmiljenId == userId);

            return Content(omiljeniOglas.ToString());
        }
    }
}