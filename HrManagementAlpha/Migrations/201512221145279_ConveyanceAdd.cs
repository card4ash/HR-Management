namespace HrManagementAlpha.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ConveyanceAdd : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ConveyanceBillRow",
                c => new
                    {
                        RowId = c.Int(nullable: false, identity: true),
                        ConveyanceBillId = c.Int(nullable: false),
                        RowDate = c.DateTime(storeType: "date"),
                        Purpose = c.String(maxLength: 250),
                        FromLocation = c.String(maxLength: 250),
                        ToLocation = c.String(maxLength: 250),
                        MadeOfTransport = c.String(maxLength: 50),
                        Fare = c.Decimal(precision: 18, scale: 2, storeType: "numeric"),
                    })
                .PrimaryKey(t => t.RowId)
                .ForeignKey("dbo.ConveyanceBills", t => t.ConveyanceBillId)
                .Index(t => t.ConveyanceBillId);
            
            CreateTable(
                "dbo.ConveyanceBills",
                c => new
                    {
                        ConveynaceBillId = c.Int(nullable: false, identity: true),
                        JobNo = c.String(maxLength: 250),
                        CostCentre = c.String(maxLength: 250),
                        NameOfClient = c.String(maxLength: 250),
                        IsBillableToClient = c.Boolean(),
                        TotalBill = c.Decimal(precision: 18, scale: 2, storeType: "numeric"),
                        SubmittedBy = c.Int(nullable: false),
                        Name = c.String(maxLength: 250),
                        IsApprovedByFinance = c.Boolean(),
                        IsApprovedByManager = c.Boolean(),
                        ConveyanceStatusId = c.Int(),
                        SubmitDate = c.DateTime(storeType: "date"),
                    })
                .PrimaryKey(t => t.ConveynaceBillId)
                .ForeignKey("dbo.ConveyanceStatus", t => t.ConveyanceStatusId)
                .ForeignKey("dbo.Employees", t => t.SubmittedBy)
                .Index(t => t.SubmittedBy)
                .Index(t => t.ConveyanceStatusId);
            
            CreateTable(
                "dbo.ConveyanceStatus",
                c => new
                    {
                        ConveyanceStatusId = c.Int(nullable: false, identity: true),
                        ConveyanceStatusName = c.String(maxLength: 500),
                    })
                .PrimaryKey(t => t.ConveyanceStatusId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ConveyanceBills", "SubmittedBy", "dbo.Employees");
            DropForeignKey("dbo.ConveyanceBills", "ConveyanceStatusId", "dbo.ConveyanceStatus");
            DropForeignKey("dbo.ConveyanceBillRow", "ConveyanceBillId", "dbo.ConveyanceBills");
            DropIndex("dbo.ConveyanceBills", new[] { "ConveyanceStatusId" });
            DropIndex("dbo.ConveyanceBills", new[] { "SubmittedBy" });
            DropIndex("dbo.ConveyanceBillRow", new[] { "ConveyanceBillId" });
            DropTable("dbo.ConveyanceStatus");
            DropTable("dbo.ConveyanceBills");
            DropTable("dbo.ConveyanceBillRow");
        }
    }
}
