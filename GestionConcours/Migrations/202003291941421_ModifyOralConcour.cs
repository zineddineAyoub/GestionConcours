namespace GestionConcours.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ModifyOralConcour : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.CouncourOrals", "ID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.CouncourOrals", "ID", c => c.Int(nullable: false));
        }
    }
}
