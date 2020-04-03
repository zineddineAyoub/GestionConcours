using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using GestionConcours.Models;

namespace GestionConcours.Services
{
    public class SelectionServiceImp : ISelectionService
    {
        GestionConcourDbContext db = new GestionConcourDbContext();

        public ConfigurationSelection getConfigurationSelection(string filiere,int niveau)
        {
            if(niveau==3)
            {
                var data = db.ConfigurationSelections.Where(c => c.Filiere == filiere && c.Niveau == "3").SingleOrDefault();
                return data;
            }

            if (niveau == 4)
            {
                var data = db.ConfigurationSelections.Where(c => c.Filiere == filiere && c.Niveau == "4").SingleOrDefault();
                return data;
            }


            return null;
        }

        public void updateConfigurationSelection(ConfigurationSelection configurationSelection)
        {

           var data = db.ConfigurationSelections.Where(c => c.Filiere == configurationSelection.Filiere && c.Niveau==configurationSelection.Niveau).SingleOrDefault();
            if (data == null)
            {
                db.ConfigurationSelections.Add(configurationSelection);
                
            }
            else
            {
                data.CoeffMath = configurationSelection.CoeffMath;
                data.CoeffSpecialite = configurationSelection.CoeffSpecialite;
                data.NbrPlace = configurationSelection.NbrPlace;
                data.NoteMin = configurationSelection.NoteMin;
                data.TypeClassement = configurationSelection.TypeClassement;
                data.NbrPlaceListAtt = configurationSelection.NbrPlaceListAtt;

            }

            db.SaveChanges();


        }

        public void calculeNoteGlobale(string filiere)
        {

            ConfigurationSelection conf = db.ConfigurationSelections.Where(a=>a.Filiere.Equals(filiere) && a.Niveau=="3").SingleOrDefault();
            var data = db.Candidats.Where(c => c.Filiere.Nom.Equals(filiere) &&  c.Niveau==3);
            foreach(var person in data)
            {
                ConcourEcrit concours =  db.CouncourEcrits.Where(c => c.Cne.Equals(person.Cne)).SingleOrDefault();
                concours.NoteGenerale = (concours.NoteMath * conf.CoeffMath + concours.NoteSpecialite * conf.CoeffSpecialite)/(conf.CoeffMath+conf.CoeffSpecialite);
                
            }
            db.SaveChanges();


        }

        public IEnumerable<AdmisModel> selectStudents(string filiere,string nv)
        {

            if(nv=="3")
            {
                ConfigurationSelection conf = db.ConfigurationSelections.Where(a => a.Filiere.Equals(filiere) && a.Niveau.Equals(nv)).SingleOrDefault();
                int nombreTotal = conf.NbrPlace + conf.NbrPlaceListAtt;
                // les persones deja admis deviens non admis 
                var data = db.Candidats.Where(c => c.Filiere.Nom.Equals(filiere) && c.Admis == true && c.Niveau==3);
                foreach (var person in data)
                {
                    person.Admis = false;
                }

                //      var total = db.CouncourEcrits.Include("Candidat").Take(nombreTotal).OrderByDescending(c=>c.NoteGenerale).Where(c => c.NoteGenerale > conf.NoteMin);

                // Take(NbrePlace)
                var admis = db.CouncourEcrits.Include("Candidat").Where(c => c.NoteGenerale > conf.NoteMin).OrderByDescending(c => c.NoteGenerale).Take(conf.NbrPlace+1);
                foreach (var a in admis)
                {
                    a.Candidat.Admis = true;
                }

                db.SaveChanges();

                var x = (from c in db.Candidats
                         join a in db.AnneeUniversitaires on c.Cne equals a.Cne
                         join fi in db.Filieres on c.ID equals fi.ID
                         join b in db.Baccalaureats on c.Cne equals b.Cne
                         join concour in db.CouncourEcrits on c.Cne equals concour.Cne
                         where concour.NoteGenerale > conf.NoteMin
                         join d in db.Diplomes on c.Cne equals d.Cne
                         where !db.Corbeilles.Select(g => g.CNE).ToList().Contains(c.Cne)
                         where c.Niveau == 3
                         where fi.Nom == filiere
                         orderby concour.NoteGenerale
                         descending


                         select new AdmisModel
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

                             Math = concour.NoteMath,
                             Specialite = concour.NoteSpecialite,
                             Globale = concour.NoteGenerale,
                             Matricule = c.Matricule,

                             Type_dip = d.Type,
                             note_Diplome = d.NoteDiplome,
                             Etablissement = d.Etablissement,
                             Ville_dip = d.VilleObtention,

                             Admis = c.Admis,
                             Filiere = db.Filieres.Where(f => f.ID == c.ID).FirstOrDefault().Nom,
                             Cin = c.Cin,
                             Cne = c.Cne,

                         }).ToList();

                int NombrePlaceTotal = conf.NbrPlaceListAtt + conf.NbrPlace;
                x = x.Take(NombrePlaceTotal).ToList();
                string classement = db.ConfigurationSelections.Where(c => c.Filiere.Equals(filiere) && c.Niveau == nv).SingleOrDefault().TypeClassement;

                if (classement.Equals("noteGlobal"))
                {
                    x = x.OrderByDescending(critere => critere.Globale).ToList();
                }

                else if (classement.Equals("noteMath"))
                {
                    x = x.OrderByDescending(critere => critere.Math).ToList();
                }

                else if(classement.Equals("noteBac"))
                {
                    x = x.OrderByDescending(critere => critere.NoteBac).ToList();
                }


                return x;
            }

