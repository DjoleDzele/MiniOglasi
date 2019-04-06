namespace MiniOglasi.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class IzbrisanModelAutaTabela : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.ModeliAuta", "MarkaAutaId", "dbo.MarkeAuta");
            DropIndex("dbo.ModeliAuta", new[] { "MarkaAutaId" });
            DropTable("dbo.ModeliAuta");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.ModeliAuta",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Model = c.String(),
                        MarkaAutaId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateIndex("dbo.ModeliAuta", "MarkaAutaId");
            AddForeignKey("dbo.ModeliAuta", "MarkaAutaId", "dbo.MarkeAuta", "Id", cascadeDelete: true);
        }
    }
}
