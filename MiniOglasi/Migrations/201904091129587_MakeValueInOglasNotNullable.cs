namespace MiniOglasi.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MakeValueInOglasNotNullable : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Oglasi", "ValutaId", "dbo.Valute");
            DropIndex("dbo.Oglasi", new[] { "ValutaId" });
            AlterColumn("dbo.Oglasi", "ValutaId", c => c.Int(nullable: false));
            CreateIndex("dbo.Oglasi", "ValutaId");
            AddForeignKey("dbo.Oglasi", "ValutaId", "dbo.Valute", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Oglasi", "ValutaId", "dbo.Valute");
            DropIndex("dbo.Oglasi", new[] { "ValutaId" });
            AlterColumn("dbo.Oglasi", "ValutaId", c => c.Int());
            CreateIndex("dbo.Oglasi", "ValutaId");
            AddForeignKey("dbo.Oglasi", "ValutaId", "dbo.Valute", "Id");
        }
    }
}
