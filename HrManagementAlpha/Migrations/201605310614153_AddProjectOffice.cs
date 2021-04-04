namespace HrManagementAlpha.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddProjectOffice : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Offices",
                c => new
                    {
                        OfficeId = c.Int(nullable: false, identity: true),
                        OfficeName = c.String(),
                        Address = c.String(),
                    })
                .PrimaryKey(t => t.OfficeId);
            
            CreateTable(
                "dbo.Projects",
                c => new
                    {
                        ProjectId = c.Int(nullable: false, identity: true),
                        ProjectName = c.String(),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.ProjectId);
            
            AddColumn("dbo.Employees", "ProjectId", c => c.Int());
            AddColumn("dbo.Employees", "OfficeId", c => c.Int());
            CreateIndex("dbo.Employees", "ProjectId");
            CreateIndex("dbo.Employees", "OfficeId");
            AddForeignKey("dbo.Employees", "OfficeId", "dbo.Offices", "OfficeId");
            AddForeignKey("dbo.Employees", "ProjectId", "dbo.Projects", "ProjectId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Employees", "ProjectId", "dbo.Projects");
            DropForeignKey("dbo.Employees", "OfficeId", "dbo.Offices");
            DropIndex("dbo.Employees", new[] { "OfficeId" });
            DropIndex("dbo.Employees", new[] { "ProjectId" });
            DropColumn("dbo.Employees", "OfficeId");
            DropColumn("dbo.Employees", "ProjectId");
            DropTable("dbo.Projects");
            DropTable("dbo.Offices");
        }
    }
}
