namespace VnStyle.Services.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ArticleRootCate : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Articles", "RootCate", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Articles", "RootCate");
        }
    }
}
