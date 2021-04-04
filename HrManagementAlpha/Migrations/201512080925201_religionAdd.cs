namespace HrManagementAlpha.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class religionAdd : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Religion",
                c => new
                    {
                        ReligionId = c.Int(nullable: false),
                        ReligionName = c.String(maxLength: 50),
                    })
                .PrimaryKey(t => t.ReligionId);
            
            CreateIndex("dbo.Persons", "ReligionId");
            AddForeignKey("dbo.Persons", "ReligionId", "dbo.Religion", "ReligionId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Persons", "ReligionId", "dbo.Religion");
            DropIndex("dbo.Persons", new[] { "ReligionId" });
            DropTable("dbo.Religion");
        }
    }
}
