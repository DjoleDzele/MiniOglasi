using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Hosting;

namespace MiniOglasi.Models
{
    public static class PomocnaKlasa
    {
        public static string PlaceholderSlika = "~/Images/placeholder-image.png";
        public static string SlikaNaslovnaAuto = "~/Images/slika-naslovna-auto.jpg";
        public static string SlikaNaslovnaRacunar = "~/Images/slika-naslovna-racunar.jpg";
        public static string SlikaNaslovnaStan = "~/Images/slika-naslovna-stan.jpg";
        public static string ImagesFolder = "~/Images";

        public enum TipoviGreskeUploadSlika
        {
            MaxPetSlikaPoOglasu,
            PogresanFormatSlika,
            PrevelikaSlika,
            NemaGreske
        }

        public static TipoviGreskeUploadSlika ProveriValidnostUploadovanihSlika(List<HttpPostedFileBase> uploadedImages)
        {
            if (uploadedImages?.Any(x => x != null) == true)
            {
                if (uploadedImages.Count > 5)
                {
                    return TipoviGreskeUploadSlika.MaxPetSlikaPoOglasu;
                }

                foreach (HttpPostedFileBase slika in uploadedImages)
                {
                    if (!JeLiTacanFormatFajla(slika))
                    {
                        return TipoviGreskeUploadSlika.PogresanFormatSlika;
                    }

                    if (slika.ContentLength > 500 * 1024)
                    {
                        return TipoviGreskeUploadSlika.PrevelikaSlika;
                    }
                }
            }
            return TipoviGreskeUploadSlika.NemaGreske;
        }

        public static void DodajSlikeOglasu(string userId, string oglasId, Oglas oglasKomSeDodajuSlike, List<HttpPostedFileBase> uploadedImages)
        {
            string punaPutanjaFolderaZaSlikeOglasa = Path.Combine(HostingEnvironment.MapPath(ImagesFolder), userId, oglasId);

            Directory.CreateDirectory(punaPutanjaFolderaZaSlikeOglasa);

            if (uploadedImages?.Any(x => x != null) == true)
            {
                foreach (HttpPostedFileBase slika in uploadedImages)
                {
                    Slika novaSlikaZaBazu = SacuvajSlikuIDodajPutanju(slika, userId, oglasId, punaPutanjaFolderaZaSlikeOglasa);
                    oglasKomSeDodajuSlike.Slike.Add(novaSlikaZaBazu);
                }
            }
        }

        public static bool DaLiDodajeViseOdPetSlika(List<HttpPostedFileBase> uploadedImages, Oglas oglasZaProveru)
        {
            return uploadedImages?.Any(x => x != null) == true && (uploadedImages.Count > 5 || (oglasZaProveru.Slike?.Count + uploadedImages.Count > 5));
        }

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