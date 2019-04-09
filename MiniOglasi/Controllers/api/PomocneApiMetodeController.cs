using Microsoft.AspNet.Identity;
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