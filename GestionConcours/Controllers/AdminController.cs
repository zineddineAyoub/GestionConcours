using GestionConcours.Models;
using GestionConcours.Services;
using GestionConcours.ViewModels;
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
        private IEpreuveService epreuve;
        private ICorrectionService corret;
        private IPreselectionService preselec;
        private ISelectionService selection;
        private IIndexService index;

        //	public AdminController(ISearch3Service search,ICorbeil3Service corbeil, IPreselectionService preselec, ICorrectionService corret, IIndexService index)

        public AdminController(ISearch3Service search, ICorbeil3Service corbeil, ICorrectionService corret, IPreselectionService preselec, ISelectionService selection, IEpreuveService epreuve, IIndexService index)

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
            return RedirectToAction("Login", "AdminAuth");
        }

        
        // ##################################### CORRECTION #############################################

        public ActionResult INFO()
        {
            string type_fil = "Informatique";
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
            string type_fil = "GTR";
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
            string type_fil = "GPMC";
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
            string type_fil = "Industriel";
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
            string type_fil = "Informatique";
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
            string type_fil = "GTR";
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
            string type_fil = "GPMC";
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

        //################################### RECHERCHE / CORBEILLE ########################

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
                NoteSeuil = Convert.ToDouble(seuil.Replace(".", ","))
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
        // ##################################### Diplome Scanné #############################################
        public ActionResult FichierScanne3()
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
        public ActionResult FichierScanne4()
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
        public JsonResult FichierScanne(string cne,int niveau)
        {
            var result = epreuve.diplomeFile(cne, niveau);
            return Json(result, JsonRequestBehavior.AllowGet);
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

        ///!!!!!!!!!!!!!!!!!!!!---------statistique-----------!!!!!!!!!!\\\
        public ActionResult Statistique3A()
        {
            if (Session["admin"] != null)
            {
                if (Session["admin"].Equals(true))
                {
                    List<Statistique> statistiquesPre = new List<Statistique>();
                    List<Statistique> statistiquesConvoque = new List<Statistique>();

                    List<Filiere> filieres = new List<Filiere>();

                    Statistique statPerDiplomPre = new Statistique();
                    Statistique statPerDiplomConv = new Statistique();

                    using (IStatistiques3AService dal = new Statistique3AImpl())
                    {

                        filieres = dal.GetFilieres();
                        foreach (var f in filieres)
                        {
                            statistiquesPre.Add(new Statistique
                            {
                                nbDeug = dal.getCandidatPreisncrit(f.ID, "deug", 3),
                                nbDut = dal.getCandidatPreisncrit(f.ID, "dut", 3),
                                nbLicenceFnd = dal.getCandidatPreisncrit(f.ID, "Lic.fnd", 3),
                                nbLicencePro = dal.getCandidatPreisncrit(f.ID, "Lic.pro", 3),
                                nbLicenceSt = dal.getCandidatPreisncrit(f.ID, "Lic.st", 3),
                                nomFiliere = f.Nom
                            });
                            statistiquesConvoque.Add(new Statistique
                            {
                                nbDeug = dal.getCandidatConv(f.ID, "deug", 3),
                                nbDut = dal.getCandidatConv(f.ID, "dut", 3),
                                nbLicenceFnd = dal.getCandidatConv(f.ID, "Lic.fnd", 3),
                                nbLicencePro = dal.getCandidatConv(f.ID, "Lic.pro", 3),
                                nbLicenceSt = dal.getCandidatConv(f.ID, "Lic.st", 3),
                                nomFiliere = f.Nom
                            });
                        }             
                        statPerDiplomPre = new Statistique
                        {
                            nbDeug = dal.getNbCandidatPerDiplome("deug", 3),
                            nbDut = dal.getNbCandidatPerDiplome("dut", 3),
                            nbLicenceFnd = dal.getNbCandidatPerDiplome("Lic.fnd", 3),
                            nbLicenceSt = dal.getNbCandidatPerDiplome("Lic.st", 3),
                            nbLicencePro = dal.getNbCandidatPerDiplome("Lic.pro", 3),
                        };
                        statPerDiplomConv = new Statistique
                        {
                            nbDeug = dal.getNbCandidatPerDiplomeConv("deug", 3),
                            nbDut = dal.getNbCandidatPerDiplomeConv("dut", 3),
                            nbLicenceFnd = dal.getNbCandidatPerDiplomeConv("Lic.fnd", 3),
                            nbLicenceSt = dal.getNbCandidatPerDiplomeConv("Lic.st", 3),
                            nbLicencePro = dal.getNbCandidatPerDiplomeConv("Lic.pro", 3),
                        };
                    }

                    ViewData["statistiquesPre"] = statistiquesPre;
                    ViewData["statistiquesConvoque"] = statistiquesConvoque;
                    ViewBag.statPerDiplomPre = statPerDiplomPre;
                    ViewBag.statPerDiplomConv = statPerDiplomConv;
                    return View();
                }
            }
              return RedirectToAction("Login", "AdminAuth");
        }
        public ActionResult Statistique4A()
        {
            if (Session["admin"] != null)
            {
                if (Session["admin"].Equals(true))
                {
                    List<Statistique> statistiquesPre = new List<Statistique>();
                    List<Statistique> statistiquesConvoque = new List<Statistique>();

                    List<Filiere> filieres = new List<Filiere>();

                    Statistique statPerDiplomPre = new Statistique();
                    Statistique statPerDiplomConv = new Statistique();

                    using (IStatistiques3AService dal = new Statistique3AImpl())
                    {

                        filieres = dal.GetFilieres();
                        foreach (var f in filieres)
                        {
                            statistiquesPre.Add(new Statistique
                            {
                                nbDeug = dal.getCandidatPreisncrit(f.ID, "deug", 4),
                                nbDut = dal.getCandidatPreisncrit(f.ID, "dut", 4),
                                nbLicenceFnd = dal.getCandidatPreisncrit(f.ID, "Lic.fnd", 4),
                                nbLicencePro = dal.getCandidatPreisncrit(f.ID, "Lic.pro", 4),
                                nbLicenceSt = dal.getCandidatPreisncrit(f.ID, "Lic.st", 4),
                                nomFiliere = f.Nom
                            });
                            statistiquesConvoque.Add(new Statistique
                            {
                                nbDeug = dal.getCandidatConv(f.ID, "deug", 4),
                                nbDut = dal.getCandidatConv(f.ID, "dut", 4),
                                nbLicenceFnd = dal.getCandidatConv(f.ID, "Lic.fnd", 4),
                                nbLicencePro = dal.getCandidatConv(f.ID, "Lic.pro", 4),
                                nbLicenceSt = dal.getCandidatConv(f.ID, "Lic.st", 4),
                                nomFiliere = f.Nom
                            });
                        }
                        statPerDiplomPre = new Statistique
                        {
                            nbDeug = dal.getNbCandidatPerDiplome("deug", 4),
                            nbDut = dal.getNbCandidatPerDiplome("dut", 4),
                            nbLicenceFnd = dal.getNbCandidatPerDiplome("Lic.fnd", 4),
                            nbLicenceSt = dal.getNbCandidatPerDiplome("Lic.st", 4),
                            nbLicencePro = dal.getNbCandidatPerDiplome("Lic.pro", 4),
                        };
                        statPerDiplomConv = new Statistique
                        {
                            nbDeug = dal.getNbCandidatPerDiplomeConv("deug", 4),
                            nbDut = dal.getNbCandidatPerDiplomeConv("dut", 4),
                            nbLicenceFnd = dal.getNbCandidatPerDiplomeConv("Lic.fnd", 4),
                            nbLicenceSt = dal.getNbCandidatPerDiplomeConv("Lic.st", 4),
                            nbLicencePro = dal.getNbCandidatPerDiplomeConv("Lic.pro", 4),
                        };
                    }

                    ViewData["statistiquesPre"] = statistiquesPre;
                    ViewData["statistiquesConvoque"] = statistiquesConvoque;
                    ViewBag.statPerDiplomPre = statPerDiplomPre;
                    ViewBag.statPerDiplomConv = statPerDiplomConv;
                    return View();
                }
            }
            return RedirectToAction("Login", "AdminAuth");
        }
        public ActionResult Statistique3ApresConcour()
        {
            if (Session["admin"] != null)
            {
                if (Session["admin"].Equals(true))
                {
                    List<Statistique> statistiquesPresent = new List<Statistique>();
                    List<Statistique> statistiquesPrincipal = new List<Statistique>();
                    List<Statistique> statistiquesAttente = new List<Statistique>();
                    

                    List<Filiere> filieres = new List<Filiere>();

                    Statistique statPerDiplomPresent = new Statistique();
                    Statistique statPerDiplomPrincipale = new Statistique();
                    Statistique statPerDiplomAtt = new Statistique();
                  

                    using (IStatistiques3AService dal = new Statistique3AImpl())
                    {

                        filieres = dal.GetFilieres();
                        foreach (var f in filieres)
                        {
                            statistiquesPresent.Add(new Statistique
                            {
                                nbDeug = dal.getCandidatPresent(f.ID, "deug", 3),
                                nbDut = dal.getCandidatPresent(f.ID, "dut", 3),
                                nbLicenceFnd = dal.getCandidatPresent(f.ID, "Lic.fnd", 3),
                                nbLicencePro = dal.getCandidatPresent(f.ID, "Lic.pro", 3),
                                nbLicenceSt = dal.getCandidatPresent(f.ID, "Lic.st", 3),
                                nomFiliere = f.Nom
                            });
                            statistiquesPrincipal.Add(new Statistique
                            {
                                nbDeug = dal.getCandidatPrincipale(f.ID, "deug", 3),
                                nbDut = dal.getCandidatPrincipale(f.ID, "dut", 3),
                                nbLicenceFnd = dal.getCandidatPrincipale(f.ID, "Lic.fnd", 3),
                                nbLicencePro = dal.getCandidatPrincipale(f.ID, "Lic.pro", 3),
                                nbLicenceSt = dal.getCandidatPrincipale(f.ID, "Lic.st", 3),
                                nomFiliere = f.Nom
                            });
                            statistiquesAttente.Add(new Statistique
                            {
                                nbDeug = dal.getCandidatListeAtt(f.ID, "deug", 3),
                                nbDut = dal.getCandidatListeAtt(f.ID, "dut", 3),
                                nbLicenceFnd = dal.getCandidatListeAtt(f.ID, "Lic.fnd", 3),
                                nbLicencePro = dal.getCandidatListeAtt(f.ID, "Lic.pro", 3),
                                nbLicenceSt = dal.getCandidatListeAtt(f.ID, "Lic.st", 3),
                                nomFiliere = f.Nom
                            });
                        }
                        statPerDiplomPresent = new Statistique
                        {
                            nbDeug = dal.getNbCandidatPresentPerDiplome("deug", 3),
                            nbDut = dal.getNbCandidatPresentPerDiplome("dut", 3),
                            nbLicenceFnd = dal.getNbCandidatPresentPerDiplome("Lic.fnd", 3),
                            nbLicenceSt = dal.getNbCandidatPresentPerDiplome("Lic.st", 3),
                            nbLicencePro = dal.getNbCandidatPresentPerDiplome("Lic.pro", 3),
                        };
                        statPerDiplomPrincipale = new Statistique
                        {
                            nbDeug = dal.getNbCandidatPrincipalPerDiplome("deug", 3),
                            nbDut = dal.getNbCandidatPrincipalPerDiplome("dut", 3),
                            nbLicenceFnd = dal.getNbCandidatPrincipalPerDiplome("Lic.fnd", 3),
                            nbLicenceSt = dal.getNbCandidatPrincipalPerDiplome("Lic.st", 3),
                            nbLicencePro = dal.getNbCandidatPrincipalPerDiplome("Lic.pro", 3),
                        };
                        statPerDiplomAtt = new Statistique
                        {
                            nbDeug = dal.getNbCandidatAttPerDiplome("deug", 3),
                            nbDut = dal.getNbCandidatAttPerDiplome("dut", 3),
                            nbLicenceFnd = dal.getNbCandidatAttPerDiplome("Lic.fnd", 3),
                            nbLicenceSt = dal.getNbCandidatAttPerDiplome("Lic.st", 3),
                            nbLicencePro = dal.getNbCandidatAttPerDiplome("Lic.pro", 3),
                        };

                    }

                    ViewData["statistiquesPresent"] = statistiquesPresent;
                    ViewData["statistiquesPrincipal"] = statistiquesPrincipal;
                    ViewData["statistiquesAttente"] = statistiquesAttente;
                    ViewBag.statPerDiplomPresent = statPerDiplomPresent;
                    ViewBag.statPerDiplomPrincipale = statPerDiplomPrincipale;
                    ViewBag.statPerDiplomAtt = statPerDiplomAtt;
                    return View();
                }
            }
            return RedirectToAction("Login", "AdminAuth");
        }
        public ActionResult Statistique4ApresConcour()
        {
            if (Session["admin"] != null)
            {
                if (Session["admin"].Equals(true))
                {
                    List<Statistique> statistiquesPresent = new List<Statistique>();
                    List<Statistique> statistiquesPrincipal = new List<Statistique>();
                    List<Statistique> statistiquesAttente = new List<Statistique>();


                    List<Filiere> filieres = new List<Filiere>();

                    Statistique statPerDiplomPresent = new Statistique();
                    Statistique statPerDiplomPrincipale = new Statistique();
                    Statistique statPerDiplomAtt = new Statistique();


                    using (IStatistiques3AService dal = new Statistique3AImpl())
                    {

                        filieres = dal.GetFilieres();
                        foreach (var f in filieres)
                        {
                            statistiquesPresent.Add(new Statistique
                            {
                                nbDeug = dal.getCandidatPresent(f.ID, "deug", 4),
                                nbDut = dal.getCandidatPresent(f.ID, "dut", 4),
                                nbLicenceFnd = dal.getCandidatPresent(f.ID, "Lic.fnd", 4),
                                nbLicencePro = dal.getCandidatPresent(f.ID, "Lic.pro", 4),
                                nbLicenceSt = dal.getCandidatPresent(f.ID, "Lic.st", 4),
                                nomFiliere = f.Nom
                            });
                            statistiquesPrincipal.Add(new Statistique
                            {
                                nbDeug = dal.getCandidatPrincipale(f.ID, "deug", 4),
                                nbDut = dal.getCandidatPrincipale(f.ID, "dut", 4),
                                nbLicenceFnd = dal.getCandidatPrincipale(f.ID, "Lic.fnd", 4),
                                nbLicencePro = dal.getCandidatPrincipale(f.ID, "Lic.pro", 4),
                                nbLicenceSt = dal.getCandidatPrincipale(f.ID, "Lic.st", 4),
                                nomFiliere = f.Nom
                            });
                            statistiquesAttente.Add(new Statistique
                            {
                                nbDeug = dal.getCandidatListeAtt(f.ID, "deug", 4),
                                nbDut = dal.getCandidatListeAtt(f.ID, "dut", 3),
                                nbLicenceFnd = dal.getCandidatListeAtt(f.ID, "Lic.fnd", 4),
                                nbLicencePro = dal.getCandidatListeAtt(f.ID, "Lic.pro", 4),
                                nbLicenceSt = dal.getCandidatListeAtt(f.ID, "Lic.st", 4),
                                nomFiliere = f.Nom
                            });
                        }
                        statPerDiplomPresent = new Statistique
                        {
                            nbDeug = dal.getNbCandidatPresentPerDiplome("deug", 4),
                            nbDut = dal.getNbCandidatPresentPerDiplome("dut", 4),
                            nbLicenceFnd = dal.getNbCandidatPresentPerDiplome("Lic.fnd", 4),
                            nbLicenceSt = dal.getNbCandidatPresentPerDiplome("Lic.st", 4),
                            nbLicencePro = dal.getNbCandidatPresentPerDiplome("Lic.pro", 4),
                        };
                        statPerDiplomPrincipale = new Statistique
                        {
                            nbDeug = dal.getNbCandidatPrincipalPerDiplome("deug", 4),
                            nbDut = dal.getNbCandidatPrincipalPerDiplome("dut", 4),
                            nbLicenceFnd = dal.getNbCandidatPrincipalPerDiplome("Lic.fnd", 4),
                            nbLicenceSt = dal.getNbCandidatPrincipalPerDiplome("Lic.st", 4),
                            nbLicencePro = dal.getNbCandidatPrincipalPerDiplome("Lic.pro", 4),
                        };
                        statPerDiplomAtt = new Statistique
                        {
                            nbDeug = dal.getNbCandidatAttPerDiplome("deug", 4),
                            nbDut = dal.getNbCandidatAttPerDiplome("dut", 4),
                            nbLicenceFnd = dal.getNbCandidatAttPerDiplome("Lic.fnd", 4),
                            nbLicenceSt = dal.getNbCandidatAttPerDiplome("Lic.st", 4),
                            nbLicencePro = dal.getNbCandidatAttPerDiplome("Lic.pro", 4),
                        };

                    }

                    ViewData["statistiquesPresent"] = statistiquesPresent;
                    ViewData["statistiquesPrincipal"] = statistiquesPrincipal;
                    ViewData["statistiquesAttente"] = statistiquesAttente;
                    ViewBag.statPerDiplomPresent = statPerDiplomPresent;
                    ViewBag.statPerDiplomPrincipale = statPerDiplomPrincipale;
                    ViewBag.statPerDiplomAtt = statPerDiplomAtt;
                    return View();
                }
            }
            return RedirectToAction("Login", "AdminAuth");
        }


        public JsonResult GetConfigurationSelection(string filiere, int nv)
        {
            var data = selection.getConfigurationSelection(filiere, nv);

            return Json(data, JsonRequestBehavior.AllowGet);
        }


        public JsonResult Test(string f, int cs, int np, int la, double nm, int cm, string cl, string nv)
        {
            return Json("niceTest", JsonRequestBehavior.AllowGet);
        }

        public JsonResult SetConfigurationSelection(string f, int cs, int np, int la, double nm, int cm, string cl, string nv)
        {
            ConfigurationSelection conf = new ConfigurationSelection();

            conf.Filiere = f;
            conf.CoeffMath = cm;
            conf.CoeffSpecialite = cs;
            conf.NbrPlace = np;
            conf.NoteMin = nm;
            conf.NbrPlaceListAtt = la;
            conf.TypeClassement = cl;
            conf.Niveau = nv;



            selection.updateConfigurationSelection(conf);
            if (nv == "3")
            {
                selection.calculeNoteGlobale(f);
            }

            var data = selection.selectStudents(f, nv);
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


        // ##################################### ENREGISTREMENT #############################################

        public ActionResult EnregistrementList()
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

        public JsonResult getEtudiantByNiveau(int niveau)
        {
            // var w = db.Candidats.Where(x => x.Niveau == niveau);



            var x = (from c in db.Candidats

                     join a in db.Filieres on c.ID equals a.ID

                     where c.Niveau == niveau

                     orderby c.Num_dossier

                     select new ListEnregistrement
                     {
                         Nom = c.Nom,
                         Prenom = c.Prenom,
                         Filiere = a.Nom,
                         Cin = c.Cin,
                         Num_dossier = c.Num_dossier,


                     }).ToList();

            db.Configuration.ProxyCreationEnabled = false;
            return Json(x, JsonRequestBehavior.AllowGet);
        }

        public JsonResult generateNumDossier(string cin) //generates num_dossier 
        {
            ViewBag.msg1 = "1";
            ViewBag.msg2 = "2";

            var c = db.Candidats.Where(x => x.Cin == cin).Count();

            if (c != 0) //cin exists in db
            {
                var c1 = db.Candidats.Where(x => x.Cin == cin).Single();

                if (c1.Num_dossier == 0)  //1st time checking in => num_dossier should be determined
                {
                    //2 cas :
                    //1: c'est le 1er candidat a confirmer la presence => num_dossier automatiquement = 1
                    //2: num_dossier depend de la valeur précédente de num_dossier

                    //2 :
                    //condition : Count(candidats avec num_dossier!=null) > 0 
                    var c3 = db.Candidats.Where(x => x.Num_dossier != 0).Count();
                    if (c3 > 0)
                    {
                        var c2 = (from e in db.Candidats
                                  orderby e.Num_dossier descending
                                  select e).Take(1).Single();   //takes the biggest num_dossier and increments it

                        var max = (from e in db.Candidats
                                            orderby e.Num_dossier descending
                                            select e).Take(1).Single();

                        int nbr = max.Num_dossier + 1;
                        c1.Num_dossier = nbr;
                    }
                    //1:                     
                    else
                    {
                        c1.Num_dossier = 1;
                    }

                    c1.Presence = true;
                }
                else
                {   //candidat déjà confirmé (présence)
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


