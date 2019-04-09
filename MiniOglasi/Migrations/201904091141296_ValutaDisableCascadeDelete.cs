namespace MiniOglasi.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ValutaDisableCascadeDelete : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Oglasi", "ValutaId", "dbo.Valute");
            AddForeignKey("dbo.Oglasi", "ValutaId", "dbo.Valute", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Oglasi", "ValutaId", "dbo.Valute");
            AddForeignKey("dbo.Oglasi", "ValutaId", "dbo.Valute", "Id", cascadeDelete: true);
        }
    }
}
