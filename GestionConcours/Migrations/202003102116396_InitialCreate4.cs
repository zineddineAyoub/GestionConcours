namespace GestionConcours.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate4 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Candidats", "Niveau", c => c.Int(nullable: false));
            AddColumn("dbo.Candidats", "Verified", c => c.Int(nullable: false));
            AlterColumn("dbo.Candidats", "Password", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Candidats", "Password", c => c.String(nullable: false));
            DropColumn("dbo.Candidats", "Verified");
            DropColumn("dbo.Candidats", "Niveau");
        }
    }
}
