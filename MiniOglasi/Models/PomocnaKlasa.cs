using System.IO;
using System.Web;

namespace MiniOglasi.Models
{
    public static class PomocnaKlasa
    {
        public static bool JeLiFormatFajlaSlika(HttpPostedFileBase SlikaFajl)
        {
            string FajlEkstenzija = Path.GetExtension(SlikaFajl.FileName);
            return FajlEkstenzija != ".png" && FajlEkstenzija != ".jpg";
        }
    }
}