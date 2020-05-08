namespace GestionConcours.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class fichier : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Fichiers",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Cne = c.String(),
                        nom = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Fichiers");
        }
    }
}
