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
                        University_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Universities", t => t.University_Id)
                .Index(t => t.University_Id);
            
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
                        Program_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Programs", t => t.Program_Id)
                .Index(t => t.Program_Id);
            
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
                        Program_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Programs", t => t.Program_Id)
                .Index(t => t.Program_Id);
            
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
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Programs", "UniversityId", "dbo.Universities");
            DropForeignKey("dbo.Universities", "CityId", "dbo.Cities");
            DropForeignKey("dbo.Addresses", "University_Id", "dbo.Universities");
            DropForeignKey("dbo.TrainingForms", "Program_Id", "dbo.Programs");
            DropForeignKey("dbo.Programs", "SpecialityId", "dbo.Specialities");
            DropForeignKey("dbo.Subjects", "Program_Id", "dbo.Programs");
            DropIndex("dbo.Universities", new[] { "CityId" });
            DropIndex("dbo.TrainingForms", new[] { "Program_Id" });
            DropIndex("dbo.Subjects", new[] { "Program_Id" });
            DropIndex("dbo.Programs", new[] { "SpecialityId" });
            DropIndex("dbo.Programs", new[] { "UniversityId" });
            DropIndex("dbo.Addresses", new[] { "University_Id" });
            DropTable("dbo.Universities");
            DropTable("dbo.TrainingForms");
            DropTable("dbo.Specialities");
            DropTable("dbo.Subjects");
            DropTable("dbo.Programs");
            DropTable("dbo.Cities");
            DropTable("dbo.Addresses");
        }
    }
}
