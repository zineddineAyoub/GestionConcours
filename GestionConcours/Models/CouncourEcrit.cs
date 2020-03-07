using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace GestionConcours.Models
{
    public class CouncourEcrit
    {
        [Key,ForeignKey("Candidat")]
        public string Cne { get; set; }
       
        public double NoteMath { get; set; }
        public double NoteSpecialite { get; set; }

        
        public virtual Candidat Candidat { get; set; }
        

    }
}