namespace VnStyle.Services.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedArticleFeatureImage : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Articles", "FeatureImageId", c => c.Long());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Articles", "FeatureImageId");
        }
    }
}
