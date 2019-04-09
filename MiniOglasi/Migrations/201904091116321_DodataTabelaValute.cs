namespace MiniOglasi.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DodataTabelaValute : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Valute",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ImeValute = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Valute");
        }
    }
}
