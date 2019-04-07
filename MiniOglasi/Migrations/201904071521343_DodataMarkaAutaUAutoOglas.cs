namespace MiniOglasi.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DodataMarkaAutaUAutoOglas : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Oglasi", "ModelAutaId", c => c.Int());
            CreateIndex("dbo.Oglasi", "ModelAutaId");
            AddForeignKey("dbo.Oglasi", "ModelAutaId", "dbo.ModeliAuta", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Oglasi", "ModelAutaId", "dbo.ModeliAuta");
            DropIndex("dbo.Oglasi", new[] { "ModelAutaId" });
            DropColumn("dbo.Oglasi", "ModelAutaId");
        }
    }
}
