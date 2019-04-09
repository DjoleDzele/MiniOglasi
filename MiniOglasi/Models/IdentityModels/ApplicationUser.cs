using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Claims;
using System.Threading.Tasks;

namespace MiniOglasi.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit https://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        //DODATI PROPERTI >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>

        public ICollection<Oglas> MojiOglasi { get; set; }

        public ICollection<OmiljeniOglasiPoKorisniku> OmiljeniOglasi { get; set; }

        [Required]
        [RegularExpression(
            @"0\d{2}-\d{3}-\d{3,4}",
            ErrorMessage = "Pogresan format telefona, unesite 0XX-XXX-XXX(X) ")]
        public string KontaktTelefon { get; set; }

        [Required]
        [ForeignKey("Grad")]
        public int GradId { get; set; }

        public Grad Grad { get; set; }

        //DODATI PROPERTI <<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }
}