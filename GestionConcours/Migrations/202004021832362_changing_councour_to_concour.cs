namespace GestionConcours.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changing_councour_to_concour : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.CouncourEcrits", newName: "ConcourEcrits");
            RenameTable(name: "dbo.CouncourOrals", newName: "ConcourOrals");
        }
        
        public override void Down()
        {
            RenameTable(name: "dbo.ConcourOrals", newName: "CouncourOrals");
            RenameTable(name: "dbo.ConcourEcrits", newName: "CouncourEcrits");
        }
    }
}
