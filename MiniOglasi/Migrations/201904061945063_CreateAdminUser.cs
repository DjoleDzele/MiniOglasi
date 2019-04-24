namespace MiniOglasi.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class CreateAdminUser : DbMigration
    {
        public override void Up()
        {
            Sql(
            "INSERT INTO [dbo].[AspNetUsers] ([Id], [KontaktTelefon], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName], [GradId]) VALUES (N'bfb08437-3fad-4ab0-b8c8-70eb1d5b84d0', N'000-000-0000', N'admin@admin.com', 0, N'AOEtdwjxjkiX2+jTrQXPgPato94ylmVpCvhn86Bq1suEdBX9hXU4rB90sRlLs/hosA==', N'435f64e4-63bc-41bc-bdd1-54c2a1398a37', NULL, 0, 0, NULL, 1, 0, N'admin@admin.com', 1) " +
            "INSERT INTO [dbo].[AspNetRoles] ([Id], [Name]) VALUES (N'31fa2451-dfb1-4ec7-b14d-4c64da095931', N'admin') " +
            "INSERT INTO [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'bfb08437-3fad-4ab0-b8c8-70eb1d5b84d0', N'31fa2451-dfb1-4ec7-b14d-4c64da095931')");
        }

        public override void Down()
        {
            Sql(
            "DELETE FROM AspNetUserRoles WHERE UserId='bfb08437-3fad-4ab0-b8c8-70eb1d5b84d0' " +
            "DELETE FROM AspNetUsers WHERE Id='bfb08437-3fad-4ab0-b8c8-70eb1d5b84d0' " +
            "DELETE FROM AspNetRoles WHERE Id='31fa2451-dfb1-4ec7-b14d-4c64da095931' ");
        }
    }
}