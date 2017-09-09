namespace VnStyle.Services.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddGalleryPhoto : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.GalleryPhotoes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FileId = c.Long(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                        CategoryId = c.Int(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.GalleryPhotoes");
        }
    }
}
