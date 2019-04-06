namespace MiniOglasi.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class SeedTabelaTipoviRacunara : DbMigration
    {
        public override void Up()
        {
            Sql("SET IDENTITY_INSERT [dbo].[TipoviRacunara] ON " +
            "INSERT INTO [dbo].[TipoviRacunara] ([Id], [Tip]) VALUES (1, N'Desktop') " +
            "INSERT INTO [dbo].[TipoviRacunara] ([Id], [Tip]) VALUES (2, N'Laptop') " +
            "INSERT INTO [dbo].[TipoviRacunara] ([Id], [Tip]) VALUES (3, N'Tablet') " +
            "SET IDENTITY_INSERT [dbo].[TipoviRacunara] OFF ");
        }

        public override void Down()
        {
            Sql("DELETE FROM TipoviRacunara");
        }
    }
}