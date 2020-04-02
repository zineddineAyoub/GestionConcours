﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace GestionConcours.Models
{
    public class ConcourOral
    {
        [Key, ForeignKey("Candidat")]
        public string Cne { get; set; }
        public int Classement { get; set; }


        public virtual Candidat Candidat { get; set; }
    }
}