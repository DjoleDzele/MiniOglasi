namespace MiniOglasi.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class IzbrisanoOglasIdPoljeIzSlike : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.Slike", name: "OglasId", newName: "Oglas_Id");
            RenameIndex(table: "dbo.Slike", name: "IX_OglasId", newName: "IX_Oglas_Id");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.Slike", name: "IX_Oglas_Id", newName: "IX_OglasId");
            RenameColumn(table: "dbo.Slike", name: "Oglas_Id", newName: "OglasId");
        }
    }
}
