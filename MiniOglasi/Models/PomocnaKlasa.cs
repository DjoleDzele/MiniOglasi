using System;
using System.IO;
using System.Web;

namespace MiniOglasi.Models
{
    public static class PomocnaKlasa
    {
        public static string PlaceholderSlika = "~/Images/placeholder-image.png";
        public static string ImagesFolder = "~/Images";

        public static bool JeLiTacanFormatFajla(HttpPostedFileBase SlikaFajl)
        {
            string FajlEkstenzija = Path.GetExtension(SlikaFajl.FileName);
            return FajlEkstenzija == ".png" || FajlEkstenzija == ".jpg";
        }

        public static Slika SacuvajSlikuIDodajPutanju(HttpPostedFileBase SlikaFajl, string userId, string oglasId, string punaPutanja)
        {
            string EkstenzijaSlike = Path.GetExtension(SlikaFajl.FileName);

            string NovoImeZaSliku = $"{Path.GetFileNameWithoutExtension(SlikaFajl.FileName).Replace(" ", "_")}_{DateTime.Now.ToString("yymmddssffff")}{EkstenzijaSlike}";

            string PutanjaSlikeZaBazu = $"~/Images/{userId}/{oglasId}/{NovoImeZaSliku}";

            string PutanjaSlikeZaSnimanje = Path.Combine(punaPutanja, NovoImeZaSliku);
            SlikaFajl.SaveAs(PutanjaSlikeZaSnimanje);

            return new Slika()
            {
                PathDoSlike = PutanjaSlikeZaBazu
            };
        }
    }
}