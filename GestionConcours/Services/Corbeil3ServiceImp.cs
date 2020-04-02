using GestionConcours.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;

namespace GestionConcours.Services
{
    public class Corbeil3ServiceImp : ICorbeil3Service
    {
        public IEnumerable<SearchModel3> info(int niveau)
        {
            GestionConcourDbContext db = new GestionConcourDbContext();
            var x = (from c in db.Candidats
                     join a in db.AnneeUniversitaires on c.Cne equals a.Cne
                     join b in db.Baccalaureats on c.Cne equals b.Cne
                     join concour in db.CouncourEcrits on c.Cne equals concour.Cne
                     join d in db.Diplomes on c.Cne equals d.Cne
                     join corb in db.Corbeilles on c.Cne equals corb.CNE
                     join oral in db.CouncourOrals on c.Cne equals oral.Cne
                     where c.Niveau == niveau
                     select new SearchModel3
                     {
                         Nom = c.Nom,
                         Prenom = c.Prenom,
                         Sexe = c.Sexe,
                         NoteBac = b.NoteBac,
                         Note1 = a.Semestre1,
                         Note2 = a.Semestre2,
                         Note3 = a.Semestre3,
                         Note4 = a.Semestre4,
                         Dossier = c.Num_dossier,
                         Convoque = c.Convoque,
                         Math = concour.NoteMath,
                         Specialite = concour.NoteSpecialite,
                         Matricule = c.Matricule,
                         Email = c.Email,
                         Lieu_Naiss = c.LieuNaissance,
                         Nationalite = c.Nationalite,
                         Adress = c.Adresse,
                         Ville = c.Ville,
                         Type_dip = d.Type,
                         note_Diplome = d.NoteDiplome,
                         Etablissement = d.Etablissement,
                         Ville_dip = d.VilleObtention,
                         Date_inscription = c.DateInscription,
                         Admis = c.Admis,
                         Filiere = db.Filieres.Where(f => f.ID == c.ID).FirstOrDefault().Nom,
                         Cin = c.Cin,
                         Cne = c.Cne,
                         NonConforme = c.Conforme
                     }).ToList();
            return x;
        }
        public IEnumerable<SearchModel3> afficheCorbeil(int niveau)
        {
            var x = this.info(niveau);
            return x;
        }


        public IEnumerable<SearchModel3> restoreCorbeil(string cne,int niveau)
        {
            GestionConcourDbContext db = new GestionConcourDbContext();
            var y = db.Corbeilles.Where(cor => cor.CNE == cne).SingleOrDefault();
            db.Corbeilles.Remove(y);
            db.SaveChanges();
            var x = this.info(niveau);
            return x;
        }

        public IEnumerable<SearchModel3> searchCriteria(string Criteria, string Key, string Diplome, string Filiere,int niveau)
        {
            //first select
            var param = Expression.Parameter(typeof(SearchModel3), "p");
            var exp = Expression.Lambda<Func<SearchModel3, bool>>(
                Expression.Equal(
                    Expression.Property(param, Criteria),
                    Expression.Constant(Key)
                ),
                param
            );
            GestionConcourDbContext db = new GestionConcourDbContext();
            db.Configuration.ProxyCreationEnabled = false;
            var x = (from c in db.Candidats
                     join a in db.AnneeUniversitaires on c.Cne equals a.Cne
                     join b in db.Baccalaureats on c.Cne equals b.Cne
                     join concour in db.CouncourEcrits on c.Cne equals concour.Cne
                     join d in db.Diplomes on c.Cne equals d.Cne
                     join corb in db.Corbeilles on c.Cne equals corb.CNE
                     join oral in db.CouncourOrals on c.Cne equals oral.Cne
                     where c.Niveau == niveau
                     select new SearchModel3
                     {
                         Nom = c.Nom,
                         Prenom = c.Prenom,
                         Sexe = c.Sexe,
                         NoteBac = b.NoteBac,
                         Note1 = a.Semestre1,
                         Note2 = a.Semestre2,
                         Note3 = a.Semestre3,
                         Note4 = a.Semestre4,
                         Dossier = c.Num_dossier,
                         Convoque = c.Convoque,
                         Math = concour.NoteMath,
                         Specialite = concour.NoteSpecialite,
                         Matricule = c.Matricule,
                         Email = c.Email,
                         Lieu_Naiss = c.LieuNaissance,
                         Nationalite = c.Nationalite,
                         Adress = c.Adresse,
                         Ville = c.Ville,
                         Type_dip = d.Type,
                         note_Diplome = d.NoteDiplome,
                         Etablissement = d.Etablissement,
                         Ville_dip = d.VilleObtention,
                         Date_inscription = c.DateInscription,
                         Admis = c.Admis,
                         Filiere = db.Filieres.Where(f => f.ID == c.ID).FirstOrDefault().Nom,
                         Cin = c.Cin,
                         Cne = c.Cne,
                         NonConforme = c.Conforme
                     });

            if (!String.IsNullOrEmpty(Key))
            {
                x = x.Where(exp);
            }

            if (Filiere != "0")
            {
                x = x.Where(n => n.Filiere == Filiere);
            }

            if (Diplome != "0")
            {
                x = x.Where(s => s.Type_dip == Diplome);
            }
            return x.ToList();
        }
    }
}