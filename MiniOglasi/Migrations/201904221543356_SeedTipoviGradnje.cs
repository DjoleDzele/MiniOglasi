namespace MiniOglasi.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class SeedTipoviGradnje : DbMigration
    {
        public override void Up()
        {
            Sql("SET IDENTITY_INSERT [dbo].[TipoviGradnje] ON " +
            "INSERT INTO [dbo].[TipoviGradnje] ([Id], [Tip]) VALUES (1, N'Stara') " +
            "INSERT INTO [dbo].[TipoviGradnje] ([Id], [Tip]) VALUES (2, N'Nova') " +
            "SET IDENTITY_INSERT [dbo].[TipoviGradnje] OFF ");
        }

        public override void Down()
        {
            Sql("DELETE FROM TipoviGradnje");
        }
    }
}