namespace MiniOglasi.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class SeedTabelaGrafickeKarte : DbMigration
    {
        public override void Up()
        {
            Sql("SET IDENTITY_INSERT [dbo].[GrafickeKarteModeli] ON " +
            "INSERT INTO [dbo].[GrafickeKarteModeli] ([Id], [MarkaGraficke]) VALUES (1, N'AMD') " +
            "INSERT INTO [dbo].[GrafickeKarteModeli] ([Id], [MarkaGraficke]) VALUES (2, N'INTEL') " +
            "INSERT INTO [dbo].[GrafickeKarteModeli] ([Id], [MarkaGraficke]) VALUES (3, N'NVIDIA') " +
            "SET IDENTITY_INSERT [dbo].[GrafickeKarteModeli] OFF ");
        }

        public override void Down()
        {
            Sql("DELETE FROM GrafickeKarteModeli");
        }
    }
}