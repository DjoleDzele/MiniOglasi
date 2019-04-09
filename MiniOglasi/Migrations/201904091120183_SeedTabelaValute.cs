namespace MiniOglasi.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class SeedTabelaValute : DbMigration
    {
        public override void Up()
        {
            Sql("SET IDENTITY_INSERT [dbo].[Valute] ON " +
            "INSERT INTO [dbo].[Valute] ([Id], [ImeValute]) VALUES (1, N'Dinar') " +
            "INSERT INTO [dbo].[Valute] ([Id], [ImeValute]) VALUES (2, N'EUR') " +
            "SET IDENTITY_INSERT [dbo].[Valute] OFF ");
        }

        public override void Down()
        {
            Sql("DELETE FROM Valute");
        }
    }
}