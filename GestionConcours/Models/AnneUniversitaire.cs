using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace GestionConcours.Models
{
    public class AnneeUniversitaire
    {
        public int ID { get; set; }
        public double NoteS1 { get; set; }
        public double NosteS2 { get; set; }
        public DateTime Date { get; set; }
        public Boolean Redoublant { get; set; }

        [ForeignKey("Candidat")]
        public string Cne { get; set; }
        public virtual Candidat Candidat { get; set; }

    }
}
