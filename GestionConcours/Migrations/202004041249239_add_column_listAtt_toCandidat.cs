namespace GestionConcours.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class add_column_listAtt_toCandidat : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Candidats", "listDatt", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Candidats", "listDatt");
        }
    }
}
