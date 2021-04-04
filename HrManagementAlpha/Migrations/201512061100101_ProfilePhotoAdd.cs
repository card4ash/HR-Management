namespace HrManagementAlpha.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ProfilePhotoAdd : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Persons", "ProfilePhoto", c => c.String(maxLength: 150));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Persons", "ProfilePhoto");
        }
    }
}
