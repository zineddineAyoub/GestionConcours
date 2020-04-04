using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GestionConcours.Models
{
    public class UploadModel
    {
        public HttpPostedFileBase file { get; set; }
        public string matiere { get; set; }
        public string annee { get; set; }
    }
}