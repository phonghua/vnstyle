namespace VnStyle.Services.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateAspNetClients : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.AspNetClients");
            AlterColumn("dbo.AspNetClients", "Id", c => c.String(nullable: false, maxLength: 128));
            AddPrimaryKey("dbo.AspNetClients", "Id");
        }
        
        public override void Down()
        {
            DropPrimaryKey("dbo.AspNetClients");
            AlterColumn("dbo.AspNetClients", "Id", c => c.String(nullable: false, maxLength: 128));
            AddPrimaryKey("dbo.AspNetClients", "Id");
        }
    }
}
