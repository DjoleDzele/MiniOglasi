namespace MiniOglasi.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class SeedRezimNekretnine : DbMigration
    {
        public override void Up()
        {
            Sql("SET IDENTITY_INSERT [dbo].[RezimOglasNekretnina] ON " +
            "INSERT INTO [dbo].[RezimOglasNekretnina] ([Id], [Rezim]) VALUES (1, N'Izdavanje') " +
            "INSERT INTO [dbo].[RezimOglasNekretnina] ([Id], [Rezim]) VALUES (2, N'Prodaja') " +
            "SET IDENTITY_INSERT [dbo].[RezimOglasNekretnina] OFF ");
        }

        public override void Down()
        {
            Sql("DELETE FROM RezimOglasNekretnina");
        }
    }
}