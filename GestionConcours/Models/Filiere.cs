using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Web;

namespace GestionConcours.Models
{
    public class Filiere
    {
        public int ID { get; set; }
        public string Nom { get; set; }

        //relation manyToOne avec la classe Candidat 
        public virtual IList<Candidat> Candidats { get; set; }
    }
}