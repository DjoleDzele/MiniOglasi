using MiniOglasi.Models.NekretninaOglasModels;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MiniOglasi.Models
{
    [Table("Gradovi")]
    public class Grad
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string ImeGrada { get; set; }

        public ICollection<ApplicationUser> Users { get; set; }

        public ICollection<NekretninaOglas> Nekretnine { get; set; }
    }
}