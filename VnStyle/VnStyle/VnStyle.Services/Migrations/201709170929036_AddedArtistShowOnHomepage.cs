namespace VnStyle.Services.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedArtistShowOnHomepage : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Artists", "ShowOnHompage", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Artists", "ShowOnHompage");
        }
    }
}
