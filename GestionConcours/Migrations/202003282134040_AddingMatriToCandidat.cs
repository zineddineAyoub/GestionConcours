namespace GestionConcours.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddingMatriToCandidat : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Candidats", "Matricule", c => c.String());
            AddColumn("dbo.Candidats", "Presence", c => c.Boolean(nullable: false));
            AddColumn("dbo.Candidats", "Conforme", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Candidats", "Conforme");
            DropColumn("dbo.Candidats", "Presence");
            DropColumn("dbo.Candidats", "Matricule");
        }
    }
}