            else if (nv=="4")
            {
                 ConfigurationSelection conf = db.ConfigurationSelections.Where(a => a.Filiere.Equals(filiere) && a.Niveau == nv).SingleOrDefault();
                int nombreTotal = conf.NbrPlace + conf.NbrPlaceListAtt;
                // les persones deja admis deviens non admis 
                var data = db.Candidats.Where(c => c.Filiere.Nom.Equals(filiere) && c.Admis == true && c.Niveau==4);
                foreach (var person in data)
                {
                    person.Admis = false;
                }

                //      var total = db.CouncourEcrits.Include("Candidat").Take(nombreTotal).OrderByDescending(c=>c.NoteGenerale).Where(c => c.NoteGenerale > conf.NoteMin);

                // Take(NbrePlace)
                var admis = db.CouncourOrals.Include("Candidat").OrderBy(c => c.Classement).Take(conf.NbrPlace);
                foreach (var a in admis)
                {
                    a.Candidat.Admis = true;
                }

                db.SaveChanges();

                var x = (from c in db.Candidats
                         join a in db.AnneeUniversitaires on c.Cne equals a.Cne
                         join fi in db.Filieres on c.ID equals fi.ID
                         join b in db.Baccalaureats on c.Cne equals b.Cne
                         join concour in db.CouncourOrals on c.Cne equals concour.Cne
                        
                         join d in db.Diplomes on c.Cne equals d.Cne
                         where !db.Corbeilles.Select(g => g.CNE).ToList().Contains(c.Cne)
                         where c.Niveau == 4
                         where fi.Nom == filiere
                         orderby concour.Classement


                         select new AdmisModel
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

                             Math = 0,
                             Specialite = 0,
                             Globale = 0,
                             Matricule = c.Matricule,

                             Type_dip = d.Type,
                             note_Diplome = d.NoteDiplome,
                             Etablissement = d.Etablissement,
                             Ville_dip = d.VilleObtention,

                             Admis = c.Admis,
                             Filiere = db.Filieres.Where(f => f.ID == c.ID).FirstOrDefault().Nom,
                             Cin = c.Cin,
                             Cne = c.Cne,

                         }).ToList();

                int NombrePlaceTotal = conf.NbrPlaceListAtt + conf.NbrPlace;
                x = x.Take(NombrePlaceTotal).ToList();
                



                return x;
            }

            return null;

        }

        public IEnumerable<ListFinal> getListAttente(string filiere)
        {
           // var data = db.Candidats.ToList();//.Include("Filiere").Where(c => c.Admis==true && c.Filiere.Nom.Equals(filiere)).ToList();
           var x = (from c in db.Candidats
                     where c.Admis==false
                    join a in db.Filieres on c.ID equals a.ID
                    join n in db.CouncourEcrits on c.Cne equals n.Cne
                    
                    where c.Niveau == 3
                    where a.Nom == filiere
                    orderby n.NoteGenerale 
                    descending
                    
                    
                     select new ListFinal
                     {
                         Nom = c.Nom,
                         Prenom = c.Prenom,
                         Matricule = c.Matricule,
                         
                        Num_dossier = c.Num_dossier,
                       
                         Cin = c.Cin,
                         
                         
                     }).ToList();

            var NombrePlace = db.ConfigurationSelections.Where(c => c.Filiere.Equals(filiere) && c.Niveau=="3").SingleOrDefault().NbrPlaceListAtt;
            var resultat = x.Take(NombrePlace).ToList();
            return resultat;
        }

        public IEnumerable<ListFinal> getListPrincipale(string filiere)
        {
             var x = (from c in db.Candidats
                     where c.Admis == true
                      join a in db.Filieres on c.ID equals a.ID
                      where a.Nom == filiere
                      where c.Niveau == 3
                     select new ListFinal
                     {
                         Nom = c.Nom,
                         Prenom = c.Prenom,
                         Matricule = c.Matricule,

                         Num_dossier = c.Num_dossier,

                         Cin = c.Cin,


                     }).ToList();
            return x;
        }

        public IEnumerable<ListFinal> getListPrincipaleSup(string filiere)
        {
            var x = (from c in db.Candidats
                     where c.Admis == true
                     join a in db.Filieres on c.ID equals a.ID
                     where a.Nom == filiere
                     where c.Niveau == 4
                     select new ListFinal
                     {
                         Nom = c.Nom,
                         Prenom = c.Prenom,
                         Matricule = c.Matricule,

                         Num_dossier = c.Num_dossier,

                         Cin = c.Cin,


                     }).ToList();
            return x;
        }

        public IEnumerable<ListFinal> getListAttenteSup(string filiere)
        {
            var x = (from c in db.Candidats
                     where c.Admis == false
                     join a in db.Filieres on c.ID equals a.ID
                     join n in db.CouncourOrals on c.Cne equals n.Cne

                     where c.Niveau == 4
                     where a.Nom == filiere
                     orderby n.Classement
                    


                     select new ListFinal
                     {
                         Nom = c.Nom,
                         Prenom = c.Prenom,
                         Matricule = c.Matricule,

                         Num_dossier = c.Num_dossier,

                         Cin = c.Cin,


                     }).ToList();

            var NombrePlace = db.ConfigurationSelections.Where(c => c.Filiere.Equals(filiere) && c.Niveau == "4").SingleOrDefault().NbrPlaceListAtt;
            var resultat = x.Take(NombrePlace).ToList();
            return resultat;
        }
    }
}