namespace Database.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initialization : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Addresses",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FullAddress = c.String(nullable: false, maxLength: 500, storeType: "nvarchar"),
                        Phone = c.String(maxLength: 20, storeType: "nvarchar"),
                        UniversityId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Universities", t => t.UniversityId, cascadeDelete: true)
                .Index(t => t.UniversityId);
            
            CreateTable(
                "dbo.Universities",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 255, storeType: "nvarchar"),
                        Description = c.String(unicode: false),
                        CityId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Cities", t => t.CityId, cascadeDelete: true)
                .Index(t => t.CityId);
            
            CreateTable(
                "dbo.Cities",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 255, storeType: "nvarchar"),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Programs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 255, storeType: "nvarchar"),
                        Description = c.String(unicode: false),
                        UniversityId = c.Int(nullable: false),
                        SpecialityId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Specialities", t => t.SpecialityId, cascadeDelete: true)
                .ForeignKey("dbo.Universities", t => t.UniversityId, cascadeDelete: true)
                .Index(t => t.UniversityId)
                .Index(t => t.SpecialityId);
            
            CreateTable(
                "dbo.Subjects",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 255, storeType: "nvarchar"),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Specialities",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 255, storeType: "nvarchar"),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.TrainingForms",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Type = c.Int(nullable: false),
                        Price = c.Decimal(precision: 18, scale: 2),
                        DurationInYears = c.Int(nullable: false),
                        BudgetExamPoints = c.Int(),
                        BudgetPlacesCount = c.Int(),
                        ProgramId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Programs", t => t.ProgramId, cascadeDelete: true)
                .Index(t => t.ProgramId);
            
            CreateTable(
                "dbo.ProgramsEgeSubjects",
                c => new
                    {
                        ProgramId = c.Int(nullable: false),
                        SubjectId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.ProgramId, t.SubjectId })
                .ForeignKey("dbo.Programs", t => t.ProgramId, cascadeDelete: true)
                .ForeignKey("dbo.Subjects", t => t.SubjectId, cascadeDelete: true)
                .Index(t => t.ProgramId)
                .Index(t => t.SubjectId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Programs", "UniversityId", "dbo.Universities");
            DropForeignKey("dbo.TrainingForms", "ProgramId", "dbo.Programs");
            DropForeignKey("dbo.Programs", "SpecialityId", "dbo.Specialities");
            DropForeignKey("dbo.ProgramsEgeSubjects", "SubjectId", "dbo.Subjects");
            DropForeignKey("dbo.ProgramsEgeSubjects", "ProgramId", "dbo.Programs");
            DropForeignKey("dbo.Universities", "CityId", "dbo.Cities");
            DropForeignKey("dbo.Addresses", "UniversityId", "dbo.Universities");
            DropIndex("dbo.ProgramsEgeSubjects", new[] { "SubjectId" });
            DropIndex("dbo.ProgramsEgeSubjects", new[] { "ProgramId" });
            DropIndex("dbo.TrainingForms", new[] { "ProgramId" });
            DropIndex("dbo.Programs", new[] { "SpecialityId" });
            DropIndex("dbo.Programs", new[] { "UniversityId" });
            DropIndex("dbo.Universities", new[] { "CityId" });
            DropIndex("dbo.Addresses", new[] { "UniversityId" });
            DropTable("dbo.ProgramsEgeSubjects");
            DropTable("dbo.TrainingForms");
            DropTable("dbo.Specialities");
            DropTable("dbo.Subjects");
            DropTable("dbo.Programs");
            DropTable("dbo.Cities");
            DropTable("dbo.Universities");
            DropTable("dbo.Addresses");
        }
    }
}
