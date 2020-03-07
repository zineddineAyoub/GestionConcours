using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace GestionConcours.Models
{
    public class Baccalaureat
    {
        [Key, ForeignKey("Candidat")]
        public string Cne { get; set; }
        public string Type { get; set; }
        public DateTime DateObtention { get; set; }
        public double Note { get; set; }
        public string Mention { get; set; }


        public virtual Candidat Candidat { get; set; }

    }
}