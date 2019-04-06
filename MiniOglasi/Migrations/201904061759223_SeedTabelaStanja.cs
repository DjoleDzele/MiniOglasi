namespace MiniOglasi.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class SeedTabelaStanja : DbMigration
    {
        public override void Up()
        {
            Sql("SET IDENTITY_INSERT [dbo].[Stanja] ON " +
            "INSERT INTO [dbo].[Stanja] ([Id], [StanjePredmetaOglasa]) VALUES (1, N'Novo') " +
            "INSERT INTO [dbo].[Stanja] ([Id], [StanjePredmetaOglasa]) VALUES (2, N'Polovno') " +
            "INSERT INTO [dbo].[Stanja] ([Id], [StanjePredmetaOglasa]) VALUES (3, N'Nekorisceno') " +
            "INSERT INTO [dbo].[Stanja] ([Id], [StanjePredmetaOglasa]) VALUES (4, N'Osteceno') " +
            "SET IDENTITY_INSERT [dbo].[Stanja] OFF");
        }

        public override void Down()
        {
            Sql("DELETE FROM Stanja");
        }
    }
}