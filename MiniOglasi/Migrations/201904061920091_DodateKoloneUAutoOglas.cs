namespace MiniOglasi.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DodateKoloneUAutoOglas : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Oglasi", "KonjskeSnage", c => c.Int());
            AddColumn("dbo.Oglasi", "Kubikaza", c => c.Int());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Oglasi", "Kubikaza");
            DropColumn("dbo.Oglasi", "KonjskeSnage");
        }
    }
}
