namespace VnStyle.Services.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitDb : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ArticleLanguages",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        HeadLine = c.String(),
                        Content = c.String(),
                        Extract = c.String(),
                        LanguageId = c.String(),
                        MetaTagId = c.Int(nullable: false),
                        ArticleId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Articles", t => t.ArticleId, cascadeDelete: true)
                .ForeignKey("dbo.MetaTags", t => t.MetaTagId, cascadeDelete: true)
                .Index(t => t.MetaTagId)
                .Index(t => t.ArticleId);
            
            CreateTable(
                "dbo.Articles",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CreatedDate = c.DateTime(nullable: false),
                        ModifiedDate = c.DateTime(nullable: false),
                        PublishDate = c.DateTime(nullable: false),
                        CreatedBy = c.Int(nullable: false),
                        State = c.Int(nullable: false),
                        HeadLine = c.String(),
                        FeatureImageId = c.Long(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.RelatedArticles",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Article1Id = c.Int(nullable: false),
                        Article2Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Articles", t => t.Article1Id, cascadeDelete: true)
                .ForeignKey("dbo.Articles", t => t.Article2Id)
                .Index(t => t.Article1Id)
                .Index(t => t.Article2Id);
            
            CreateTable(
                "dbo.MetaTags",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Description = c.String(),
                        Keywords = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
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
                        UserId = c.Int(nullable: false),
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
                        RoleId = c.Int(nullable: false),
                        UserId = c.Int(nullable: false),
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
                        CreatedDate = c.DateTime(nullable: false),
                        ModifiedDate = c.DateTime(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Categories",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Parent = c.Int(),
                        Seq = c.Int(nullable: false),
                        InActive = c.Boolean(nullable: false),
                        RootCategory = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Files",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        MimeType = c.String(),
                        Path = c.String(),
                        SourceTarget = c.Int(nullable: false),
                        IsUsed = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ArticleLanguages", "MetaTagId", "dbo.MetaTags");
            DropForeignKey("dbo.ArticleLanguages", "ArticleId", "dbo.Articles");
            DropForeignKey("dbo.RelatedArticles", "Article2Id", "dbo.Articles");
            DropForeignKey("dbo.RelatedArticles", "Article1Id", "dbo.Articles");
            DropIndex("dbo.RelatedArticles", new[] { "Article2Id" });
            DropIndex("dbo.RelatedArticles", new[] { "Article1Id" });
            DropIndex("dbo.ArticleLanguages", new[] { "ArticleId" });
            DropIndex("dbo.ArticleLanguages", new[] { "MetaTagId" });
            DropTable("dbo.Files");
            DropTable("dbo.Categories");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetUserProfiles");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.AspNetRegisters");
            DropTable("dbo.AspNetRefreshTokens");
            DropTable("dbo.AspNetClients");
            DropTable("dbo.MetaTags");
            DropTable("dbo.RelatedArticles");
            DropTable("dbo.Articles");
            DropTable("dbo.ArticleLanguages");
        }
    }
}
