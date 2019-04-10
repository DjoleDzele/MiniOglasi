using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using MiniOglasi.Models;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace MiniOglasi.Controllers.api
{
    public class PomocneApiMetodeController : ApiController
    {
        private ApplicationDbContext dbContext;

        public PomocneApiMetodeController()
        {
            dbContext = new ApplicationDbContext();
        }

        [HttpDelete]
        [Route("api/delete-user/{korisnikId}")]
        public IHttpActionResult DeleteUser(string korisnikId)
        {
            UserStore<ApplicationUser> userStore = new UserStore<ApplicationUser>(dbContext);
            UserManager<ApplicationUser> userManager = new UserManager<ApplicationUser>(userStore);

            var userZaBrisanje = userManager.FindById(korisnikId);

            if (userZaBrisanje == null)
            {
                return NotFound();
            }
            else
            {
                var logins = userZaBrisanje.Logins;
                var rolesForUser = userManager.GetRoles(korisnikId);

                foreach (var login in logins)
                {
                    userManager.RemoveLogin(korisnikId, new UserLoginInfo(login.LoginProvider, login.ProviderKey));
                }

                if (rolesForUser.Count > 0)
                {
                    foreach (var item in rolesForUser.ToList())
                    {
                        userManager.RemoveFromRole(korisnikId, item);
                    }
                }

                var omiljeniOglasiZaBrisanje = dbContext.OmiljeniOglasiPoKorisniku.Where(x => x.KorisnikKomeJeOglasOmiljenId == korisnikId || x.OmiljeniOglas.UserAutorOglasaId == korisnikId);
                dbContext.OmiljeniOglasiPoKorisniku.RemoveRange(omiljeniOglasiZaBrisanje);

                dbContext.SaveChanges();

                string putanjaZaBrisanjeSlikaFolderKorisnika = HttpContext.Current.Server.MapPath(Path.Combine(PomocnaKlasa.ImagesFolder, korisnikId));

                if (Directory.Exists(putanjaZaBrisanjeSlikaFolderKorisnika))
                {
                    Directory.Delete(HttpContext.Current.Server.MapPath(Path.Combine(PomocnaKlasa.ImagesFolder, korisnikId)), true);
                }

                userManager.Delete(userZaBrisanje);
                return Ok();
            }
        }

        [HttpPost]
        [Route("api/omiljeni-oglas/{id}")]
        public IHttpActionResult AddOmiljeniOglas(int id)
        {
            string ulogovaniKorisnikId = User.Identity.GetUserId();

            var omiljeniOglas = dbContext.OmiljeniOglasiPoKorisniku
                                    .SingleOrDefault(om => om.KorisnikKomeJeOglasOmiljenId == ulogovaniKorisnikId
                                    && om.OmiljeniOglasId == id);
            if (omiljeniOglas == null)
            {
                OmiljeniOglasiPoKorisniku omiljeniOglasNovi = new OmiljeniOglasiPoKorisniku
                {
                    KorisnikKomeJeOglasOmiljenId = ulogovaniKorisnikId,
                    OmiljeniOglasId = id
                };
                dbContext.OmiljeniOglasiPoKorisniku.Add(omiljeniOglasNovi);
                dbContext.SaveChanges();
            }

            return Ok();
        }

        [HttpDelete]
        [Route("api/omiljeni-oglas/{id}")]
        public IHttpActionResult DeleteOmiljeniOglas(int id)
        {
            string ulogovaniKorisnikId = User.Identity.GetUserId();

            var omiljeniOglas = dbContext.OmiljeniOglasiPoKorisniku
                                    .SingleOrDefault(om => om.KorisnikKomeJeOglasOmiljenId == ulogovaniKorisnikId
                                    && om.OmiljeniOglasId == id);
            if (omiljeniOglas == null)
            {
                return NotFound();
            }
            else
            {
                dbContext.OmiljeniOglasiPoKorisniku.Remove(omiljeniOglas);
                dbContext.SaveChanges();
                return Ok();
            }
        }

        [HttpDelete]
        [Route("api/slike/{id}")]
        public IHttpActionResult DeleteSliku(int id)
        {
            var slika = dbContext.Slike.Find(id);
            if (slika != null)
            {
                File.Delete(HttpContext.Current.Server.MapPath(slika.PathDoSlike));

                dbContext.Slike.Remove(slika);
                dbContext.SaveChanges();
                return Ok();
            }
            else
            {
                return NotFound();
            }
        }

        [Route("api/auto-modeli/{id}")]
        public IHttpActionResult GetAutoModelePoProizvodjacu(int id)
        {
            var autoModeli = dbContext.ModeliAuta.Where(mod => mod.MarkaAutaId == id);
            if (autoModeli?.Any() == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(autoModeli.ToList());
            }
        }
    }
}