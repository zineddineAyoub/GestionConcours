namespace GestionConcours.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Admins",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Username = c.String(nullable: false),
                        Password = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.AnneeUniversitaires",
                c => new
                    {
                        Cne = c.String(nullable: false, maxLength: 128),
                        Semestre1 = c.Double(nullable: false),
                        Semestre2 = c.Double(nullable: false),
                        Semestre3 = c.Double(nullable: false),
                        Semestre4 = c.Double(nullable: false),
                        Semestre5 = c.Double(nullable: false),
                        Semestre6 = c.Double(nullable: false),
                        Redoublant1 = c.String(),
                        Redoublant2 = c.String(),
                        Redoublant3 = c.String(),
                        AnneUni1 = c.String(),
                        AnneUni2 = c.String(),
                        AnneUni3 = c.String(),
                    })
                .PrimaryKey(t => t.Cne)
                .ForeignKey("dbo.Candidats", t => t.Cne)
                .Index(t => t.Cne);
            
            CreateTable(
                "dbo.Candidats",
                c => new
                    {
                        Cne = c.String(nullable: false, maxLength: 128),
                        Cin = c.String(nullable: false),
                        Nom = c.String(nullable: false),
                        Prenom = c.String(nullable: false),
                        Email = c.String(nullable: false),
                        Adresse = c.String(),
                        Ville = c.String(),
                        LieuNaissance = c.String(),
                        Telephone = c.String(),
                        Nationalite = c.String(),
                        Num_dossier = c.Int(nullable: false),
                        Sexe = c.String(),
                        Gsm = c.String(),
                        DateInscription = c.DateTime(nullable: false),
                        DateNaissance = c.DateTime(nullable: false),
                        Photo = c.String(),
                        NotePreselec = c.Double(nullable: false),
                        Convoque = c.Boolean(nullable: false),
                        Admis = c.Boolean(nullable: false),
                        Niveau = c.Int(nullable: false),
                        Verified = c.Int(nullable: false),
                        Password = c.String(),
                        Matricule = c.String(),
                        Presence = c.Boolean(nullable: false),
                        Conforme = c.Boolean(nullable: false),
                        listDatt = c.Boolean(nullable: false),
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
                        TypeBac = c.String(),
                        DateObtentionBac = c.String(),
                        NoteBac = c.Double(nullable: false),
                        MentionBac = c.String(),
                    })
                .PrimaryKey(t => t.Cne)
                .ForeignKey("dbo.Candidats", t => t.Cne)
                .Index(t => t.Cne);
            
            CreateTable(
                "dbo.ConcourEcrits",
                c => new
                    {
                        Cne = c.String(nullable: false, maxLength: 128),
                        NoteMath = c.Double(nullable: false),
                        NoteSpecialite = c.Double(nullable: false),
                        NoteGenerale = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.Cne)
                .ForeignKey("dbo.Candidats", t => t.Cne)
                .Index(t => t.Cne);
            
            CreateTable(
                "dbo.ConcourOrals",
                c => new
                    {
                        Cne = c.String(nullable: false, maxLength: 128),
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
                        CNE = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
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
            DropForeignKey("dbo.AnneeUniversitaires", "Cne", "dbo.Candidats");
            DropForeignKey("dbo.Candidats", "ID", "dbo.Filieres");
            DropForeignKey("dbo.Diplomes", "Cne", "dbo.Candidats");
            DropForeignKey("dbo.ConcourOrals", "Cne", "dbo.Candidats");
            DropForeignKey("dbo.ConcourEcrits", "Cne", "dbo.Candidats");
            DropForeignKey("dbo.Baccalaureats", "Cne", "dbo.Candidats");
            DropIndex("dbo.Diplomes", new[] { "Cne" });
            DropIndex("dbo.ConcourOrals", new[] { "Cne" });
            DropIndex("dbo.ConcourEcrits", new[] { "Cne" });
            DropIndex("dbo.Baccalaureats", new[] { "Cne" });
            DropIndex("dbo.Candidats", new[] { "ID" });
            DropIndex("dbo.AnneeUniversitaires", new[] { "Cne" });
            DropTable("dbo.Fichiers");
            DropTable("dbo.Epreuves");
            DropTable("dbo.Corbeilles");
            DropTable("dbo.ConfigurationSelections");
            DropTable("dbo.ConfigurationPreselections");
            DropTable("dbo.Filieres");
            DropTable("dbo.Diplomes");
            DropTable("dbo.ConcourOrals");
            DropTable("dbo.ConcourEcrits");
            DropTable("dbo.Baccalaureats");
            DropTable("dbo.Candidats");
            DropTable("dbo.AnneeUniversitaires");
            DropTable("dbo.Admins");
        }
    }
}
