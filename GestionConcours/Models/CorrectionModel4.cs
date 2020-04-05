using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GestionConcours.Models
{
	public class CorrectionModel4
	{
		public int Num_dossier { get; set; }
		public string Cin { get; set; }
		public string Nom { get; set; }
		public string Prenom { get; set; }
		public string Etablissement { get; set; }
		public string Diplome { get; set; }
		public string Filiere { get; set; }
		public string VilleObtention { get; set; }
		public string Cne { get; set; }
		public string Specialite { get; set; }
		public double Classement { get; set; }
	}
}