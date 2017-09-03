namespace VnStyle.Services.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RenameCategoryArticle : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.ArticleCategories", newName: "Categories");
            AddColumn("dbo.Categories", "RootCategory", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Categories", "RootCategory");
            RenameTable(name: "dbo.Categories", newName: "ArticleCategories");
        }
    }
}
