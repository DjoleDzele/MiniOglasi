namespace MiniOglasi.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class SeedTabelaModeliAuta : DbMigration
    {
        public override void Up()
        {
            Sql("SET IDENTITY_INSERT [dbo].[ModeliAuta] ON " +
            "INSERT INTO [dbo].[ModeliAuta] ([Id], [AutoModel], [MarkaAutaId]) VALUES (1, N'A1', 1) " +
            "INSERT INTO [dbo].[ModeliAuta] ([Id], [AutoModel], [MarkaAutaId]) VALUES (2, N'A2', 1) " +
            "INSERT INTO [dbo].[ModeliAuta] ([Id], [AutoModel], [MarkaAutaId]) VALUES (3, N'A3', 1) " +
            "INSERT INTO [dbo].[ModeliAuta] ([Id], [AutoModel], [MarkaAutaId]) VALUES (4, N'A4', 1) " +
            "INSERT INTO [dbo].[ModeliAuta] ([Id], [AutoModel], [MarkaAutaId]) VALUES (5, N'A5', 1) " +
            "INSERT INTO [dbo].[ModeliAuta] ([Id], [AutoModel], [MarkaAutaId]) VALUES (6, N'A6', 1) " +
            "INSERT INTO [dbo].[ModeliAuta] ([Id], [AutoModel], [MarkaAutaId]) VALUES (7, N'A7', 1) " +
            "INSERT INTO [dbo].[ModeliAuta] ([Id], [AutoModel], [MarkaAutaId]) VALUES (8, N'A8', 1) " +
            "INSERT INTO [dbo].[ModeliAuta] ([Id], [AutoModel], [MarkaAutaId]) VALUES (9, N'318', 2) " +
            "INSERT INTO [dbo].[ModeliAuta] ([Id], [AutoModel], [MarkaAutaId]) VALUES (10, N'320', 2) " +
            "INSERT INTO [dbo].[ModeliAuta] ([Id], [AutoModel], [MarkaAutaId]) VALUES (11, N'323', 2) " +
            "INSERT INTO [dbo].[ModeliAuta] ([Id], [AutoModel], [MarkaAutaId]) VALUES (12, N'324', 2) " +
            "INSERT INTO [dbo].[ModeliAuta] ([Id], [AutoModel], [MarkaAutaId]) VALUES (13, N'325', 2) " +
            "INSERT INTO [dbo].[ModeliAuta] ([Id], [AutoModel], [MarkaAutaId]) VALUES (14, N'518', 2) " +
            "INSERT INTO [dbo].[ModeliAuta] ([Id], [AutoModel], [MarkaAutaId]) VALUES (15, N'520', 2) " +
            "INSERT INTO [dbo].[ModeliAuta] ([Id], [AutoModel], [MarkaAutaId]) VALUES (16, N'545', 2) " +
            "INSERT INTO [dbo].[ModeliAuta] ([Id], [AutoModel], [MarkaAutaId]) VALUES (17, N'730', 2) " +
            "INSERT INTO [dbo].[ModeliAuta] ([Id], [AutoModel], [MarkaAutaId]) VALUES (18, N'A 140', 3) " +
            "INSERT INTO [dbo].[ModeliAuta] ([Id], [AutoModel], [MarkaAutaId]) VALUES (19, N'A 150', 3) " +
            "INSERT INTO [dbo].[ModeliAuta] ([Id], [AutoModel], [MarkaAutaId]) VALUES (20, N'A 160', 3) " +
            "INSERT INTO [dbo].[ModeliAuta] ([Id], [AutoModel], [MarkaAutaId]) VALUES (21, N'A 170', 3) " +
            "INSERT INTO [dbo].[ModeliAuta] ([Id], [AutoModel], [MarkaAutaId]) VALUES (22, N'A 180', 3) " +
            "INSERT INTO [dbo].[ModeliAuta] ([Id], [AutoModel], [MarkaAutaId]) VALUES (23, N'A 190', 3) " +
            "INSERT INTO [dbo].[ModeliAuta] ([Id], [AutoModel], [MarkaAutaId]) VALUES (24, N'A 200', 3) " +
            "INSERT INTO [dbo].[ModeliAuta] ([Id], [AutoModel], [MarkaAutaId]) VALUES (25, N'A 210', 3) " +
            "INSERT INTO [dbo].[ModeliAuta] ([Id], [AutoModel], [MarkaAutaId]) VALUES (26, N'YUGO 45', 4) " +
            "INSERT INTO [dbo].[ModeliAuta] ([Id], [AutoModel], [MarkaAutaId]) VALUES (27, N'YUGO 55', 4) " +
            "INSERT INTO [dbo].[ModeliAuta] ([Id], [AutoModel], [MarkaAutaId]) VALUES (28, N'YUGO 65', 4) " +
            "INSERT INTO [dbo].[ModeliAuta] ([Id], [AutoModel], [MarkaAutaId]) VALUES (29, N'YUGO Ciao', 4) " +
            "INSERT INTO [dbo].[ModeliAuta] ([Id], [AutoModel], [MarkaAutaId]) VALUES (30, N'Skala 55', 4) " +
            "INSERT INTO [dbo].[ModeliAuta] ([Id], [AutoModel], [MarkaAutaId]) VALUES (31, N'Florida', 4) " +
            "INSERT INTO [dbo].[ModeliAuta] ([Id], [AutoModel], [MarkaAutaId]) VALUES (32, N'101', 4) " +
            "INSERT INTO [dbo].[ModeliAuta] ([Id], [AutoModel], [MarkaAutaId]) VALUES (33, N'128', 4) " +
            "INSERT INTO [dbo].[ModeliAuta] ([Id], [AutoModel], [MarkaAutaId]) VALUES (34, N'Koral', 4) " +
            "INSERT INTO [dbo].[ModeliAuta] ([Id], [AutoModel], [MarkaAutaId]) VALUES (35, N'500C', 5) " +
            "INSERT INTO [dbo].[ModeliAuta] ([Id], [AutoModel], [MarkaAutaId]) VALUES (36, N'500L', 5) " +
            "INSERT INTO [dbo].[ModeliAuta] ([Id], [AutoModel], [MarkaAutaId]) VALUES (37, N'500X', 5) " +
            "INSERT INTO [dbo].[ModeliAuta] ([Id], [AutoModel], [MarkaAutaId]) VALUES (38, N'Bravo', 5) " +
            "INSERT INTO [dbo].[ModeliAuta] ([Id], [AutoModel], [MarkaAutaId]) VALUES (39, N'Coupe', 5) " +
            "INSERT INTO [dbo].[ModeliAuta] ([Id], [AutoModel], [MarkaAutaId]) VALUES (40, N'Panda', 5) " +
            "INSERT INTO [dbo].[ModeliAuta] ([Id], [AutoModel], [MarkaAutaId]) VALUES (42, N'Golf 1', 6) " +
            "INSERT INTO [dbo].[ModeliAuta] ([Id], [AutoModel], [MarkaAutaId]) VALUES (43, N'Golf 2', 6) " +
            "INSERT INTO [dbo].[ModeliAuta] ([Id], [AutoModel], [MarkaAutaId]) VALUES (44, N'Golf 3', 6) " +
            "INSERT INTO [dbo].[ModeliAuta] ([Id], [AutoModel], [MarkaAutaId]) VALUES (45, N'Golf 4', 6) " +
            "INSERT INTO [dbo].[ModeliAuta] ([Id], [AutoModel], [MarkaAutaId]) VALUES (46, N'Golf 5', 6) " +
            "INSERT INTO [dbo].[ModeliAuta] ([Id], [AutoModel], [MarkaAutaId]) VALUES (47, N'Golf 6', 6) " +
            "INSERT INTO [dbo].[ModeliAuta] ([Id], [AutoModel], [MarkaAutaId]) VALUES (48, N'Jetta', 6) " +
            "INSERT INTO [dbo].[ModeliAuta] ([Id], [AutoModel], [MarkaAutaId]) VALUES (49, N'Passat', 6) " +
            "INSERT INTO [dbo].[ModeliAuta] ([Id], [AutoModel], [MarkaAutaId]) VALUES (50, N'Polo', 6) " +
            "INSERT INTO [dbo].[ModeliAuta] ([Id], [AutoModel], [MarkaAutaId]) VALUES (51, N'Touareg', 6) " +
            "INSERT INTO [dbo].[ModeliAuta] ([Id], [AutoModel], [MarkaAutaId]) VALUES (52, N'Ascona', 7) " +
            "INSERT INTO [dbo].[ModeliAuta] ([Id], [AutoModel], [MarkaAutaId]) VALUES (53, N'Kadet', 7) " +
            "INSERT INTO [dbo].[ModeliAuta] ([Id], [AutoModel], [MarkaAutaId]) VALUES (54, N'Astra', 7) " +
            "INSERT INTO [dbo].[ModeliAuta] ([Id], [AutoModel], [MarkaAutaId]) VALUES (55, N'Corsa', 7) " +
            "INSERT INTO [dbo].[ModeliAuta] ([Id], [AutoModel], [MarkaAutaId]) VALUES (56, N'GT', 7) " +
            "INSERT INTO [dbo].[ModeliAuta] ([Id], [AutoModel], [MarkaAutaId]) VALUES (57, N'Vectra', 7) " +
            "INSERT INTO [dbo].[ModeliAuta] ([Id], [AutoModel], [MarkaAutaId]) VALUES (58, N'Rekord', 7) " +
            "INSERT INTO [dbo].[ModeliAuta] ([Id], [AutoModel], [MarkaAutaId]) VALUES (59, N'Niva', 8) " +
            "INSERT INTO [dbo].[ModeliAuta] ([Id], [AutoModel], [MarkaAutaId]) VALUES (60, N'Samara', 8) " +
            "INSERT INTO [dbo].[ModeliAuta] ([Id], [AutoModel], [MarkaAutaId]) VALUES (61, N'Aleko', 8) " +
            "INSERT INTO [dbo].[ModeliAuta] ([Id], [AutoModel], [MarkaAutaId]) VALUES (62, N'1300', 8) " +
            "INSERT INTO [dbo].[ModeliAuta] ([Id], [AutoModel], [MarkaAutaId]) VALUES (63, N'Riva', 8) " +
            "INSERT INTO [dbo].[ModeliAuta] ([Id], [AutoModel], [MarkaAutaId]) VALUES (64, N'Vesta', 8) " +
            "INSERT INTO [dbo].[ModeliAuta] ([Id], [AutoModel], [MarkaAutaId]) VALUES (65, N'1500', 8) " +
            "INSERT INTO [dbo].[ModeliAuta] ([Id], [AutoModel], [MarkaAutaId]) VALUES (66, N'Kalina', 8) " +
            "SET IDENTITY_INSERT [dbo].[ModeliAuta] OFF ");
        }

        public override void Down()
        {
            Sql("DELETE FROM ModeliAuta");
        }
    }
}