namespace VnStyle.Services.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedArtistImage : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Artists", "ImageId", c => c.Long(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Artists", "ImageId");
        }
    }
}
