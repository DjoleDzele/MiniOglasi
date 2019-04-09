namespace MiniOglasi.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddValuteToOglas : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Oglasi", "ValutaId", c => c.Int());
            CreateIndex("dbo.Oglasi", "ValutaId");
            AddForeignKey("dbo.Oglasi", "ValutaId", "dbo.Valute", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Oglasi", "ValutaId", "dbo.Valute");
            DropIndex("dbo.Oglasi", new[] { "ValutaId" });
            DropColumn("dbo.Oglasi", "ValutaId");
        }
    }
}
