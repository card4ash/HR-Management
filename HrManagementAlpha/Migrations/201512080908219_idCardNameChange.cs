namespace HrManagementAlpha.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class idCardNameChange : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Persons", "IDCardNo", c => c.String(maxLength: 50));
            DropColumn("dbo.Persons", "NidNo");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Persons", "NidNo", c => c.String(maxLength: 50));
            DropColumn("dbo.Persons", "IDCardNo");
        }
    }
}
