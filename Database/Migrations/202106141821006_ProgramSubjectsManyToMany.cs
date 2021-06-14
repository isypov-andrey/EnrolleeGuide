namespace Database.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ProgramSubjectsManyToMany : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Subjects", "Program_Id", "dbo.Programs");
            DropIndex("dbo.Subjects", new[] { "Program_Id" });
            CreateTable(
                "dbo.SubjectPrograms",
                c => new
                    {
                        Subject_Id = c.Int(nullable: false),
                        Program_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Subject_Id, t.Program_Id })
                .ForeignKey("dbo.Subjects", t => t.Subject_Id, cascadeDelete: true)
                .ForeignKey("dbo.Programs", t => t.Program_Id, cascadeDelete: true)
                .Index(t => t.Subject_Id)
                .Index(t => t.Program_Id);
            
            DropColumn("dbo.Subjects", "Program_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Subjects", "Program_Id", c => c.Int());
            DropForeignKey("dbo.SubjectPrograms", "Program_Id", "dbo.Programs");
            DropForeignKey("dbo.SubjectPrograms", "Subject_Id", "dbo.Subjects");
            DropIndex("dbo.SubjectPrograms", new[] { "Program_Id" });
            DropIndex("dbo.SubjectPrograms", new[] { "Subject_Id" });
            DropTable("dbo.SubjectPrograms");
            CreateIndex("dbo.Subjects", "Program_Id");
            AddForeignKey("dbo.Subjects", "Program_Id", "dbo.Programs", "Id");
        }
    }
}
