namespace HrManagementAlpha.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class edulblUpdate : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.EmployeeEducation", new[] { "EducationLevelId" });
            AlterColumn("dbo.EmployeeEducation", "EducationLevelId", c => c.Int());
            CreateIndex("dbo.EmployeeEducation", "EducationLevelId");
        }
        
        public override void Down()
        {
            DropIndex("dbo.EmployeeEducation", new[] { "EducationLevelId" });
            AlterColumn("dbo.EmployeeEducation", "EducationLevelId", c => c.Int(nullable: false));
            CreateIndex("dbo.EmployeeEducation", "EducationLevelId");
        }
    }
}
