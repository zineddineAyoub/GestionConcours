using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GestionConcours.Models
{
    public class Fichier
    {
        [Key]
        public int ID { get; set; }
        public string Cne { get; set; }
        public string nom { get; set; }
    }
}