namespace VnStyle.Services.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Article : DbMigration
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
                        LanguageId = c.Int(nullable: false),
                        MetaTagId = c.Int(nullable: false),
                        ArticleId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Articles", t => t.ArticleId, cascadeDelete: true)
                .ForeignKey("dbo.Languages", t => t.LanguageId, cascadeDelete: true)
                .ForeignKey("dbo.MetaTags", t => t.MetaTagId, cascadeDelete: true)
                .Index(t => t.LanguageId)
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
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Languages",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Code = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.MetaTags",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Description = c.String(),
                        Keywords = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            DropTable("dbo.Posts");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Posts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        Content = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            DropForeignKey("dbo.ArticleLanguages", "MetaTagId", "dbo.MetaTags");
            DropForeignKey("dbo.ArticleLanguages", "LanguageId", "dbo.Languages");
            DropForeignKey("dbo.ArticleLanguages", "ArticleId", "dbo.Articles");
            DropIndex("dbo.ArticleLanguages", new[] { "ArticleId" });
            DropIndex("dbo.ArticleLanguages", new[] { "MetaTagId" });
            DropIndex("dbo.ArticleLanguages", new[] { "LanguageId" });
            DropTable("dbo.MetaTags");
            DropTable("dbo.Languages");
            DropTable("dbo.Articles");
            DropTable("dbo.ArticleLanguages");
        }
    }
}
