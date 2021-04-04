namespace HrManagementAlpha.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddLeave : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.EmployeeLeaves",
                c => new
                    {
                        EmployeeLeaveId = c.Int(nullable: false, identity: true),
                        EmployeeId = c.Int(nullable: false),
                        LeaveTypeId = c.Int(nullable: false),
                        LeaveApplyDate = c.DateTime(nullable: false),
                        LeaveBeginDate = c.DateTime(nullable: false),
                        LeaveEndDate = c.DateTime(nullable: false),
                        NoOfDays = c.Int(nullable: false),
                        LeaveStatusId = c.Int(nullable: false),
                        Purpose = c.String(),
                        RelativeName = c.String(),
                        Relation = c.String(),
                        EmployeeRemarks = c.String(),
                        SupervisorComments = c.String(),
                        AdministrativeNotes = c.String(),
                    })
                .PrimaryKey(t => t.EmployeeLeaveId)
                .ForeignKey("dbo.Employees", t => t.EmployeeId, cascadeDelete: true)
                .Index(t => t.EmployeeId);
            
            CreateTable(
                "dbo.LeaveStatistics",
                c => new
                    {
                        LeaveStatisticsId = c.Int(nullable: false, identity: true),
                        LeaveTypeId = c.Int(nullable: false),
                        EmployeeId = c.Int(nullable: false),
                        ReaminingLeave = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.LeaveStatisticsId)
                .ForeignKey("dbo.Employees", t => t.EmployeeId, cascadeDelete: true)
                .ForeignKey("dbo.LeaveTypes", t => t.LeaveTypeId, cascadeDelete: true)
                .Index(t => t.LeaveTypeId)
                .Index(t => t.EmployeeId);
            
            CreateTable(
                "dbo.LeaveTypes",
                c => new
                    {
                        LeaveTypeId = c.Int(nullable: false, identity: true),
                        LeaveTypeName = c.String(),
                    })
                .PrimaryKey(t => t.LeaveTypeId);
            
            CreateTable(
                "dbo.LeaveStatus",
                c => new
                    {
                        LeaveStatusId = c.Int(nullable: false, identity: true),
                        LeaveStatusName = c.String(),
                    })
                .PrimaryKey(t => t.LeaveStatusId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.LeaveStatistics", "LeaveTypeId", "dbo.LeaveTypes");
            DropForeignKey("dbo.LeaveStatistics", "EmployeeId", "dbo.Employees");
            DropForeignKey("dbo.EmployeeLeaves", "EmployeeId", "dbo.Employees");
            DropIndex("dbo.LeaveStatistics", new[] { "EmployeeId" });
            DropIndex("dbo.LeaveStatistics", new[] { "LeaveTypeId" });
            DropIndex("dbo.EmployeeLeaves", new[] { "EmployeeId" });
            DropTable("dbo.LeaveStatus");
            DropTable("dbo.LeaveTypes");
            DropTable("dbo.LeaveStatistics");
            DropTable("dbo.EmployeeLeaves");
        }
    }
}
