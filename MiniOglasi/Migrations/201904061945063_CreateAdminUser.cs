namespace MiniOglasi.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class CreateAdminUser : DbMigration
    {
        public override void Up()
        {
            Sql("INSERT INTO [dbo].[AspNetUsers] ([Id], [KontaktTelefon], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName], [GradId]) VALUES (N'bf3cfd6a-c0c3-470d-9ad3-6aa0756389f6', N'000-000-0000', N'admin@admin.com', 0, N'AMWg/+FMR899eSxXNSpqL19hyL/vtA0ghgKn7NFHwOqdpKw2FzoLV5E1tgIsiQoynw==', N'6775fc0d-9a64-463b-bdd6-7c0e0037795c', NULL, 0, 0, NULL, 1, 0, N'admin@admin.com', 1) " +
                "INSERT INTO [dbo].[AspNetRoles] ([Id], [Name]) VALUES (N'31fa2451-dfb1-4ec7-b14d-4c64da095931', N'admin') " +
                "INSERT INTO [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'bf3cfd6a-c0c3-470d-9ad3-6aa0756389f6', N'31fa2451-dfb1-4ec7-b14d-4c64da095931')");
        }

        public override void Down()
        {
            Sql("DELETE FROM AspNetUserRoles WHERE UserId='bf3cfd6a-c0c3-470d-9ad3-6aa0756389f6' " +
                "DELETE FROM AspNetUsers WHERE Id='bf3cfd6a-c0c3-470d-9ad3-6aa0756389f6' " +
                "DELETE FROM AspNetRoles WHERE Id='31fa2451-dfb1-4ec7-b14d-4c64da095931' ");
        }
    }
}