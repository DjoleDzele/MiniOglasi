namespace MiniOglasi.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class StanjeNijeRequiredVise : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.Oglasi", new[] { "StanjeId" });
            AlterColumn("dbo.Oglasi", "StanjeId", c => c.Int());
            CreateIndex("dbo.Oglasi", "StanjeId");
        }
        
        public override void Down()
        {
            DropIndex("dbo.Oglasi", new[] { "StanjeId" });
            AlterColumn("dbo.Oglasi", "StanjeId", c => c.Int(nullable: false));
            CreateIndex("dbo.Oglasi", "StanjeId");
        }
    }
}
