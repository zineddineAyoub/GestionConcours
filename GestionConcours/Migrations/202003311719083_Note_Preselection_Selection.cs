namespace GestionConcours.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Note_Preselection_Selection : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Candidats", "NotePreselec", c => c.Double(nullable: false));
            AddColumn("dbo.CouncourEcrits", "NoteGenerale", c => c.Double(nullable: false));
            AlterColumn("dbo.Baccalaureats", "DateObtentionBac", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Baccalaureats", "DateObtentionBac", c => c.DateTime(nullable: false));
            DropColumn("dbo.CouncourEcrits", "NoteGenerale");
            DropColumn("dbo.Candidats", "NotePreselec");
        }
    }
}
