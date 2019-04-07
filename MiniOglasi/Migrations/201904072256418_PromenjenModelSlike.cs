namespace MiniOglasi.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PromenjenModelSlike : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.Slike", name: "Oglas_Id", newName: "OglasId");
            RenameIndex(table: "dbo.Slike", name: "IX_Oglas_Id", newName: "IX_OglasId");
            AddColumn("dbo.Slike", "PathDoSlike", c => c.String());
            DropColumn("dbo.Slike", "ImageData");
            DropColumn("dbo.Slike", "ImageMimeType");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Slike", "ImageMimeType", c => c.String());
            AddColumn("dbo.Slike", "ImageData", c => c.Binary());
            DropColumn("dbo.Slike", "PathDoSlike");
            RenameIndex(table: "dbo.Slike", name: "IX_OglasId", newName: "IX_Oglas_Id");
            RenameColumn(table: "dbo.Slike", name: "OglasId", newName: "Oglas_Id");
        }
    }
}
