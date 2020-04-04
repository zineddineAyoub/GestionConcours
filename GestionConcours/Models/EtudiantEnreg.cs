using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

              //  ### Note ###
    // I encountered a prblm in the serialization of objects while using them in : return JSon(object, JsonRequestBehavior.AllowGet)
            // => solution :

namespace GestionConcours.Models
{
    public class EtudiantEnreg
    {
        string Cne, Cin, Nom, Prenom, Ville, Num_dossier, Photo, Matricule;
        Boolean Convoque;

        public EtudiantEnreg(string Cne, string Cin, string Nom, string Prenom, string Ville)
        {
            this.Cin = Cin;
            this.Cne = Cne;
            this.Nom = Nom;
            this.Prenom = Prenom;
            this.Ville = Ville;
       
        }
    }
}