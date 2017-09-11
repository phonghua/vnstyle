namespace VnStyle.Services.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DropSettingIsDeleted : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Settings", "IsDeleted");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Settings", "IsDeleted", c => c.Boolean(nullable: false));
        }
    }
}
