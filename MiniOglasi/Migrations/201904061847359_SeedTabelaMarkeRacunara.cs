namespace MiniOglasi.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class SeedTabelaMarkeRacunara : DbMigration
    {
        public override void Up()
        {
            Sql("SET IDENTITY_INSERT [dbo].[MarkeRacunara] ON " +
            "INSERT INTO [dbo].[MarkeRacunara] ([Id], [Marka]) VALUES (1, N'Lenovo') " +
            "INSERT INTO [dbo].[MarkeRacunara] ([Id], [Marka]) VALUES (2, N'HP') " +
            "INSERT INTO [dbo].[MarkeRacunara] ([Id], [Marka]) VALUES (3, N'Dell') " +
            "INSERT INTO [dbo].[MarkeRacunara] ([Id], [Marka]) VALUES (4, N'Apple') " +
            "INSERT INTO [dbo].[MarkeRacunara] ([Id], [Marka]) VALUES (5, N'Asus') " +
            "INSERT INTO [dbo].[MarkeRacunara] ([Id], [Marka]) VALUES (6, N'Acer') " +
            "SET IDENTITY_INSERT [dbo].[MarkeRacunara] OFF ");
        }

        public override void Down()
        {
            Sql("DELETE FROM MarkeRacunara");
        }
    }
}