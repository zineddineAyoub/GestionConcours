namespace GestionConcours.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AnneeUniversitaires",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        NoteS1 = c.Double(nullable: false),
                        NosteS2 = c.Double(nullable: false),
                        Date = c.DateTime(nullable: false),
                        Redoublant = c.Boolean(nullable: false),
                        Cne = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Candidats", t => t.Cne)
                .Index(t => t.Cne);
            
            CreateTable(
                "dbo.Candidats",
                c => new
                    {
                        Cne = c.String(nullable: false, maxLength: 128),
                        Cin = c.String(),
                        Nom = c.String(),
                        Prenom = c.String(),
                        Email = c.String(),
                        Adresse = c.String(),
                        LieuNaissance = c.String(),
                        Telephone = c.String(),
                        Nationalite = c.String(),
                        Num_dossier = c.String(),
                        Sexe = c.String(),
                        Gsm = c.String(),
                        DateInscription = c.DateTime(nullable: false),
                        Photo = c.String(),
                        Convoque = c.Boolean(nullable: false),
                        Admis = c.Boolean(nullable: false),
                        Password = c.String(),
                        ID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Cne)
                .ForeignKey("dbo.Filieres", t => t.ID, cascadeDelete: true)
                .Index(t => t.ID);
            
            CreateTable(
                "dbo.Baccalaureats",
                c => new
                    {
                        Cne = c.String(nullable: false, maxLength: 128),
                        Type = c.String(),
                        DateObtention = c.DateTime(nullable: false),
                        Note = c.Double(nullable: false),
                        Mention = c.String(),
                    })
                .PrimaryKey(t => t.Cne)
                .ForeignKey("dbo.Candidats", t => t.Cne)
                .Index(t => t.Cne);
            
            CreateTable(
                "dbo.CouncourEcrits",
                c => new
                    {
                        Cne = c.String(nullable: false, maxLength: 128),
                        NoteMath = c.Double(nullable: false),
                        NoteSpecialite = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.Cne)
                .ForeignKey("dbo.Candidats", t => t.Cne)
                .Index(t => t.Cne);
            
            CreateTable(
                "dbo.CouncourOrals",
                c => new
                    {
                        Cne = c.String(nullable: false, maxLength: 128),
                        ID = c.Int(nullable: false),
                        Classement = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Cne)
                .ForeignKey("dbo.Candidats", t => t.Cne)
                .Index(t => t.Cne);
            
            CreateTable(
                "dbo.Diplomes",
                c => new
                    {
                        Cne = c.String(nullable: false, maxLength: 128),
                        Type = c.String(),
                        Etablissement = c.String(),
                        VilleObtention = c.String(),
                        NoteDiplome = c.Double(nullable: false),
                        Specialite = c.String(),
                    })
                .PrimaryKey(t => t.Cne)
                .ForeignKey("dbo.Candidats", t => t.Cne)
                .Index(t => t.Cne);
            
            CreateTable(
                "dbo.Filieres",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Nom = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.ConfigurationPreselections",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Filiere = c.String(),
                        TypeDiplome = c.String(),
                        CoeffBac = c.Int(nullable: false),
                        CoeffS1 = c.Int(nullable: false),
                        CoeffS2 = c.Int(nullable: false),
                        CoeffS3 = c.Int(nullable: false),
                        CoeffS4 = c.Int(nullable: false),
                        CoeffS5 = c.Int(nullable: false),
                        CoeffS6 = c.Int(nullable: false),
                        NoteJoker = c.Double(nullable: false),
                        NoteSeuil = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.ConfigurationSelections",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Filiere = c.String(),
                        CoeffMath = c.Int(nullable: false),
                        CoeffSpecialite = c.Int(nullable: false),
                        NbrPlace = c.Int(nullable: false),
                        NbrPlaceListAtt = c.Int(nullable: false),
                        NoteMin = c.Double(nullable: false),
                        TypeClassement = c.String(),
                        Niveau = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Corbeilles",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Candidat_Cne = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Candidats", t => t.Candidat_Cne)
                .Index(t => t.Candidat_Cne);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Corbeilles", "Candidat_Cne", "dbo.Candidats");
            DropForeignKey("dbo.AnneeUniversitaires", "Cne", "dbo.Candidats");
            DropForeignKey("dbo.Candidats", "ID", "dbo.Filieres");
            DropForeignKey("dbo.Diplomes", "Cne", "dbo.Candidats");
            DropForeignKey("dbo.CouncourOrals", "Cne", "dbo.Candidats");
            DropForeignKey("dbo.CouncourEcrits", "Cne", "dbo.Candidats");
            DropForeignKey("dbo.Baccalaureats", "Cne", "dbo.Candidats");
            DropIndex("dbo.Corbeilles", new[] { "Candidat_Cne" });
            DropIndex("dbo.Diplomes", new[] { "Cne" });
            DropIndex("dbo.CouncourOrals", new[] { "Cne" });
            DropIndex("dbo.CouncourEcrits", new[] { "Cne" });
            DropIndex("dbo.Baccalaureats", new[] { "Cne" });
            DropIndex("dbo.Candidats", new[] { "ID" });
            DropIndex("dbo.AnneeUniversitaires", new[] { "Cne" });
            DropTable("dbo.Corbeilles");
            DropTable("dbo.ConfigurationSelections");
            DropTable("dbo.ConfigurationPreselections");
            DropTable("dbo.Filieres");
            DropTable("dbo.Diplomes");
            DropTable("dbo.CouncourOrals");
            DropTable("dbo.CouncourEcrits");
            DropTable("dbo.Baccalaureats");
            DropTable("dbo.Candidats");
            DropTable("dbo.AnneeUniversitaires");
        }
    }
}
