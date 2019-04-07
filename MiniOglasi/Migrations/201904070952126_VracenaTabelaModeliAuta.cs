namespace MiniOglasi.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class VracenaTabelaModeliAuta : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ModeliAuta",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        AutoModel = c.String(nullable: false),
                        MarkaAutaId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.MarkeAuta", t => t.MarkaAutaId, cascadeDelete: true)
                .Index(t => t.MarkaAutaId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ModeliAuta", "MarkaAutaId", "dbo.MarkeAuta");
            DropIndex("dbo.ModeliAuta", new[] { "MarkaAutaId" });
            DropTable("dbo.ModeliAuta");
        }
    }
}
