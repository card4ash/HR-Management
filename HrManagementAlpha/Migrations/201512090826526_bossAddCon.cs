namespace HrManagementAlpha.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class bossAddCon : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Employees", "Boss_EmployeeId", c => c.Int());
            CreateIndex("dbo.Employees", "Boss_EmployeeId");
            AddForeignKey("dbo.Employees", "Boss_EmployeeId", "dbo.Employees", "EmployeeId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Employees", "Boss_EmployeeId", "dbo.Employees");
            DropIndex("dbo.Employees", new[] { "Boss_EmployeeId" });
            DropColumn("dbo.Employees", "Boss_EmployeeId");
        }
    }
}
