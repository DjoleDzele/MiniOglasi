namespace MiniOglasi.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class SeedTabelaGradovi : DbMigration
    {
        public override void Up()
        {
            Sql("SET IDENTITY_INSERT [dbo].[Gradovi] ON " +
            "INSERT INTO [dbo].[Gradovi] ([Id], [ImeGrada]) VALUES (1, N'Beograd') " +
            "INSERT INTO [dbo].[Gradovi] ([Id], [ImeGrada]) VALUES (2, N'Novi Sad') " +
            "INSERT INTO [dbo].[Gradovi] ([Id], [ImeGrada]) VALUES (3, N'Nis') " +
            "INSERT INTO [dbo].[Gradovi] ([Id], [ImeGrada]) VALUES (4, N'Kragujevac') " +
            "INSERT INTO [dbo].[Gradovi] ([Id], [ImeGrada]) VALUES (5, N'Cacak') " +
            "SET IDENTITY_INSERT [dbo].[Gradovi] OFF ");
        }

        public override void Down()
        {
            Sql("DELETE FROM Gradovi");
        }
    }
}