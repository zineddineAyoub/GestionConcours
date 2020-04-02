namespace GestionConcours.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate7 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Epreuves",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Matiere = c.String(nullable: false),
                        Annee = c.String(nullable: false),
                        NomFichier = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Epreuves");
        }
    }
}
