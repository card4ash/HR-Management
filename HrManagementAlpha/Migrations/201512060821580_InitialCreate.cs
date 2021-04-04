namespace HrManagementAlpha.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Companies",
                c => new
                    {
                        CompanyId = c.Int(nullable: false, identity: true),
                        CompanyName = c.String(maxLength: 150, unicode: false),
                        LoginName = c.String(maxLength: 50),
                        LoginPassword = c.String(maxLength: 50),
                        IsActive = c.Boolean(),
                    })
                .PrimaryKey(t => t.CompanyId);
            
            CreateTable(
                "dbo.Departments",
                c => new
                    {
                        DepartmentId = c.Int(nullable: false, identity: true),
                        DepartmentName = c.String(maxLength: 50, unicode: false),
                    })
                .PrimaryKey(t => t.DepartmentId);
            
            CreateTable(
                "dbo.Employees",
                c => new
                    {
                        EmployeeId = c.Int(nullable: false, identity: true),
                        PersonId = c.Int(nullable: false),
                        UserId = c.String(nullable: false, maxLength: 128),
                        DesignationId = c.Int(),
                        DepartmentId = c.Int(),
                        BossId = c.Int(),
                        EmployeeCode = c.String(maxLength: 50),
                        JoinDate = c.DateTime(storeType: "date"),
                        CreatedBy = c.Int(),
                        Active = c.Boolean(),
                    })
                .PrimaryKey(t => t.EmployeeId)
                .ForeignKey("dbo.Departments", t => t.DepartmentId)
                .ForeignKey("dbo.Designations", t => t.DesignationId)
                .ForeignKey("dbo.Persons", t => t.PersonId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.PersonId)
                .Index(t => t.UserId)
                .Index(t => t.DesignationId)
                .Index(t => t.DepartmentId);
            
            CreateTable(
                "dbo.Designations",
                c => new
                    {
                        DesignationId = c.Int(nullable: false, identity: true),
                        DesignationName = c.String(maxLength: 50, unicode: false),
                        ParentId = c.Int(),
                    })
                .PrimaryKey(t => t.DesignationId);
            
            CreateTable(
                "dbo.EmployeeCertification",
                c => new
                    {
                        EmployeeCertificationId = c.Int(nullable: false, identity: true),
                        EmployeeId = c.Int(nullable: false),
                        CertificationName = c.String(maxLength: 250),
                        InstituteName = c.String(maxLength: 250),
                        Location = c.String(maxLength: 250),
                        FromDate = c.DateTime(storeType: "date"),
                        ToDate = c.DateTime(storeType: "date"),
                        Active = c.Boolean(),
                    })
                .PrimaryKey(t => t.EmployeeCertificationId)
                .ForeignKey("dbo.Employees", t => t.EmployeeId)
                .Index(t => t.EmployeeId);
            
            CreateTable(
                "dbo.EmployeeEducation",
                c => new
                    {
                        EmployeeEducationId = c.Int(nullable: false, identity: true),
                        EmployeeId = c.Int(nullable: false),
                        EducationLevelId = c.Int(nullable: false),
                        ExamTitle = c.String(maxLength: 250),
                        GroupName = c.String(maxLength: 50),
                        InstituteName = c.String(maxLength: 350),
                        IsForeign = c.Boolean(),
                        ResultId = c.Int(),
                        MarksPercent = c.Decimal(precision: 5, scale: 2, storeType: "numeric"),
                        Grade = c.Decimal(precision: 5, scale: 2, storeType: "numeric"),
                        Scale = c.Decimal(precision: 5, scale: 2, storeType: "numeric"),
                        IsMarksMention = c.Boolean(),
                        YearOfPassing = c.Int(),
                        Duration = c.Int(),
                        Achievement = c.String(maxLength: 450),
                        Active = c.Boolean(),
                    })
                .PrimaryKey(t => t.EmployeeEducationId)
                .ForeignKey("dbo.EducationLevel", t => t.EducationLevelId)
                .ForeignKey("dbo.Employees", t => t.EmployeeId)
                .Index(t => t.EmployeeId)
                .Index(t => t.EducationLevelId);
            
            CreateTable(
                "dbo.EducationLevel",
                c => new
                    {
                        EducationLevelId = c.Int(nullable: false, identity: true),
                        EducationLevelName = c.String(maxLength: 250),
                        Active = c.Boolean(),
                    })
                .PrimaryKey(t => t.EducationLevelId);
            
            CreateTable(
                "dbo.EmployeeEmploymentHistory",
                c => new
                    {
                        EmployeeEmploymentHistoryId = c.Int(nullable: false, identity: true),
                        EmployeeId = c.Int(nullable: false),
                        CompanyName = c.String(maxLength: 250),
                        CompanyBusinessId = c.Int(),
                        CompanyBusiness = c.String(maxLength: 250),
                        CompanyLocation = c.String(maxLength: 250),
                        PositionHeld = c.String(maxLength: 250),
                        Department = c.String(maxLength: 250),
                        Responsibilities = c.String(maxLength: 500, fixedLength: true),
                        FromDate = c.DateTime(storeType: "date"),
                        ToDate = c.DateTime(storeType: "date"),
                        IsCurrentlyWorking = c.Boolean(),
                        Active = c.Boolean(),
                    })
                .PrimaryKey(t => t.EmployeeEmploymentHistoryId)
                .ForeignKey("dbo.Employees", t => t.EmployeeId)
                .Index(t => t.EmployeeId);
            
            CreateTable(
                "dbo.EmployeeTraining",
                c => new
                    {
                        EmployeeTrainingId = c.Int(nullable: false, identity: true),
                        EmployeeId = c.Int(nullable: false),
                        TrainingTitle = c.String(maxLength: 250),
                        TopicCovered = c.String(maxLength: 250),
                        Institute = c.String(maxLength: 250),
                        Country = c.String(maxLength: 100, fixedLength: true),
                        Location = c.String(maxLength: 100, fixedLength: true),
                        TrainingYear = c.Int(),
                        Duration = c.String(maxLength: 50),
                        Active = c.Boolean(),
                    })
                .PrimaryKey(t => t.EmployeeTrainingId)
                .ForeignKey("dbo.Employees", t => t.EmployeeId)
                .Index(t => t.EmployeeId);
            
            CreateTable(
                "dbo.Persons",
                c => new
                    {
                        PersonId = c.Int(nullable: false, identity: true),
                        PersonName = c.String(maxLength: 150),
                        FatherName = c.String(maxLength: 150),
                        MotherName = c.String(maxLength: 150),
                        PresentAddress = c.String(maxLength: 400),
                        PresentDistrictId = c.Int(),
                        PresentThanaId = c.Int(),
                        PermanentAddress = c.String(maxLength: 400),
                        DistrictId = c.Int(),
                        ThanaId = c.Int(),
                        ContactNo = c.String(maxLength: 50),
                        SecondContactNo = c.String(maxLength: 50),
                        DateOfBirth = c.DateTime(storeType: "date"),
                        NidType = c.Int(),
                        NidNo = c.String(maxLength: 50),
                        GenderId = c.Int(),
                        MaritalStatusId = c.Int(),
                        ReligionId = c.Int(),
                        Nationality = c.String(maxLength: 50),
                        IsActive = c.Boolean(),
                    })
                .PrimaryKey(t => t.PersonId)
                .ForeignKey("dbo.Districts", t => t.DistrictId)
                .ForeignKey("dbo.Districts", t => t.PresentDistrictId)
                .ForeignKey("dbo.Thanas", t => t.ThanaId)
                .ForeignKey("dbo.Thanas", t => t.PresentThanaId)
                .ForeignKey("dbo.Gender", t => t.GenderId)
                .ForeignKey("dbo.MaritalStatus", t => t.MaritalStatusId)
                .Index(t => t.PresentDistrictId)
                .Index(t => t.PresentThanaId)
                .Index(t => t.DistrictId)
                .Index(t => t.ThanaId)
                .Index(t => t.GenderId)
                .Index(t => t.MaritalStatusId);
            
            CreateTable(
                "dbo.Districts",
                c => new
                    {
                        DISTRICT_ID = c.Int(nullable: false),
                        DISTRICT_NAME = c.String(maxLength: 50, unicode: false),
                        ACTIVE = c.Boolean(),
                        REMARKS = c.String(maxLength: 50, unicode: false),
                    })
                .PrimaryKey(t => t.DISTRICT_ID);
            
            CreateTable(
                "dbo.Thanas",
                c => new
                    {
                        THANA_ID = c.Int(nullable: false),
                        THANA_NAME = c.String(maxLength: 50, unicode: false),
                        DISTRICT_ID = c.Int(),
                        ACTIVE = c.Boolean(),
                        REMARKS = c.String(maxLength: 50, unicode: false),
                    })
                .PrimaryKey(t => t.THANA_ID)
                .ForeignKey("dbo.Districts", t => t.DISTRICT_ID)
                .Index(t => t.DISTRICT_ID);
            
            CreateTable(
                "dbo.Gender",
                c => new
                    {
                        GenderId = c.Int(nullable: false),
                        GenderName = c.String(maxLength: 50, unicode: false),
                    })
                .PrimaryKey(t => t.GenderId);
            
            CreateTable(
                "dbo.MaritalStatus",
                c => new
                    {
                        MaritalStatusId = c.Int(nullable: false),
                        MaritalStatusName = c.String(maxLength: 50),
                    })
                .PrimaryKey(t => t.MaritalStatusId);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.Employees", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Employees", "PersonId", "dbo.Persons");
            DropForeignKey("dbo.Persons", "MaritalStatusId", "dbo.MaritalStatus");
            DropForeignKey("dbo.Persons", "GenderId", "dbo.Gender");
            DropForeignKey("dbo.Persons", "PresentThanaId", "dbo.Thanas");
            DropForeignKey("dbo.Persons", "ThanaId", "dbo.Thanas");
            DropForeignKey("dbo.Thanas", "DISTRICT_ID", "dbo.Districts");
            DropForeignKey("dbo.Persons", "PresentDistrictId", "dbo.Districts");
            DropForeignKey("dbo.Persons", "DistrictId", "dbo.Districts");
            DropForeignKey("dbo.EmployeeTraining", "EmployeeId", "dbo.Employees");
            DropForeignKey("dbo.EmployeeEmploymentHistory", "EmployeeId", "dbo.Employees");
            DropForeignKey("dbo.EmployeeEducation", "EmployeeId", "dbo.Employees");
            DropForeignKey("dbo.EmployeeEducation", "EducationLevelId", "dbo.EducationLevel");
            DropForeignKey("dbo.EmployeeCertification", "EmployeeId", "dbo.Employees");
            DropForeignKey("dbo.Employees", "DesignationId", "dbo.Designations");
            DropForeignKey("dbo.Employees", "DepartmentId", "dbo.Departments");
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.Thanas", new[] { "DISTRICT_ID" });
            DropIndex("dbo.Persons", new[] { "MaritalStatusId" });
            DropIndex("dbo.Persons", new[] { "GenderId" });
            DropIndex("dbo.Persons", new[] { "ThanaId" });
            DropIndex("dbo.Persons", new[] { "DistrictId" });
            DropIndex("dbo.Persons", new[] { "PresentThanaId" });
            DropIndex("dbo.Persons", new[] { "PresentDistrictId" });
            DropIndex("dbo.EmployeeTraining", new[] { "EmployeeId" });
            DropIndex("dbo.EmployeeEmploymentHistory", new[] { "EmployeeId" });
            DropIndex("dbo.EmployeeEducation", new[] { "EducationLevelId" });
            DropIndex("dbo.EmployeeEducation", new[] { "EmployeeId" });
            DropIndex("dbo.EmployeeCertification", new[] { "EmployeeId" });
            DropIndex("dbo.Employees", new[] { "DepartmentId" });
            DropIndex("dbo.Employees", new[] { "DesignationId" });
            DropIndex("dbo.Employees", new[] { "UserId" });
            DropIndex("dbo.Employees", new[] { "PersonId" });
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.MaritalStatus");
            DropTable("dbo.Gender");
            DropTable("dbo.Thanas");
            DropTable("dbo.Districts");
            DropTable("dbo.Persons");
            DropTable("dbo.EmployeeTraining");
            DropTable("dbo.EmployeeEmploymentHistory");
            DropTable("dbo.EducationLevel");
            DropTable("dbo.EmployeeEducation");
            DropTable("dbo.EmployeeCertification");
            DropTable("dbo.Designations");
            DropTable("dbo.Employees");
            DropTable("dbo.Departments");
            DropTable("dbo.Companies");
        }
    }
}
