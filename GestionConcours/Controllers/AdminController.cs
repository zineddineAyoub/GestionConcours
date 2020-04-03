using GestionConcours.Models;
using GestionConcours.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;

namespace GestionConcours.Controllers
{
    public class AdminController : Controller
    {
		
		private ISearch3Service search;
        private ICorbeil3Service corbeil;
        private ICorrectionService corret;
		private IPreselectionService preselec;


		public AdminController(ISearch3Service search,ICorbeil3Service corbeil, IPreselectionService preselec, ICorrectionService corret)
        {
			
            this.search = search;
            this.corbeil = corbeil;
            this.preselec = preselec;
			this.corret = corret;

		}

		
		// GET: Admin
		public ActionResult Index()
        {
            if (Session["admin"] != null)
            {
                if (Session["admin"].Equals(true))
                    return View();

            }
            return RedirectToAction("Login","AdminAuth");
        }

        public ActionResult Recherche()
        {
            if (Session["admin"] != null)
            {
                if (Session["admin"].Equals(true))
                {
                    var x = search.generalSearch(3);
                    return View(x);
                }

            }
            return RedirectToAction("Login", "AdminAuth");
        }





		public ActionResult Recherche4()
        {
            if (Session["admin"] != null)
            {
                if (Session["admin"].Equals(true))
                {
                    var x = search.generalSearch(4);
                    return View(x);
                }

            }
            return RedirectToAction("Login", "AdminAuth");
        }

		// ##################################### CORRECTION #############################################

		public ActionResult INFO()
		{
			string type_fil = "informatique";
			if (Session["admin"] != null)
			{

				if (Session["admin"].Equals(true))
				{
					var x = corret.corr(type_fil);
					return View(x);
					
				}

			}
			return RedirectToAction("Login", "AdminAuth");
		}



		[HttpPost]
		public ActionResult INFO_Post()
		{
			GestionConcourDbContext db = new GestionConcourDbContext();

			var NOTES = Request["etudiants.NoteMath"].ToString().Split(',');

			var CNE = Request["etudiants.Cne"].ToString().Split(',');

			var NOTES2 = Request["etudiants.NoteSpecialite"].ToString().Split(',');
			//var CIN = Request["etudiants.Cin"].ToString().Split(',');
			String d;
			for (int i = 0; i < CNE.Length; i++)
			{
				d = CNE[i].ToString();
				CouncourEcrit con = db.CouncourEcrits.Find(d);
				con.NoteMath = Double.Parse(NOTES[i]);
				con.NoteSpecialite = Double.Parse(NOTES2[i]);
				db.SaveChanges();
			}

			return RedirectToAction("index");

		}



		public ActionResult GTR()
		{
			string type_fil = "gtr";
			if (Session["admin"] != null)
			{

				if (Session["admin"].Equals(true))
				{
					var x = corret.corr(type_fil);
					return View(x);

				}

			}
			return RedirectToAction("Login", "AdminAuth");
		}



		[HttpPost]
		public ActionResult GTR_Post()
		{
			GestionConcourDbContext db = new GestionConcourDbContext();

			var NOTES = Request["etudiants.NoteMath"].ToString().Split(',');

			var CNE = Request["etudiants.Cne"].ToString().Split(',');

			var NOTES2 = Request["etudiants.NoteSpecialite"].ToString().Split(',');
			//var CIN = Request["etudiants.Cin"].ToString().Split(',');
			String d;
			for (int i = 0; i < CNE.Length; i++)
			{
				d = CNE[i].ToString();
				CouncourEcrit con = db.CouncourEcrits.Find(d);
				con.NoteMath = Double.Parse(NOTES[i]);
				con.NoteSpecialite = Double.Parse(NOTES2[i]);
				db.SaveChanges();
			}

			return RedirectToAction("index");

		}

		public ActionResult GPMC()
		{
			string type_fil = "gpmc";
			if (Session["admin"] != null)
			{

				if (Session["admin"].Equals(true))
				{
					var x = corret.corr(type_fil);
					return View(x);

				}

			}
			return RedirectToAction("Login", "AdminAuth");
		}



