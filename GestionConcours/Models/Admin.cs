using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GestionConcours.Models
{
    public class Admin
    {
        public int ID { get; set; }
        [Required]
        string Username { get; set; }
        [Required]
        string Password { get; set; }
    }
}