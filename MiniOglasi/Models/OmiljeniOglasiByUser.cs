using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MiniOglasi.Models
{
    [Table("OmiljeniOglasiPoKorisniku")]
    public class OmiljeniOglasiPoKorisniku
    {
        [Key]
        [Column(Order = 1)]
        [ForeignKey("KorisnikKomeJeOglasOmiljen")]
        public string KorisnikKomeJeOglasOmiljenId { get; set; }

        public ApplicationUser KorisnikKomeJeOglasOmiljen { get; set; }

        [Key]
        [Column(Order = 2)]
        [ForeignKey("OmiljeniOglas")]
        public int OmiljeniOglasId { get; set; }

        public Oglas OmiljeniOglas { get; set; }
    }
}