		[HttpPost]
		public ActionResult GPMC_Post()
		{
			GestionConcourDbContext db = new GestionConcourDbContext();

			var NOTES = Request["etudiants.NoteMath"].ToString().Split(',');

			var CNE = Request["etudiants.Cne"].ToString().Split(',');

			var NOTES2 = Request["etudiants.NoteSpecialite"].ToString().Split(',');
			//var CIN = Request["etudiants.Cin"].ToString().Split(',');
			String d;
			for (int i = 0; i < CNE.Length; i++)
			{
				d = CNE[i].ToString();
				CouncourEcrit con = db.CouncourEcrits.Find(d);
				con.NoteMath = Double.Parse(NOTES[i]);
				con.NoteSpecialite = Double.Parse(NOTES2[i]);
				db.SaveChanges();
			}

			return RedirectToAction("index");

		}

		public ActionResult INDUS()
		{
			string type_fil = "indus";
			if (Session["admin"] != null)
			{

				if (Session["admin"].Equals(true))
				{
					var x = corret.corr(type_fil);
					return View(x);

				}

			}
			return RedirectToAction("Login", "AdminAuth");
		}



		[HttpPost]
		public ActionResult INDUS_Post()
		{
			GestionConcourDbContext db = new GestionConcourDbContext();

			var NOTES = Request["etudiants.NoteMath"].ToString().Split(',');

			var CNE = Request["etudiants.Cne"].ToString().Split(',');

			var NOTES2 = Request["etudiants.NoteSpecialite"].ToString().Split(',');
			//var CIN = Request["etudiants.Cin"].ToString().Split(',');
			String d;
			for (int i = 0; i < CNE.Length; i++)
			{
				d = CNE[i].ToString();
				CouncourEcrit con = db.CouncourEcrits.Find(d);
				con.NoteMath = Double.Parse(NOTES[i]);
				con.NoteSpecialite = Double.Parse(NOTES2[i]);
				db.SaveChanges();
			}

			return RedirectToAction("index");

		}



		public ActionResult INFO4()
		{
			string type_fil = "informatique";
			if (Session["admin"] != null)
			{

				if (Session["admin"].Equals(true))
				{
					var x = corret.corr4(type_fil);
					return View(x);

				}

			}
			return RedirectToAction("Login", "AdminAuth");
		}



		[HttpPost]
		public ActionResult INFO_Post4()
		{
			GestionConcourDbContext db = new GestionConcourDbContext();

			var NOTES = Request["etudiants.Classement"].ToString().Split(',');
			var CNE = Request["etudiants.Cne"].ToString().Split(',');
			String d;
			for (int i = 0; i < CNE.Length; i++)
			{
				d = CNE[i].ToString();
				CouncourOral con = db.CouncourOrals.Find(d);
				con.Classement = Int32.Parse(NOTES[i]);
				db.SaveChanges();
			}

			return RedirectToAction("index");

		}


		public ActionResult GTR4()
		{
			string type_fil = "gtr";
			if (Session["admin"] != null)
			{

				if (Session["admin"].Equals(true))
				{
					var x = corret.corr4(type_fil);
					return View(x);

				}

			}
			return RedirectToAction("Login", "AdminAuth");
		}



		[HttpPost]
		public ActionResult GTR_Post4()
		{
			GestionConcourDbContext db = new GestionConcourDbContext();

			var NOTES = Request["etudiants.Classement"].ToString().Split(',');
			var CNE = Request["etudiants.Cne"].ToString().Split(',');
			String d;
			for (int i = 0; i < CNE.Length; i++)
			{
				d = CNE[i].ToString();
				CouncourOral con = db.CouncourOrals.Find(d);
				con.Classement = Int32.Parse(NOTES[i]);
				db.SaveChanges();
			}

			return RedirectToAction("index");

		}


		public ActionResult GPMC4()
		{
			string type_fil = "gpmc";
			if (Session["admin"] != null)
			{

				if (Session["admin"].Equals(true))
				{
					var x = corret.corr4(type_fil);
					return View(x);

				}

			}
			return RedirectToAction("Login", "AdminAuth");
		}



