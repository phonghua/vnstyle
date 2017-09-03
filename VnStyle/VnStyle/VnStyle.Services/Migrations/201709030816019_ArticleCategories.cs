namespace VnStyle.Services.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ArticleCategories : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ArticleCategories",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Parent = c.Int(),
                        Seq = c.Int(nullable: false),
                        InActive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.ArticleCategories");
        }
    }
}
