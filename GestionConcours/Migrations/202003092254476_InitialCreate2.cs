namespace GestionConcours.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate2 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.AnneeUniversitaires", "Date", c => c.String());
            AlterColumn("dbo.AnneeUniversitaires", "Redoublant", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.AnneeUniversitaires", "Redoublant", c => c.Boolean(nullable: false));
            AlterColumn("dbo.AnneeUniversitaires", "Date", c => c.DateTime(nullable: false));
        }
    }
}
