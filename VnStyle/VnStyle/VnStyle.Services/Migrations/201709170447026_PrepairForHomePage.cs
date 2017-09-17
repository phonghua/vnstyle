namespace VnStyle.Services.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PrepairForHomePage : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Artists",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Seq = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.HomePageFeaturedArticles",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ArticleId = c.Int(nullable: false),
                        Seq = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Articles", "Section1", c => c.Boolean(nullable: false));
            AddColumn("dbo.Categories", "FeaturedImageId", c => c.Long());
            AddColumn("dbo.GalleryPhotoes", "ArtistId", c => c.Int(nullable: false));
            DropColumn("dbo.GalleryPhotoes", "CategoryId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.GalleryPhotoes", "CategoryId", c => c.Int());
            DropColumn("dbo.GalleryPhotoes", "ArtistId");
            DropColumn("dbo.Categories", "FeaturedImageId");
            DropColumn("dbo.Articles", "Section1");
            DropTable("dbo.HomePageFeaturedArticles");
            DropTable("dbo.Artists");
        }
    }
}
