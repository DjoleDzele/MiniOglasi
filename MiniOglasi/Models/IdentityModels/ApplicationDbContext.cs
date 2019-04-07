using Microsoft.AspNet.Identity.EntityFramework;
using MiniOglasi.Models.AutoOglasModels;
using MiniOglasi.Models.NekretninaOglasModels;
using MiniOglasi.Models.RacunarOglasModels;
using System.Data.Entity;

namespace MiniOglasi.Models
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public DbSet<Grad> Gradovi { get; set; }
        public DbSet<Oglas> Oglasi { get; set; }
        public DbSet<OmiljeniOglasiPoKorisniku> OmiljeniOglasiPoKorisniku { get; set; }
        public DbSet<Slika> Slike { get; set; }
        public DbSet<Stanje> Stanja { get; set; }
        public DbSet<MarkaAuta> MarkeAuta { get; set; }
        public DbSet<ModelAuta> ModeliAuta { get; set; }
        public DbSet<TipNekretnine> TipoviNekretnina { get; set; }
        public DbSet<GrafickaKartaMarka> GrafickeKarte { get; set; }
        public DbSet<MarkaRacunara> MarkeRacunara { get; set; }
        public DbSet<TipRacunara> TipoviRacunara { get; set; }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<OmiljeniOglasiPoKorisniku>()
                .HasRequired(om => om.OmiljeniOglas)
                .WithMany()
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ApplicationUser>()
                .HasMany(u => u.MojiOglasi)
                .WithRequired(o => o.UserAutorOglasa)
                .HasForeignKey(o => o.UserAutorOglasaId)
                .WillCascadeOnDelete(true);

            modelBuilder.Entity<ApplicationUser>()
                .HasRequired(u => u.Grad)
                .WithMany(g => g.Users)
                .HasForeignKey(u => u.GradId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<NekretninaOglas>()
                .HasRequired(n => n.Grad)
                .WithMany(g => g.Nekretnine)
                .HasForeignKey(n => n.GradId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<NekretninaOglas>()
                .HasRequired(n => n.TipNekretnine)
                .WithMany(t => t.NekretninaOglasi)
                .HasForeignKey(n => n.TipNekretnineId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Oglas>()
                .HasMany(o => o.Slike)
                .WithRequired(s => s.Oglas)
                .WillCascadeOnDelete(true);

            modelBuilder.Entity<Oglas>()
                .HasRequired(o => o.Stanje)
                .WithMany(s => s.Oglasi)
                .HasForeignKey(o => o.StanjeId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<AutoOglas>()
                .HasRequired(a => a.MarkaAuta)
                .WithMany(m => m.OglasiAuto)
                .HasForeignKey(a => a.MarkaAutaId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<AutoOglas>()
                .HasRequired(a => a.ModelAuta)
                .WithMany()
                .HasForeignKey(a => a.ModelAutaId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<MarkaAuta>()
                .HasMany(mar => mar.ModeliAuta)
                .WithRequired(mod => mod.MarkaAuta)
                .HasForeignKey(mod => mod.MarkaAutaId)
                .WillCascadeOnDelete(true);

            modelBuilder.Entity<RacunarOglas>()
                .HasOptional(r => r.MarkaRacunara)
                .WithMany(m => m.Racunari)
                .HasForeignKey(r => r.MarkaRacunaraId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<RacunarOglas>()
                .HasOptional(r => r.TipRacunara)
                .WithMany(t => t.Racunari)
                .HasForeignKey(r => r.TipRacunaraId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<RacunarOglas>()
                .HasOptional(r => r.MarkaGrafickeKarte)
                .WithMany(m => m.Racunari)
                .HasForeignKey(r => r.MarkaGrafickeKarteId)
                .WillCascadeOnDelete(false);

            base.OnModelCreating(modelBuilder);
        }
    }
}