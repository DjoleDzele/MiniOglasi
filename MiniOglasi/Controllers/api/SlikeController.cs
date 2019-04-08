using MiniOglasi.Models;
using System.IO;
using System.Web;
using System.Web.Http;

namespace MiniOglasi.Controllers.api
{
    public class SlikeController : ApiController
    {
        private ApplicationDbContext context;

        public SlikeController()
        {
            context = new ApplicationDbContext();
        }

        [HttpDelete]
        public IHttpActionResult DeleteSliku(int id)
        {
            var slika = context.Slike.Find(id);
            if (slika != null)
            {
                File.Delete(HttpContext.Current.Server.MapPath(slika.PathDoSlike));

                context.Slike.Remove(slika);
                context.SaveChanges();
                return Ok();
            }
            else
            {
                return NotFound();
            }
        }
    }
}