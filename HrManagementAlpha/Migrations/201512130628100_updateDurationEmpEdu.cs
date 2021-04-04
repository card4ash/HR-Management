namespace HrManagementAlpha.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updateDurationEmpEdu : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.EmployeeEducation", "Duration", c => c.String(maxLength: 50));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.EmployeeEducation", "Duration", c => c.Int());
        }
    }
}
