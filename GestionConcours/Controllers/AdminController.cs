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
        private IIndexService index;
        public AdminController(ISearch3Service search,ICorbeil3Service corbeil, IPreselectionService preselec, IIndexService index)
        {
            this.search = search;
            this.corbeil = corbeil;
            this.preselec = preselec;
            this.index = index;
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
        
    }
}