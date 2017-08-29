namespace VnStyle.Services.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemovedLanguage : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.ArticleLanguages", "LanguageId", "dbo.Languages");
            DropIndex("dbo.ArticleLanguages", new[] { "LanguageId" });
            AlterColumn("dbo.ArticleLanguages", "LanguageId", c => c.String());
            DropTable("dbo.Languages");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Languages",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Code = c.String(),
                        IsDefault = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            AlterColumn("dbo.ArticleLanguages", "LanguageId", c => c.Int(nullable: false));
            CreateIndex("dbo.ArticleLanguages", "LanguageId");
            AddForeignKey("dbo.ArticleLanguages", "LanguageId", "dbo.Languages", "Id", cascadeDelete: true);
        }
    }
}
