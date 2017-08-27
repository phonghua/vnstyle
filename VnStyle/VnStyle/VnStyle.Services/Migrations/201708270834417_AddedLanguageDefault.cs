namespace VnStyle.Services.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedLanguageDefault : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Languages", "IsDefault", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Languages", "IsDefault");
        }
    }
}
