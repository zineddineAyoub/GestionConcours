namespace GestionConcours.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addingCityToCandidat : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Candidats", "Ville", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Candidats", "Ville");
        }
    }
}
