namespace MiniOglasi.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class SeedTabelaTipNekretnine : DbMigration
    {
        public override void Up()
        {
            Sql("SET IDENTITY_INSERT [dbo].[TipoviNekretnina] ON " +
            "INSERT INTO [dbo].[TipoviNekretnina] ([Id], [Tip]) VALUES (1, N'Garsonjera') " +
            "INSERT INTO [dbo].[TipoviNekretnina] ([Id], [Tip]) VALUES (2, N'Stan') " +
            "INSERT INTO [dbo].[TipoviNekretnina] ([Id], [Tip]) VALUES (3, N'Sprat') " +
            "INSERT INTO [dbo].[TipoviNekretnina] ([Id], [Tip]) VALUES (4, N'Kuca') " +
            "INSERT INTO [dbo].[TipoviNekretnina] ([Id], [Tip]) VALUES (5, N'Lokal') " +
            "SET IDENTITY_INSERT [dbo].[TipoviNekretnina] OFF ");
        }

        public override void Down()
        {
            Sql("DELETE FROM TipoviNekretnina");
        }
    }
}