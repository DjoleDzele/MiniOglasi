using MiniOglasi.Models.AutoOglasModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MiniOglasi.Models
{
    public class AutoOglas : Oglas, IValidatableObject
    {
        [Required]
        [Display(Name = "Marka auta")]
        [ForeignKey("MarkaAuta")]
        public int MarkaAutaId { get; set; }

        public MarkaAuta MarkaAuta { get; set; }

        [Required]
        [Display(Name = "Model auta")]
        [ForeignKey("ModelAuta")]
        public int ModelAutaId { get; set; }

        public ModelAuta ModelAuta { get; set; }

        [Range(0, 1000000)]
        public int Kilometraza { get; set; }

        [Range(0, 1000)]
        [Display(Name = "Konjskih snaga")]
        public int KonjskeSnage { get; set; }

        [Range(100, 10000)]
        public int Kubikaza { get; set; }

        public int Godiste { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (Godiste < 1900 || Godiste > DateTime.Now.Year)
            {
                yield return new ValidationResult("Unesite validno godiste");
            }
        }
    }
}