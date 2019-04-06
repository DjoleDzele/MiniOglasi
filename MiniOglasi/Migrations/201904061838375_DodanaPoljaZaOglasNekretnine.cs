namespace MiniOglasi.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DodanaPoljaZaOglasNekretnine : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Oglasi", "Kvadratura", c => c.Int());
            AddColumn("dbo.Oglasi", "BrojSoba", c => c.Int());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Oglasi", "BrojSoba");
            DropColumn("dbo.Oglasi", "Kvadratura");
        }
    }
}
