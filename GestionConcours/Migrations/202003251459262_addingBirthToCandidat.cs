namespace GestionConcours.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addingBirthToCandidat : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Candidats", "DateNaissance", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Candidats", "DateNaissance");
        }
    }
}
