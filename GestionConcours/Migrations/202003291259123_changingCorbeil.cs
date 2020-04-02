namespace GestionConcours.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changingCorbeil : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Corbeilles", "Candidat_Cne", "dbo.Candidats");
            DropIndex("dbo.Corbeilles", new[] { "Candidat_Cne" });
            AddColumn("dbo.Corbeilles", "CNE", c => c.String());
            DropColumn("dbo.Corbeilles", "Candidat_Cne");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Corbeilles", "Candidat_Cne", c => c.String(maxLength: 128));
            DropColumn("dbo.Corbeilles", "CNE");
            CreateIndex("dbo.Corbeilles", "Candidat_Cne");
            AddForeignKey("dbo.Corbeilles", "Candidat_Cne", "dbo.Candidats", "Cne");
        }
    }
}
