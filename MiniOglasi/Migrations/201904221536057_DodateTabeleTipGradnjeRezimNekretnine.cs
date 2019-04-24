namespace MiniOglasi.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DodateTabeleTipGradnjeRezimNekretnine : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.RezimOglasNekretnina",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Rezim = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.TipoviGradnje",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Tip = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Oglasi", "TipGradnjeId", c => c.Int());
            AddColumn("dbo.Oglasi", "RezimOglasaNekretnineId", c => c.Int());
            CreateIndex("dbo.Oglasi", "TipGradnjeId");
            CreateIndex("dbo.Oglasi", "RezimOglasaNekretnineId");
            AddForeignKey("dbo.Oglasi", "RezimOglasaNekretnineId", "dbo.RezimOglasNekretnina", "Id");
            AddForeignKey("dbo.Oglasi", "TipGradnjeId", "dbo.TipoviGradnje", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Oglasi", "TipGradnjeId", "dbo.TipoviGradnje");
            DropForeignKey("dbo.Oglasi", "RezimOglasaNekretnineId", "dbo.RezimOglasNekretnina");
            DropIndex("dbo.Oglasi", new[] { "RezimOglasaNekretnineId" });
            DropIndex("dbo.Oglasi", new[] { "TipGradnjeId" });
            DropColumn("dbo.Oglasi", "RezimOglasaNekretnineId");
            DropColumn("dbo.Oglasi", "TipGradnjeId");
            DropTable("dbo.TipoviGradnje");
            DropTable("dbo.RezimOglasNekretnina");
        }
    }
}
