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
        [ForeignKey("MarkaAuta")]
        public int MarkaAutaId { get; set; }

        public MarkaAuta MarkaAuta { get; set; }

        [Range(0, 1000000)]
        public int Kilometraza { get; set; }

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