using MiniOglasi.Models.AutoOglasModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MiniOglasi.Models
{
    [Table("Oglasi")]
    public abstract class Oglas
    {
        public int Id { get; set; }

        [ForeignKey("UserAutorOglasa")]
        public string UserAutorOglasaId { get; set; }

        public ApplicationUser UserAutorOglasa { get; set; }

        [Required]
        [ForeignKey("Stanje")]
        public int StanjeId { get; set; }

        public Stanje Stanje { get; set; }

        [Required]
        [Display(Name = "Naslov oglasa")]
        [StringLength(255)]
        public string NaslovOglasa { get; set; }

        [Display(Name = "Datum Postavljanja")]
        public DateTime DatumPostavljanja { get; set; }

        public ICollection<Slika> Slike { get; set; }

        [Required]
        [Range(0, 99999)]
        public int Cena { get; set; }

        [Required]
        [StringLength(2000)]
        [Display(Name = "Opis oglasa")]
        public string OpisOglasa { get; set; }
    }
}