namespace HrManagementAlpha.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NTypeAdd : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.IDCardType",
                c => new
                    {
                        IDCardTypeId = c.Int(nullable: false),
                        IDCardTypeName = c.String(maxLength: 50),
                    })
                .PrimaryKey(t => t.IDCardTypeId);
            
            AddColumn("dbo.Persons", "IDCardTypeId", c => c.Int());
            CreateIndex("dbo.Persons", "IDCardTypeId");
            AddForeignKey("dbo.Persons", "IDCardTypeId", "dbo.IDCardType", "IDCardTypeId");
            DropColumn("dbo.Persons", "NidType");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Persons", "NidType", c => c.Int());
            DropForeignKey("dbo.Persons", "IDCardTypeId", "dbo.IDCardType");
            DropIndex("dbo.Persons", new[] { "IDCardTypeId" });
            DropColumn("dbo.Persons", "IDCardTypeId");
            DropTable("dbo.IDCardType");
        }
    }
}
