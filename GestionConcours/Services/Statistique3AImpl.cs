using GestionConcours.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GestionConcours.Services
{
    public class Statistique3AImpl : IStatistiques3AService
    {
        private GestionConcourDbContext Bdd;
       
        public Statistique3AImpl()
        {
            Bdd = new GestionConcourDbContext();
        }

        public void Dispose()
        {
            Bdd.Dispose();
        }
        //statistique des convoques
        public int getCandidatConv(int idFilier, string diplome, int niveau)
        {
            return Bdd.Candidats.Count(c => c.Filiere.ID == idFilier && c.Diplome.Type.Contains(diplome) && c.Niveau == niveau && c.Convoque==true);
        }
        public int getNbCandidatPerDiplomeConv(string diplom, int niveau)
        {
            return Bdd.Candidats.Count(c => c.Diplome.Type.Contains(diplom) && c.Niveau == niveau && c.Convoque == true);
        }

        //statistique des preinscrits
        public int getCandidatPreisncrit(int idFilere, string diplome,int niveau)
        {
            return Bdd.Candidats.Count(c => c.Filiere.ID == idFilere && c.Diplome.Type.Contains(diplome) && c.Niveau==niveau);
        }
        public int getNbCandidatPerDiplome(string diplom, int niveau)
        {
            return Bdd.Candidats.Count(c => c.Diplome.Type.Contains(diplom) && c.Niveau == niveau);
        }

        //statistique des presents
        public int getCandidatPresent(int idFilier, string diplome, int niveau)
        {
            return Bdd.Candidats.Count(c => c.Filiere.ID == idFilier && c.Diplome.Type.Contains(diplome) && c.Niveau == niveau && c.Presence == true);
        }
        public int getNbCandidatPresentPerDiplome(string diplom, int niveau)
        {
            return Bdd.Candidats.Count(c => c.Diplome.Type.Contains(diplom) && c.Niveau == niveau && c.Presence == true);
        }

        //statistique des principales
        public int getCandidatPrincipale(int idFilier, string diplome, int niveau)
        {
            return Bdd.Candidats.Count(c => c.Filiere.ID == idFilier && c.Diplome.Type.Contains(diplome) && c.Niveau == niveau && c.Admis == true);
        }
        public int getNbCandidatPrincipalPerDiplome(string diplom, int niveau)
        {
            return Bdd.Candidats.Count(c => c.Diplome.Type.Contains(diplom) && c.Niveau == niveau && c.Admis == true);
        }

        public List<Filiere> GetFilieres()
        {
            return Bdd.Filieres.ToList();
        }

        public int getCandidatListeAtt(int idFilier, string diplome,string niveau)
        {
            Filiere f = Bdd.Filieres.FirstOrDefault(fl => fl.ID == idFilier);
            return Bdd.ConfigurationSelections.Count(c => c.Filiere == f.Nom && c.Niveau == niveau);
            
        }

    
    }
}