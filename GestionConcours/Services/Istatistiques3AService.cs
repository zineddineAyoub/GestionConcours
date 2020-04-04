using GestionConcours.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GestionConcours.Services
{
    public interface IStatistiques3AService:IDisposable
    {
        int getCandidatPreisncrit(int idFilier,string diplome,int niveau);
        int getCandidatConv(int idFilier, string diplome, int niveau);
        List<Filiere> GetFilieres();
        int getNbCandidatPerDiplome(string diplom, int niveau);
        int getNbCandidatPerDiplomeConv(string diplom, int niveau);
        int getCandidatPresent(int idFilier, string diplome, int niveau);
        int getNbCandidatPresentPerDiplome(string diplom, int niveau);
        int getCandidatPrincipale(int idFilier, string diplome, int niveau);
        int getNbCandidatPrincipalPerDiplome(string diplom, int niveau);
        int getCandidatListeAtt(int idFilier, string diplome, int niveau);
        int getNbCandidatAttPerDiplome(string diplom, int niveau);




    }
}