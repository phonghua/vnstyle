namespace VnStyle.Services.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddCateIntoArticle : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Articles", "CategoryId", c => c.Int());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Articles", "CategoryId");
        }
    }
}
