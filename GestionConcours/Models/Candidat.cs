using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace GestionConcours.Models
{
    public class Candidat
    {
        [Key]
        public string Cne { get; set; }
        public string Cin { get; set; }
        public string Nom { get; set; }
        public string Prenom { get; set; }
        public string Email { get; set; }
        public string Adresse { get; set; }
        public string LieuNaissance { get; set; }
        public string Telephone { get; set; }
        public string Nationalite { get; set; }
        public string Num_dossier { get; set; }
        public string Sexe { get; set; }
        public string Gsm { get; set; }
        public DateTime DateInscription { get; set; }
        public string Photo { get; set; }
        public Boolean Convoque { get; set; }
        public Boolean Admis { get; set; }
        public string Password { get; set; }


        //relation avec la classe annee universitaire oneToMany
        public virtual IList<AnneeUniversitaire> AnneeUniversitaires { get; set; }
        //relation avec la classe Baccalaureat oneToOne
        public virtual Baccalaureat Baccalaureat { get; set; }
        //relation avec la classe CouncourEcrit oneToOne
        public virtual CouncourEcrit CouncourEcrit { get; set; }
        //relation avec la classe councourOral oneToOne
        public virtual CouncourOral CouncourOral { get; set; }
        //relation avec la classe Diplome oneToOne
        public virtual Diplome Diplome { get; set; }

        [ForeignKey("Filiere")]
        public int ID { get; set; }
        public virtual Filiere Filiere { get; set; }


    }
}