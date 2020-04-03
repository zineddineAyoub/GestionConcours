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
        private IPreselectionService preselec;
        private ISelectionService selection;
        public AdminController(ISearch3Service search,ICorbeil3Service corbeil, IPreselectionService preselec,ISelectionService selection)
        {
            this.search = search;
            this.corbeil = corbeil;
            this.preselec = preselec;
            this.selection = selection;
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
/*
        public JsonResult GetListeFinal(string filiere)
        {
            // return data of filiere
           // var data = selection.getListPrincipale(filiere);
            GestionConcourDbContext a = new GestionConcourDbContext();
            var data1 = a.Candidats;
            var data2 = "hhh";
            var data = filiere;
            return Json(data1, JsonRequestBehavior.AllowGet);
        }*/

      public ActionResult ListFinaleSup()
        {
            return View();
        }

        public JsonResult GetListePrincipal(string filiere)
        {
            var data = selection.getListPrincipale(filiere);
             // var data = selection.getConfigurationSelection(filiere);
          

            return Json(data, JsonRequestBehavior.AllowGet);
        }


        public JsonResult GetListeAttente(string filiere)
        {
            var data = selection.getListAttente(filiere);
            // var data = selection.getConfigurationSelection(filiere);


            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetListePrincipalSup(string filiere)
        {
            var data = selection.getListPrincipaleSup(filiere);
            // var data = selection.getConfigurationSelection(filiere);


            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetListeAttenteSup(string filiere)
        {
            var data = selection.getListAttenteSup(filiere);
            // var data = selection.getConfigurationSelection(filiere);


            return Json(data, JsonRequestBehavior.AllowGet);
        }



    }
}