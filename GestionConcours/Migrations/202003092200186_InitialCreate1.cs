namespace GestionConcours.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Baccalaureats", "TypeBac", c => c.String());
            AddColumn("dbo.Baccalaureats", "DateObtentionBac", c => c.DateTime(nullable: false));
            AddColumn("dbo.Baccalaureats", "NoteBac", c => c.Double(nullable: false));
            AddColumn("dbo.Baccalaureats", "MentionBac", c => c.String());
            AlterColumn("dbo.Candidats", "Cin", c => c.String(nullable: false));
            AlterColumn("dbo.Candidats", "Nom", c => c.String(nullable: false));
            AlterColumn("dbo.Candidats", "Prenom", c => c.String(nullable: false));
            AlterColumn("dbo.Candidats", "Email", c => c.String(nullable: false));
            AlterColumn("dbo.Candidats", "Password", c => c.String(nullable: false));
            DropColumn("dbo.Baccalaureats", "Type");
            DropColumn("dbo.Baccalaureats", "DateObtention");
            DropColumn("dbo.Baccalaureats", "Note");
            DropColumn("dbo.Baccalaureats", "Mention");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Baccalaureats", "Mention", c => c.String());
            AddColumn("dbo.Baccalaureats", "Note", c => c.Double(nullable: false));
            AddColumn("dbo.Baccalaureats", "DateObtention", c => c.DateTime(nullable: false));
            AddColumn("dbo.Baccalaureats", "Type", c => c.String());
            AlterColumn("dbo.Candidats", "Password", c => c.String());
            AlterColumn("dbo.Candidats", "Email", c => c.String());
            AlterColumn("dbo.Candidats", "Prenom", c => c.String());
            AlterColumn("dbo.Candidats", "Nom", c => c.String());
            AlterColumn("dbo.Candidats", "Cin", c => c.String());
            DropColumn("dbo.Baccalaureats", "MentionBac");
            DropColumn("dbo.Baccalaureats", "NoteBac");
            DropColumn("dbo.Baccalaureats", "DateObtentionBac");
            DropColumn("dbo.Baccalaureats", "TypeBac");
        }
    }
}
