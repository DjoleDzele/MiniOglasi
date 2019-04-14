namespace MiniOglasi.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DodataRamMemorijaKolona : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Oglasi", "RamMemorija", c => c.Int());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Oglasi", "RamMemorija");
        }
    }
}
