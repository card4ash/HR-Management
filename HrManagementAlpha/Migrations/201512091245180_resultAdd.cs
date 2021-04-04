namespace HrManagementAlpha.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class resultAdd : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Results",
                c => new
                    {
                        ResultId = c.Int(nullable: false, identity: true),
                        ResultName = c.String(maxLength: 250),
                    })
                .PrimaryKey(t => t.ResultId);
            
            CreateIndex("dbo.EmployeeEducation", "ResultId");
            AddForeignKey("dbo.EmployeeEducation", "ResultId", "dbo.Results", "ResultId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.EmployeeEducation", "ResultId", "dbo.Results");
            DropIndex("dbo.EmployeeEducation", new[] { "ResultId" });
            DropTable("dbo.Results");
        }
    }
}
