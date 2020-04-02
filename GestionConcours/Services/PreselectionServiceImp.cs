using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using GestionConcours.Models;

namespace GestionConcours.Services
{
    public class PreselectionServiceImp : IPreselectionService
    {
        //Context
        GestionConcourDbContext db;

        //Constructor
        public PreselectionServiceImp()
        {
            db = new GestionConcourDbContext();
            db.Configuration.ProxyCreationEnabled = false;
        }
        

        public IEnumerable<PreselectionModel> getAll(int niveau)
        {
            var res = (from c in db.Candidats
                       join a in db.AnneeUniversitaires on c.Cne equals a.Cne
                       join b in db.Baccalaureats on c.Cne equals b.Cne
                       join d in db.Diplomes on c.Cne equals d.Cne
                       where !db.Corbeilles.Select(g => g.CNE).ToList().Contains(c.Cne)
                       where c.Niveau == niveau
                       select new PreselectionModel
                       {
                           Matricule = c.Matricule,
                           Cin = c.Cin,
                           Cne = c.Cne,
                           Nom = c.Nom,
                           Prenom = c.Prenom,
                           Sexe = c.Sexe,
                           Convoque = c.Convoque,
                           
                           NoteBac = b.NoteBac,
                           Note1 = a.Semestre1,
                           Note2 = a.Semestre2,
                           Note3 = a.Semestre3,
                           Note4 = a.Semestre4,
                           Note5 = a.Semestre5,
                           Note6 = a.Semestre6,

                           Type_dip = d.Type,
                           Ville_dip = d.VilleObtention,
                           Etablissement = d.Etablissement,

                           Redoublant1 = a.Redoublant1,
                           Redoublant2 = a.Redoublant2,
                           Redoublant3 = a.Redoublant3,
                           AnneUni1 = a.AnneUni1,
                           AnneUni2 = a.AnneUni2,
                           AnneUni3 = a.AnneUni3,

                           Filiere = db.Filieres.Where(f => f.ID == c.ID).FirstOrDefault().Nom,
                           Date_inscription = c.DateInscription,
                           
                       }).ToList();

            return res;
        }

        public IEnumerable<PreselectionModel> calculerPreselec(string fil, string diplome, int niveau)
        {
            ConfigurationPreselection conf = db.ConfigurationPreselections.Where(c => c.Filiere.Equals(fil) && c.TypeDiplome.Equals(diplome)).First();
            double sum1;
            int sumCoeff;

            if(niveau == 3)
            {
                sumCoeff = conf.CoeffBac + conf.CoeffS1 + conf.CoeffS2;
            }

            foreach (PreselectionModel c in this.getAll(niveau).ToList())
            {
                sum1 = 0;
                sum1 = (c.NoteBac * conf.CoeffBac) + (c.Note1 * conf.CoeffS1) + (c.Note2 * conf.CoeffS2) + (c.Note3 * conf.CoeffS3) + (c.Note4 * conf.CoeffS4);
                if(niveau == 4)
                {
                    sum1 += (c.Note5 * conf.CoeffS5) + (c.Note6 * conf.CoeffS6);
                }


            }


            return null;
        }

        //public double calculerNote()


        public void setConfig(ConfigurationPreselection config, int niveau)
        {
            var conf_tab = db.ConfigurationPreselections.Where(c => c.Filiere == config.Filiere && c.TypeDiplome == config.TypeDiplome).FirstOrDefault();
            if (conf_tab == null)
            {
                config.ID = 1;
                db.ConfigurationPreselections.Add(config);
                db.SaveChanges();
            }
            else
            {
                conf_tab.Filiere = config.Filiere;
                conf_tab.TypeDiplome = config.TypeDiplome;
                conf_tab.CoeffBac = config.CoeffBac;
                conf_tab.CoeffS1 = config.CoeffS1;
                conf_tab.CoeffS2 = config.CoeffS2;
                conf_tab.CoeffS3 = config.CoeffS3;
                conf_tab.CoeffS4 = config.CoeffS4;
                if (niveau == 4)
                {
                    conf_tab.CoeffS5 = config.CoeffS5;
                    conf_tab.CoeffS6 = config.CoeffS6;
                }

            }
        }

       
    }
}