		[HttpPost]
		public ActionResult GPMC_Post4()
		{
			GestionConcourDbContext db = new GestionConcourDbContext();

			var NOTES = Request["etudiants.Classement"].ToString().Split(',');
			var CNE = Request["etudiants.Cne"].ToString().Split(',');
			String d;
			for (int i = 0; i < CNE.Length; i++)
			{
				d = CNE[i].ToString();
				CouncourOral con = db.CouncourOrals.Find(d);
				con.Classement = Int32.Parse(NOTES[i]);
				db.SaveChanges();
			}

			return RedirectToAction("index");

		}



		public ActionResult INDUS4()
		{
			string type_fil = "indus";
			if (Session["admin"] != null)
			{

				if (Session["admin"].Equals(true))
				{
					var x = corret.corr4(type_fil);
					return View(x);

				}

			}
			return RedirectToAction("Login", "AdminAuth");
		}



		[HttpPost]
		public ActionResult INDUS_Post4()
		{
			GestionConcourDbContext db = new GestionConcourDbContext();

			var NOTES = Request["etudiants.Classement"].ToString().Split(',');
			var CNE = Request["etudiants.Cne"].ToString().Split(',');
			String d;
			for (int i = 0; i < CNE.Length; i++)
			{
				d = CNE[i].ToString();
				CouncourOral con = db.CouncourOrals.Find(d);
				con.Classement = Int32.Parse(NOTES[i]);
				db.SaveChanges();
			}

			return RedirectToAction("index");

		}












		public ActionResult Corbeil()
        {
            if (Session["admin"] != null)
            {
                if (Session["admin"].Equals(true))
                {
                    var x = corbeil.afficheCorbeil(3);
                    return View(x);
                }

            }
            return RedirectToAction("Login", "AdminAuth");
        }

        public ActionResult Corbeil4()
        {
            if (Session["admin"] != null)
            {
                if (Session["admin"].Equals(true))
                {
                    var x = corbeil.afficheCorbeil(4);
                    return View(x);
                }

            }
            return RedirectToAction("Login", "AdminAuth");
        }

        public JsonResult RestoreStudent(string cne,int Niveau)
        {
            var x = corbeil.restoreCorbeil(cne,Niveau);
            return Json(x, JsonRequestBehavior.AllowGet);
        }

        public JsonResult SearchCriteria(string Criteria,string Key,string Diplome,string Filiere,int Niveau)
        {
            var x = search.specifiedSearch(Criteria, Key, Diplome, Filiere,Niveau);
            return Json(x, JsonRequestBehavior.AllowGet);
        }

        public JsonResult SearchCriteriaCorb(string Criteria, string Key, string Diplome, string Filiere,int Niveau)
        {
            var x = corbeil.searchCriteria(Criteria, Key, Diplome, Filiere,Niveau);
            return Json(x, JsonRequestBehavior.AllowGet);
        }

        //delete candidat
        public JsonResult deleteStudent(string cne,int Niveau)
        {
            var x = search.deleteCandidat(cne,Niveau);
            return Json(x, JsonRequestBehavior.AllowGet);
        }

        public JsonResult conformeStudent(string cne,int Niveau)
        {
            var x = search.conformCandidat(cne,Niveau);
            return Json(x, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Enregistrement()
        {
            if (Session["admin"] != null)
            {
                if (Session["admin"].Equals(true))
                    return View();

            }
            return RedirectToAction("Login", "AdminAuth");
        }
        // ##################################### PRESELECTION #############################################

        public ActionResult Preselection()
        {
            return View();
        }

        public JsonResult CalculerPreselec(string fil, string diplome, int Cs1, int Cs2, int Cs3, int Cs4, int Cbac, string seuil, int niv)
        {
            ConfigurationPreselection conf = new ConfigurationPreselection()
            {
                Filiere = fil,
                TypeDiplome = diplome,
                CoeffBac = Cbac,
                CoeffS1 = Cs1,
                CoeffS2 = Cs2,
                CoeffS3 = Cs3,
                CoeffS4 = Cs4,
                NoteSeuil = Convert.ToInt32(seuil)
            };

            preselec.setConfig(conf, niv);
            
            return Json(conf, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Preselection4()
        {
            return View();
        }
    }
}
 