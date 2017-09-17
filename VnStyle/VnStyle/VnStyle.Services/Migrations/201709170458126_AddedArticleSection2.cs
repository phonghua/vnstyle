namespace VnStyle.Services.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedArticleSection2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Articles", "Section2", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Articles", "Section2");
        }
    }
}
