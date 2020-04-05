using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using GestionConcours.Models;

namespace GestionConcours.Services
{
    public class Search3ServiceImp : ISearch3Service
    {


		public IEnumerable<SearchModel3> info(int niveau)
        {
            GestionConcourDbContext db = new GestionConcourDbContext();
            var x = (from c in db.Candidats
                     join a in db.AnneeUniversitaires on c.Cne equals a.Cne
                     join b in db.Baccalaureats on c.Cne equals b.Cne
                     join concour in db.CouncourEcrits on c.Cne equals concour.Cne
                     join d in db.Diplomes on c.Cne equals d.Cne
                     where !db.Corbeilles.Select(g => g.CNE).ToList().Contains(c.Cne)
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
                         Note5 = a.Semestre5,
                         Note6 = a.Semestre6,
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

        public IEnumerable<SearchModel3> conformCandidat(string cne,int niveau)
        {
            GestionConcourDbContext db = new GestionConcourDbContext();
            var x = db.Candidats.Where(c => c.Cne == cne).SingleOrDefault();
            x.Conforme = !x.Conforme;
            db.SaveChanges();
            var y = this.info(niveau);
            return y;

        }

        public IEnumerable<SearchModel3> deleteCandidat(string cne,int niveau)
        {
            GestionConcourDbContext db = new GestionConcourDbContext();
            Candidat cand = db.Candidats.Where(c => c.Cne == cne).SingleOrDefault();
            Corbeille corb = new Corbeille();
            corb.CNE = cne;
            db.Corbeilles.Add(corb);
            db.SaveChanges();
            var x = this.info(niveau);
            return x;
        }

        public IEnumerable<SearchModel3> generalSearch(int niveau)
        {
            var x = this.info(niveau);
            return x;
        }

        public IEnumerable<SearchModel3> specifiedSearch(string Criteria, string Key,string Diplome, string Filiere,int niveau)
        {
            //first select
            GestionConcourDbContext db = new GestionConcourDbContext();
            db.Configuration.ProxyCreationEnabled = false;
            var x = (from c in db.Candidats
                     join a in db.AnneeUniversitaires on c.Cne equals a.Cne
                     join b in db.Baccalaureats on c.Cne equals b.Cne
                     join concour in db.CouncourEcrits on c.Cne equals concour.Cne
                     join d in db.Diplomes on c.Cne equals d.Cne
                     where !db.Corbeilles.Select(g => g.CNE).ToList().Contains(c.Cne)
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
                if (Criteria == "convoque" || Criteria == "admis")
                {
                    var param = Expression.Parameter(typeof(SearchModel3), "p");
                    var exp = Expression.Lambda<Func<SearchModel3, bool>>(
                        Expression.Equal(
                            Expression.Property(param, Criteria),
                            Expression.Constant(bool.Parse(Key))
                    ),
                    param
                );
                    x = x.Where(exp);
                }
                else
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
                    x = x.Where(exp);
                }
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