namespace MiniOglasi.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DodaniOstaliModeli : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Gradovi",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ImeGrada = c.String(nullable: false, maxLength: 100),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Oglasi",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserAutorOglasaId = c.String(nullable: false, maxLength: 128),
                        NaslovOglasa = c.String(nullable: false, maxLength: 255),
                        DatumPostavljanja = c.DateTime(nullable: false),
                        Cena = c.Int(nullable: false),
                        OpisOglasa = c.String(nullable: false, maxLength: 2000),
                        StanjeId = c.Int(nullable: false),
                        TipNekretnineId = c.Int(),
                        GradId = c.Int(),
                        Internet = c.Boolean(),
                        Grejanje = c.Boolean(),
                        Kablovska = c.Boolean(),
                        Terasa = c.Boolean(),
                        MarkaAutaId = c.Int(),
                        Kilometraza = c.Int(),
                        Godiste = c.Int(),
                        MarkaRacunaraId = c.Int(),
                        TipRacunaraId = c.Int(),
                        HddMemorija = c.Int(),
                        ProcesorBrzina = c.Int(),
                        ProcesorJezgara = c.Int(),
                        GrafickaMemorija = c.Int(),
                        MarkaGrafickeKarteId = c.Int(),
                        Monitor = c.Boolean(),
                        Tastatura = c.Boolean(),
                        Mis = c.Boolean(),
                        Discriminator = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Gradovi", t => t.GradId)
                .ForeignKey("dbo.Stanja", t => t.StanjeId)
                .ForeignKey("dbo.AspNetUsers", t => t.UserAutorOglasaId, cascadeDelete: true)
                .ForeignKey("dbo.MarkeAuta", t => t.MarkaAutaId)
                .ForeignKey("dbo.GrafickeKarteModeli", t => t.MarkaGrafickeKarteId)
                .ForeignKey("dbo.MarkeRacunara", t => t.MarkaRacunaraId)
                .ForeignKey("dbo.TipoviRacunara", t => t.TipRacunaraId)
                .ForeignKey("dbo.TipoviNekretnina", t => t.TipNekretnineId)
                .Index(t => t.UserAutorOglasaId)
                .Index(t => t.StanjeId)
                .Index(t => t.TipNekretnineId)
                .Index(t => t.GradId)
                .Index(t => t.MarkaAutaId)
                .Index(t => t.MarkaRacunaraId)
                .Index(t => t.TipRacunaraId)
                .Index(t => t.MarkaGrafickeKarteId);
            
            CreateTable(
                "dbo.Slike",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ImageData = c.Binary(),
                        ImageMimeType = c.String(),
                        OglasId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Oglasi", t => t.OglasId, cascadeDelete: true)
                .Index(t => t.OglasId);
            
            CreateTable(
                "dbo.Stanja",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        StanjePredmetaOglasa = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.MarkeAuta",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Marka = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ModeliAuta",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Model = c.String(),
                        MarkaAutaId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.MarkeAuta", t => t.MarkaAutaId, cascadeDelete: true)
                .Index(t => t.MarkaAutaId);
            
            CreateTable(
                "dbo.GrafickeKarteModeli",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        MarkaGraficke = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.MarkeRacunara",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Marka = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.TipoviRacunara",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Tip = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.TipoviNekretnina",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Tip = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.OmiljeniOglasiPoKorisniku",
                c => new
                    {
                        KorisnikKomeJeOglasOmiljenId = c.String(nullable: false, maxLength: 128),
                        OmiljeniOglasId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.KorisnikKomeJeOglasOmiljenId, t.OmiljeniOglasId })
                .ForeignKey("dbo.AspNetUsers", t => t.KorisnikKomeJeOglasOmiljenId, cascadeDelete: true)
                .ForeignKey("dbo.Oglasi", t => t.OmiljeniOglasId)
                .Index(t => t.KorisnikKomeJeOglasOmiljenId)
                .Index(t => t.OmiljeniOglasId);
            
            AddColumn("dbo.AspNetUsers", "GradId", c => c.Int(nullable: false));
            CreateIndex("dbo.AspNetUsers", "GradId");
            AddForeignKey("dbo.AspNetUsers", "GradId", "dbo.Gradovi", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.OmiljeniOglasiPoKorisniku", "OmiljeniOglasId", "dbo.Oglasi");
            DropForeignKey("dbo.OmiljeniOglasiPoKorisniku", "KorisnikKomeJeOglasOmiljenId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Oglasi", "TipNekretnineId", "dbo.TipoviNekretnina");
            DropForeignKey("dbo.Oglasi", "TipRacunaraId", "dbo.TipoviRacunara");
            DropForeignKey("dbo.Oglasi", "MarkaRacunaraId", "dbo.MarkeRacunara");
            DropForeignKey("dbo.Oglasi", "MarkaGrafickeKarteId", "dbo.GrafickeKarteModeli");
            DropForeignKey("dbo.Oglasi", "MarkaAutaId", "dbo.MarkeAuta");
            DropForeignKey("dbo.ModeliAuta", "MarkaAutaId", "dbo.MarkeAuta");
            DropForeignKey("dbo.Oglasi", "UserAutorOglasaId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUsers", "GradId", "dbo.Gradovi");
            DropForeignKey("dbo.Oglasi", "StanjeId", "dbo.Stanja");
            DropForeignKey("dbo.Slike", "OglasId", "dbo.Oglasi");
            DropForeignKey("dbo.Oglasi", "GradId", "dbo.Gradovi");
            DropIndex("dbo.OmiljeniOglasiPoKorisniku", new[] { "OmiljeniOglasId" });
            DropIndex("dbo.OmiljeniOglasiPoKorisniku", new[] { "KorisnikKomeJeOglasOmiljenId" });
            DropIndex("dbo.ModeliAuta", new[] { "MarkaAutaId" });
            DropIndex("dbo.AspNetUsers", new[] { "GradId" });
            DropIndex("dbo.Slike", new[] { "OglasId" });
            DropIndex("dbo.Oglasi", new[] { "MarkaGrafickeKarteId" });
            DropIndex("dbo.Oglasi", new[] { "TipRacunaraId" });
            DropIndex("dbo.Oglasi", new[] { "MarkaRacunaraId" });
            DropIndex("dbo.Oglasi", new[] { "MarkaAutaId" });
            DropIndex("dbo.Oglasi", new[] { "GradId" });
            DropIndex("dbo.Oglasi", new[] { "TipNekretnineId" });
            DropIndex("dbo.Oglasi", new[] { "StanjeId" });
            DropIndex("dbo.Oglasi", new[] { "UserAutorOglasaId" });
            DropColumn("dbo.AspNetUsers", "GradId");
            DropTable("dbo.OmiljeniOglasiPoKorisniku");
            DropTable("dbo.TipoviNekretnina");
            DropTable("dbo.TipoviRacunara");
            DropTable("dbo.MarkeRacunara");
            DropTable("dbo.GrafickeKarteModeli");
            DropTable("dbo.ModeliAuta");
            DropTable("dbo.MarkeAuta");
            DropTable("dbo.Stanja");
            DropTable("dbo.Slike");
            DropTable("dbo.Oglasi");
            DropTable("dbo.Gradovi");
        }
    }
}
