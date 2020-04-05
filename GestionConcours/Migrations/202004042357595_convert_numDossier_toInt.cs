namespace GestionConcours.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class convert_numDossier_toInt : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Candidats", "Num_dossier", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Candidats", "Num_dossier", c => c.String());
        }
    }
}
