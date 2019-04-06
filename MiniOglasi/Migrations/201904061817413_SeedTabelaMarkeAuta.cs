namespace MiniOglasi.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class SeedTabelaMarkeAuta : DbMigration
    {
        public override void Up()
        {
            Sql("SET IDENTITY_INSERT [dbo].[MarkeAuta] ON " +
            "INSERT INTO [dbo].[MarkeAuta] ([Id], [Marka]) VALUES (1, N'Audi') " +
            "INSERT INTO [dbo].[MarkeAuta] ([Id], [Marka]) VALUES (2, N'BMW') " +
            "INSERT INTO [dbo].[MarkeAuta] ([Id], [Marka]) VALUES (3, N'Mercedes') " +
            "INSERT INTO [dbo].[MarkeAuta] ([Id], [Marka]) VALUES (4, N'Zastava') " +
            "INSERT INTO [dbo].[MarkeAuta] ([Id], [Marka]) VALUES (5, N'Fiat') " +
            "INSERT INTO [dbo].[MarkeAuta] ([Id], [Marka]) VALUES (6, N'Volkswagen') " +
            "INSERT INTO [dbo].[MarkeAuta] ([Id], [Marka]) VALUES (7, N'Opel') " +
            "INSERT INTO [dbo].[MarkeAuta] ([Id], [Marka]) VALUES (8, N'Lada') " +
            "SET IDENTITY_INSERT [dbo].[MarkeAuta] OFF ");
        }

        public override void Down()
        {
            Sql("DELETE FROM MarkeAuta");
        }
    }
}