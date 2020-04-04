using GestionConcours.Models;
using GestionConcours.Services;
using GestionConcours.ViewModels;
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
        
        public AdminController (ISearch3Service search,ICorbeil3Service corbeil, IPreselectionService preselec)
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

    }
}