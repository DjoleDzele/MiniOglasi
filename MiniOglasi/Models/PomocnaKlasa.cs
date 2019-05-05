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

        //public static T PopuniOglas<T>(OglasFormViewModel<T> oglasViewModel, T oglasZaIzmenu)
        //{
        //    if (oglasZaIzmenu == null)
        //    {
        //        oglasZaIzmenu = (T)Activator.CreateInstance(typeof(T));
        //    }
        //    // >> ZAJEDNICKI >>>
        //    Oglas oglasZaIzmenu_Oglas = oglasZaIzmenu as Oglas;
        //    Oglas oglasViewModel_Oglas = oglasViewModel.Oglas as Oglas;

        //    oglasZaIzmenu_Oglas.NaslovOglasa = oglasViewModel_Oglas.NaslovOglasa;
        //    oglasZaIzmenu_Oglas.OpisOglasa = oglasViewModel_Oglas.OpisOglasa;
        //    oglasZaIzmenu_Oglas.Cena = oglasViewModel_Oglas.Cena;
        //    oglasZaIzmenu_Oglas.ValutaId = oglasViewModel_Oglas.ValutaId;
        //    oglasZaIzmenu_Oglas.StanjeId = oglasViewModel_Oglas.StanjeId;

        //    oglasZaIzmenu_Oglas.DatumPostavljanja = oglasViewModel_Oglas.DatumPostavljanja == default(DateTime)
        //        ? DateTime.Now
        //        : oglasViewModel_Oglas.DatumPostavljanja;

        //    if (oglasZaIzmenu_Oglas.Slike == null)
        //    {
        //        oglasZaIzmenu_Oglas.Slike = new Collection<Slika>();
        //    }

        //    oglasZaIzmenu_Oglas.UserAutorOglasaId = oglasViewModel_Oglas.UserAutorOglasaId ?? User.Identity.GetUserId();

        //    // <<< AUTO <<<
        //    if (oglasViewModel._vrstaOglasa == VrstaOglasa.Auto)
        //    {
        //        AutoOglas oglasZaIzmenu_AutoOglas = oglasZaIzmenu as AutoOglas;
        //        AutoOglas oglasViewModel_AutoOglas = oglasViewModel.Oglas as AutoOglas;

        //        oglasZaIzmenu_AutoOglas.Godiste = oglasViewModel_AutoOglas.Godiste;
        //        oglasZaIzmenu_AutoOglas.Kilometraza = oglasViewModel_AutoOglas.Kilometraza;
        //        oglasZaIzmenu_AutoOglas.KonjskeSnage = oglasViewModel_AutoOglas.KonjskeSnage;
        //        oglasZaIzmenu_AutoOglas.Kubikaza = oglasViewModel_AutoOglas.Kubikaza;
        //        oglasZaIzmenu_AutoOglas.MarkaAutaId = oglasViewModel_AutoOglas.MarkaAutaId;
        //        oglasZaIzmenu_AutoOglas.ModelAutaId = oglasViewModel_AutoOglas.ModelAutaId;
        //    }

        //    return oglasZaIzmenu;
        //}

        //public static OglasFormViewModel<T> NapraviOglasFormViewModel<T>(T oglasIzBaze, VrstaOglasa vrstaOglasa, ApplicationDbContext dbContext)
        //{
        //    OglasFormViewModel<T> oglasFormViewModel = new OglasFormViewModel<T>(vrstaOglasa)
        //    {
        //        Oglas = oglasIzBaze == null ? (T)Activator.CreateInstance(typeof(T)) : oglasIzBaze,
        //        Stanja = dbContext.Stanja.ToList(),
        //        Valute = dbContext.Valute.ToList()
        //    };

        //    if (vrstaOglasa == VrstaOglasa.Auto)
        //    {
        //        oglasFormViewModel.MarkeAuta = dbContext.MarkeAuta.ToList();
        //    }
        //    else if (vrstaOglasa == VrstaOglasa.Racunar)
        //    {
        //        oglasFormViewModel.TipoviRacunara = dbContext.TipoviRacunara.ToList();
        //        oglasFormViewModel.MarkeRacunara = dbContext.MarkeRacunara.ToList();
        //        oglasFormViewModel.GrafickaKartaMarke = dbContext.GrafickeKarte.ToList();
        //    }

        //    return oglasFormViewModel;
        //}

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

        public static void DodajSlikeOglasu(string userId, int oglasId, Oglas oglasKomSeDodajuSlike, List<HttpPostedFileBase> uploadedImages)
        {
            string punaPutanjaFolderaZaSlikeOglasa = Path.Combine(HostingEnvironment.MapPath(ImagesFolder), userId, oglasId.ToString());

            Directory.CreateDirectory(punaPutanjaFolderaZaSlikeOglasa);

            if (uploadedImages?.Any(x => x != null) == true)
            {
                foreach (HttpPostedFileBase slika in uploadedImages)
                {
                    Slika novaSlikaZaBazu = SacuvajSlikuIDodajPutanju(slika, userId, oglasId.ToString(), punaPutanjaFolderaZaSlikeOglasa);
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