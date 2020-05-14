using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GestionConcours.Models
{
    public class ListEnregistrement
    {

        public string Nom { get; set; }
        public string Prenom { get; set; }
        public string Filiere { get; set; }
        public string Cin { get; set; }
        public int Num_dossier { get; set; }
       
    }
}