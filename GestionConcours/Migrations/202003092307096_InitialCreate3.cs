namespace GestionConcours.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate3 : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.AnneeUniversitaires", new[] { "Cne" });
            DropPrimaryKey("dbo.AnneeUniversitaires");
            AddColumn("dbo.AnneeUniversitaires", "Semestre1", c => c.Double(nullable: false));
            AddColumn("dbo.AnneeUniversitaires", "Semestre2", c => c.Double(nullable: false));
            AddColumn("dbo.AnneeUniversitaires", "Semestre3", c => c.Double(nullable: false));
            AddColumn("dbo.AnneeUniversitaires", "Semestre4", c => c.Double(nullable: false));
            AddColumn("dbo.AnneeUniversitaires", "Semestre5", c => c.Double(nullable: false));
            AddColumn("dbo.AnneeUniversitaires", "Semestre6", c => c.Double(nullable: false));
            AddColumn("dbo.AnneeUniversitaires", "Redoublant1", c => c.String());
            AddColumn("dbo.AnneeUniversitaires", "Redoublant2", c => c.String());
            AddColumn("dbo.AnneeUniversitaires", "Redoublant3", c => c.String());
            AddColumn("dbo.AnneeUniversitaires", "AnneUni1", c => c.String());
            AddColumn("dbo.AnneeUniversitaires", "AnneUni2", c => c.String());
            AddColumn("dbo.AnneeUniversitaires", "AnneUni3", c => c.String());
            AlterColumn("dbo.AnneeUniversitaires", "Cne", c => c.String(nullable: false, maxLength: 128));
            AddPrimaryKey("dbo.AnneeUniversitaires", "Cne");
            CreateIndex("dbo.AnneeUniversitaires", "Cne");
            DropColumn("dbo.AnneeUniversitaires", "ID");
            DropColumn("dbo.AnneeUniversitaires", "NoteS1");
            DropColumn("dbo.AnneeUniversitaires", "NosteS2");
            DropColumn("dbo.AnneeUniversitaires", "Date");
            DropColumn("dbo.AnneeUniversitaires", "Redoublant");
        }
        
        public override void Down()
        {
            AddColumn("dbo.AnneeUniversitaires", "Redoublant", c => c.String());
            AddColumn("dbo.AnneeUniversitaires", "Date", c => c.String());
            AddColumn("dbo.AnneeUniversitaires", "NosteS2", c => c.Double(nullable: false));
            AddColumn("dbo.AnneeUniversitaires", "NoteS1", c => c.Double(nullable: false));
            AddColumn("dbo.AnneeUniversitaires", "ID", c => c.Int(nullable: false, identity: true));
            DropIndex("dbo.AnneeUniversitaires", new[] { "Cne" });
            DropPrimaryKey("dbo.AnneeUniversitaires");
            AlterColumn("dbo.AnneeUniversitaires", "Cne", c => c.String(maxLength: 128));
            DropColumn("dbo.AnneeUniversitaires", "AnneUni3");
            DropColumn("dbo.AnneeUniversitaires", "AnneUni2");
            DropColumn("dbo.AnneeUniversitaires", "AnneUni1");
            DropColumn("dbo.AnneeUniversitaires", "Redoublant3");
            DropColumn("dbo.AnneeUniversitaires", "Redoublant2");
            DropColumn("dbo.AnneeUniversitaires", "Redoublant1");
            DropColumn("dbo.AnneeUniversitaires", "Semestre6");
            DropColumn("dbo.AnneeUniversitaires", "Semestre5");
            DropColumn("dbo.AnneeUniversitaires", "Semestre4");
            DropColumn("dbo.AnneeUniversitaires", "Semestre3");
            DropColumn("dbo.AnneeUniversitaires", "Semestre2");
            DropColumn("dbo.AnneeUniversitaires", "Semestre1");
            AddPrimaryKey("dbo.AnneeUniversitaires", "ID");
            CreateIndex("dbo.AnneeUniversitaires", "Cne");
        }
    }
}
