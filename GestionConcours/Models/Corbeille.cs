using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GestionConcours.Models
{
    public class Corbeille
    {
        public int ID { get; set; }
        Candidat Candidat { get; set; }
    }
}