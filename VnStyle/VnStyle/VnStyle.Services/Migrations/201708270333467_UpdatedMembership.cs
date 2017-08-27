namespace VnStyle.Services.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdatedMembership : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.AspNetUserRoles");
            AddColumn("dbo.AspNetUsers", "CreatedDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.AspNetUsers", "ModifiedDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.AspNetUsers", "IsDeleted", c => c.Boolean(nullable: false));
            AlterColumn("dbo.AspNetUserLogins", "UserId", c => c.Int(nullable: false));
            AlterColumn("dbo.AspNetUserRoles", "RoleId", c => c.Int(nullable: false));
            AlterColumn("dbo.AspNetUserRoles", "UserId", c => c.Int(nullable: false));
            AddPrimaryKey("dbo.AspNetUserRoles", new[] { "RoleId", "UserId" });
        }
        
        public override void Down()
        {
            DropPrimaryKey("dbo.AspNetUserRoles");
            AlterColumn("dbo.AspNetUserRoles", "UserId", c => c.Long(nullable: false));
            AlterColumn("dbo.AspNetUserRoles", "RoleId", c => c.Long(nullable: false));
            AlterColumn("dbo.AspNetUserLogins", "UserId", c => c.Long(nullable: false));
            DropColumn("dbo.AspNetUsers", "IsDeleted");
            DropColumn("dbo.AspNetUsers", "ModifiedDate");
            DropColumn("dbo.AspNetUsers", "CreatedDate");
            AddPrimaryKey("dbo.AspNetUserRoles", new[] { "RoleId", "UserId" });
        }
    }
}
