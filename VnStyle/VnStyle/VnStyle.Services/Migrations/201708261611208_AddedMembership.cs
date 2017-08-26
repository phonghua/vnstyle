namespace VnStyle.Services.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedMembership : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AspNetClients",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Secret = c.String(),
                        Name = c.String(),
                        ApplicationType = c.Int(nullable: false),
                        Active = c.Boolean(nullable: false),
                        RefreshTokenLifeTime = c.Int(nullable: false),
                        AllowedOrigin = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.AspNetRefreshTokens",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TokenId = c.String(),
                        Subject = c.String(),
                        ClientId = c.String(),
                        IssuedUtc = c.DateTime(nullable: false),
                        ExpiresUtc = c.DateTime(nullable: false),
                        ProtectedTicket = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.AspNetRegisters",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Email = c.String(),
                        Token = c.String(),
                        RegisteredDate = c.DateTime(nullable: false),
                        ExpiredDate = c.DateTime(nullable: false),
                        SignupTimes = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.Long(nullable: false),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.Long(nullable: false),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey });
            
            CreateTable(
                "dbo.AspNetUserProfiles",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        DisplayName = c.String(),
                        FirstName = c.String(),
                        LastName = c.String(),
                        Skype = c.String(),
                        Facebook = c.String(),
                        DateOfBirth = c.DateTime(),
                        IdentityPassport = c.String(),
                        Nationality = c.Long(),
                        MaritalStatus = c.Int(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        RoleId = c.Long(nullable: false),
                        UserId = c.Long(nullable: false),
                    })
                .PrimaryKey(t => new { t.RoleId, t.UserId });
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Email = c.String(),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDate = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(),
                        UserProfileId = c.Long(nullable: false),
                        IsExternal = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetUserProfiles");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.AspNetRegisters");
            DropTable("dbo.AspNetRefreshTokens");
            DropTable("dbo.AspNetClients");
        }
    }
}
