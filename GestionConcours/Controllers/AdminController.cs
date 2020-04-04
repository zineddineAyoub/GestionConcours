using GestionConcours.Models;
using GestionConcours.Services;
using System;
using System.Collections.Generic;
using System.Data.Entity.SqlServer;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;

namespace GestionConcours.Controllers
{
    public class AdminController : Controller
    {
        private GestionConcourDbContext db = new GestionConcourDbContext();
        private ISearch3Service search;
        private ICorbeil3Service corbeil;
        private IPreselectionService preselec;
        public AdminController(ISearch3Service search, ICorbeil3Service corbeil, IPreselectionService preselec)
        {
            this.search = search;
            this.corbeil = corbeil;
            this.preselec = preselec;
        }
        // GET: Admin
        public ActionResult Index()
        {
            if (Session["admin"] != null)
            {
                if (Session["admin"].Equals(true))
                    return View();

            }
            return RedirectToAction("Login", "AdminAuth");
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

        public JsonResult RestoreStudent(string cne, int Niveau)
        {
            var x = corbeil.restoreCorbeil(cne, Niveau);
            return Json(x, JsonRequestBehavior.AllowGet);
        }

        public JsonResult SearchCriteria(string Criteria, string Key, string Diplome, string Filiere, int Niveau)
        {
            var x = search.specifiedSearch(Criteria, Key, Diplome, Filiere, Niveau);
            return Json(x, JsonRequestBehavior.AllowGet);
        }

        public JsonResult SearchCriteriaCorb(string Criteria, string Key, string Diplome, string Filiere, int Niveau)
        {
            var x = corbeil.searchCriteria(Criteria, Key, Diplome, Filiere, Niveau);
            return Json(x, JsonRequestBehavior.AllowGet);
        }

        //delete candidat
        public JsonResult deleteStudent(string cne, int Niveau)
        {
            var x = search.deleteCandidat(cne, Niveau);
            return Json(x, JsonRequestBehavior.AllowGet);
        }

        public JsonResult conformeStudent(string cne, int Niveau)
        {
            var x = search.conformCandidat(cne, Niveau);
            return Json(x, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Enregistrement()
        {
            /*
            if (Session["admin"] != null)
            {
                if (Session["admin"].Equals(true))
                    return View();

            }
            return RedirectToAction("Login", "AdminAuth");
            */
            return View();
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

        // ##################################### ENREGISTREMENT #############################################

        public JsonResult Enregistrement3A(string cin)
        {
            var c = db.Candidats.Where(x => x.Cin == cin);
            db.Configuration.ProxyCreationEnabled = false;

            return Json(c, JsonRequestBehavior.AllowGet);  //returns candidat
        }

        public JsonResult getFiliere(int id)
        {
            var c = db.Filieres.Where(x => x.ID == id);
            db.Configuration.ProxyCreationEnabled = false;

            return Json(c, JsonRequestBehavior.AllowGet);   //returns filiere
        }

        public JsonResult getDiplome(string cne)
        {
            var c = db.Diplomes.Where(x => x.Cne == cne);
            db.Configuration.ProxyCreationEnabled = false;

            return Json(c, JsonRequestBehavior.AllowGet);   //returns diplome
        }

        public JsonResult getNumDossier(string cin)
        {
            var c = db.Candidats.Where(x => x.Cin == cin);
            db.Configuration.ProxyCreationEnabled = false;

            return Json(c, JsonRequestBehavior.AllowGet);   //returns num_dossier
        }

        public JsonResult generateNumDossier(string cin) //generates num_dossier 
        {
            ViewBag.msg1 = "1";
            ViewBag.msg2 = "2";
            
            var c = db.Candidats.Where(x => x.Cin == cin).Count();

            if (c != 0) //cin exists in db
            {
                var c1 = db.Candidats.Where(x => x.Cin == cin).Single();

                if(c1.Num_dossier == null)  //1st time checking in => num_dossier should be determined
                {  
                    //2 cas :
                        //1: c'est le 1er candidat a confirmer la presence => num_dossier automatiquement = 1
                        //2: num_dossier depend de la valeur précédente de num_dossier

                    //2 :
                        //condition : Count(candidats avec num_dossier!=null) > 0 
                    var c3 = db.Candidats.Where(x => x.Num_dossier != null).Count();
                    if (c3 > 0)
                    {
                        var c2 = (from e in db.Candidats
                                  orderby e.Num_dossier descending
                                  select e).Take(1).Single();   //takes the biggest num_dossier and increments it

                        int nbr = Int32.Parse(c2.Num_dossier) + 1;
                        c1.Num_dossier = nbr.ToString();
                    }
                    //1:                     
                    else
                    {
                        c1.Num_dossier = "1";
                    }

                    c1.Presence = true;                    
                }
                else{   //candidat déjà confirmé (présence)
                    ViewBag.msg1 = "Numéro de dossier déjà attribué";
                }
            }
            else    //cin doesn't exist f bd
            {
                ViewBag.msg2 = cin + " CIN non valide ";
            }

            db.SaveChanges();

            db.Configuration.ProxyCreationEnabled = false;
            return Json(db.Candidats.Where(x => x.Cin == cin), JsonRequestBehavior.AllowGet);                
        }

    }


}