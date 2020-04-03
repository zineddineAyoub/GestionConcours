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
        private IEpreuveService epreuve;
        private ICorrectionService corret;
		private IPreselectionService preselec;
   private ISelectionService selection;
    private IIndexService index;

	//	public AdminController(ISearch3Service search,ICorbeil3Service corbeil, IPreselectionService preselec, ICorrectionService corret, IIndexService index)

              
        public AdminController(ISearch3Service search,ICorbeil3Service corbeil,ICorrectionService corret, IPreselectionService preselec,ISelectionService selection,IEpreuveService epreuve, IIndexService index)
        {
			
            this.search = search;
            this.corbeil = corbeil;
            this.preselec = preselec;
            this.selection = selection;
			this.corret = corret;
            this.index = index;
            this.epreuve = epreuve;
		    }
        
        
        // GET: Admin
        public ActionResult Index()

        {
            if (Session["admin"] != null)
            {
                if (Session["admin"].Equals(true))
                {
                    IndexModel model = new IndexModel()
                    {
                        Info3 = index.getNbr_filiere(1, 3),
                        Info4 = index.getNbr_filiere(1, 4),
                        Gtr3 = index.getNbr_filiere(2, 3),
                        Gtr4 = index.getNbr_filiere(2, 4),
                        Indus3 = index.getNbr_filiere(3, 3),
                        Indus4 = index.getNbr_filiere(3, 4),
                        Gpmc3 = index.getNbr_filiere(4, 3),
                        Gpmc4 = index.getNbr_filiere(4, 4),

                        Inscrit3 = index.getInscrits_niv(3),
                        Inscrit4 = index.getInscrits_niv(4),

                        Suprim3 = index.getNbr_Corbeille(3),
                        Suprim4 = index.getNbr_Corbeille(4),
                        Suprim = index.getNbr_Corbeille(3) + index.getNbr_Corbeille(4),

                        Total = index.getAll()
                    };
                    
                    return View(model);
                }

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
				ConcourEcrit con = db.CouncourEcrits.Find(d);
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
				ConcourEcrit con = db.CouncourEcrits.Find(d);
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
				ConcourEcrit con = db.CouncourEcrits.Find(d);
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
				ConcourEcrit con = db.CouncourEcrits.Find(d);
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
				ConcourOral con = db.CouncourOrals.Find(d);
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
				ConcourOral con = db.CouncourOrals.Find(d);
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
				ConcourOral con = db.CouncourOrals.Find(d);
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
				ConcourOral con = db.CouncourOrals.Find(d);
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
            if (Session["admin"] != null)
            {
                if (Session["admin"].Equals(true))
                {
                    return View();
                }
            }
            return RedirectToAction("Login", "AdminAuth");
        }

        public ActionResult Preselection4()
        {
            if (Session["admin"] != null)
            {
                if (Session["admin"].Equals(true))
                {
                    return View();
                }
            }
            return RedirectToAction("Login", "AdminAuth");
        }

        public JsonResult CalculerPreselec4(string fil, string diplome, int Cs1, int Cs2, int Cs3, int Cs4, int Cs5, int Cs6, int Cbac, string seuil, int niv)
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
                CoeffS5 = Cs5,
                CoeffS6 = Cs6,
                NoteSeuil = Convert.ToDouble(seuil.Replace(".",","))
            };

            preselec.setConfig(conf, niv);
            preselec.calculerPreselec(niv, fil, diplome);
            var x = preselec.getConvoques(niv, fil, diplome);
            
            return Json(x, JsonRequestBehavior.AllowGet);
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
                NoteSeuil = Convert.ToDouble(seuil.Replace(".", ","))
            };

            preselec.setConfig(conf, niv);
            preselec.calculerPreselec(niv, fil, diplome);
            var x = preselec.getConvoques(niv, fil, diplome);

            return Json(x, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetConfig(string fil, string diplome)
        {
            ConfigurationPreselection list = preselec.getConfig(fil, diplome);
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetConvoquets(string fil, string diplome, int niv)
        {
            var list = preselec.getConvoques(niv, fil, diplome);
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetPourcentage(string fil, string diplome, int niv)
        {
            var list = preselec.getPourcentage(niv, fil, diplome);
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        // ##################################### Upload Epreuve #############################################
        public ActionResult Epreuve()
        {
            if (Session["admin"] != null)
            {
                if (Session["admin"].Equals(true))
                {
                    return View();
                }

            }
            return RedirectToAction("Login", "AdminAuth");
        }

        public JsonResult UploadEpreuve(UploadModel epr)
        {
            var result = epreuve.uploadEpreuve(epr);
            return Json(result, JsonRequestBehavior.AllowGet);
        }


        public ActionResult Selection()
        {
           return View();
        }

        public ActionResult SelectionL()
        {
            return View();
        }

     

        public JsonResult GetConfigurationSelection(string filiere,int nv)
        {
            var data = selection.getConfigurationSelection(filiere,nv);
          
            return Json(data, JsonRequestBehavior.AllowGet);
        }


        public JsonResult Test(string f, int cs, int np, int la, double nm, int cm, string cl, string nv)
        {
            return Json("niceTest", JsonRequestBehavior.AllowGet);
        }

        public JsonResult SetConfigurationSelection(string f, int cs, int np, int la, double nm,int cm,string cl,string nv)
        {
            ConfigurationSelection conf = new ConfigurationSelection();

            conf.Filiere = f;
            conf.CoeffMath = cm;
            conf.CoeffSpecialite = cs;
            conf.NbrPlace = np;
            conf.NoteMin = nm;
            conf.NbrPlaceListAtt = la;
            conf.TypeClassement =cl;
            conf.Niveau = nv;


         
             selection.updateConfigurationSelection(conf);
            if(nv=="3")
            {
                selection.calculeNoteGlobale(f);
            }
           
          var data = selection.selectStudents(f,nv);
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ListeFinal()
        {
            
            return View();
        }


      public ActionResult ListFinaleSup()
        {
            return View();
        }

        public JsonResult GetListePrincipal(string filiere)
        {
            var data = selection.getListPrincipale(filiere);
           return Json(data, JsonRequestBehavior.AllowGet);
        }


        public JsonResult GetListeAttente(string filiere)
        {
            var data = selection.getListAttente(filiere);
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetListePrincipalSup(string filiere)
        {
            var data = selection.getListPrincipaleSup(filiere);
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetListeAttenteSup(string filiere)
        {
            var data = selection.getListAttenteSup(filiere);
            return Json(data, JsonRequestBehavior.AllowGet);
        }
    }
}
 