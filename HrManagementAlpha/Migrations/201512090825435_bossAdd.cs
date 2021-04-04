namespace HrManagementAlpha.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class bossAdd : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Employees", "BossId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Employees", "BossId", c => c.Int());
        }
    }
}
