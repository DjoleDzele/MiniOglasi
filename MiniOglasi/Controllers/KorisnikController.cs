using Microsoft.AspNet.Identity;
using MiniOglasi.Models;
using MiniOglasi.ViewModels;
using PagedList;
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

        public ActionResult MojiOglasi(int? page)
        {
            string userId = User.Identity.GetUserId();

            var mojiOglasi = dbContext.Oglasi
                .Where(o => o.UserAutorOglasaId == userId)
                .Include(o => o.Slike)
                .Include(o => o.Valuta)
                .Include(o => o.UserAutorOglasa)
                .ToList();

            ViewBag.Title = "Moji oglasi";

            OglasIndexViewModel mojiOglasiViewModel = new OglasIndexViewModel(VrstaOglasa.OglasiPoKorisniku)
            {
                Oglasi = mojiOglasi.ToPagedList(page ?? 1, 5)
            };

            return View("IndexOglasa", mojiOglasiViewModel);
        }

        public ActionResult OmiljeniOglasi(int? page)
        {
            string userId = User.Identity.GetUserId();

            var omiljeniOglasi = dbContext.OmiljeniOglasiPoKorisniku
                .Where(om => om.KorisnikKomeJeOglasOmiljenId == userId)
                .Select(om => om.OmiljeniOglas)
                .Include(om => om.Slike)
                .Include(om => om.Valuta)
                .Include(o => o.UserAutorOglasa)
                .ToList();

            ViewBag.Title = "Omiljeni oglasi";

            OglasIndexViewModel mojiOglasiViewModel = new OglasIndexViewModel(VrstaOglasa.OglasiPoKorisniku)
            {
                Oglasi = omiljeniOglasi.ToPagedList(page ?? 1, 5)
            };

            return View("IndexOglasa", mojiOglasiViewModel);
        }

        public ActionResult OglasiPoKorisniku(int? page, string korisnikID)
        {
            var user = dbContext.Users.Include(u => u.Grad).SingleOrDefault(u => u.Id == korisnikID);

            var oglasiPoKorisniku = dbContext.Oglasi
                .Include(om => om.Slike)
                .Include(om => om.Valuta)
                .Include(o => o.UserAutorOglasa)
                .Include(o => o.UserAutorOglasa.Grad)
                .Where(x => x.UserAutorOglasaId == korisnikID).ToList();

            ViewBag.Title = "Oglasi korisnika " + user.UserName;
            ViewBag.Telefon = "Telefon: " + user.KontaktTelefon;
            ViewBag.Grad = "Grad: " + user.Grad.ImeGrada;

            OglasIndexViewModel mojiOglasiViewModel = new OglasIndexViewModel(VrstaOglasa.OglasiPoKorisniku)
            {
                Oglasi = oglasiPoKorisniku.ToPagedList(page ?? 1, 5)
            };

            return View("IndexOglasa", mojiOglasiViewModel);
        }

        [AllowAnonymous]
        public bool DaLiJeOmiljeniOglas(int idOglasa)
        {
            string userId = User.Identity.GetUserId();
            return dbContext.OmiljeniOglasiPoKorisniku
                .Include(og => og.OmiljeniOglas.Slike)
                .Any(og => og.OmiljeniOglasId == idOglasa && og.KorisnikKomeJeOglasOmiljenId == userId);
        }

        [HttpGet]
        public ActionResult RegulisiKorisnike(string username = "", string telefon = "", string grad = "")
        {
            var korisniciLista = dbContext.Users
                                    .Include(x => x.Grad);
            if (username != "")
            {
                korisniciLista = korisniciLista.Where(x => x.UserName.Contains(username));
            }

            if (telefon != "")
            {
                korisniciLista = korisniciLista.Where(x => x.KontaktTelefon.Replace("-", "").Contains(telefon));
            }

            if (grad != "")
            {
                korisniciLista = korisniciLista.Where(x => x.Grad.ImeGrada.Contains(grad));
            }

            return View(korisniciLista);
        }
    }
}