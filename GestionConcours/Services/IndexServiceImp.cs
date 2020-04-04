using GestionConcours.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GestionConcours.Services
{
    public class IndexServiceImp : IIndexService
    {
        //Context
        GestionConcourDbContext db;

        //Constructor
        public IndexServiceImp()
        {
            db = new GestionConcourDbContext();
            db.Configuration.ProxyCreationEnabled = false;
        }

        public int getAll()
        {
            return db.Candidats.Count();
        }

        public int getInscrits_niv(int niv)
        {
            return db.Candidats.Where(c => c.Niveau == niv).Count();
        }

        public int getNbr_Corbeille(int niv)
        {
            var res = from c in db.Candidats
                      join cor in db.Corbeilles on c.Cne equals cor.CNE
                      where c.Niveau == niv                      
                      select c;

            return res.Count();
        }

        public int getNbr_filiere(int fil, int niv)
        {
            var res = from c in db.Candidats
                      join f in db.Filieres on c.ID equals f.ID
                      where c.Niveau == niv
                      && f.ID == fil
                      select c;

            return res.Count();
        }
    }
}