using MiniOglasi.Models;
using System.Linq;
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