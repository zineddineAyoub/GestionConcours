using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GestionConcours.ViewModels
{
    public class Statistique
    {
        public int nbDut { get; set; }
        public int nbDeug { get; set; }
        public int nbLicencePro { get; set; }
        public int nbLicenceSt { get; set; }
        public int nbLicenceFnd { get; set; }
        public string nomFiliere { get; set; }

        public int getTotal()
        {
            return nbDut + nbDeug + nbLicencePro + nbLicenceSt + nbLicenceFnd ;
        }
    }
    
}