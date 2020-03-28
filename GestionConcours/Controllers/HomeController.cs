
﻿using GestionConcours.Models;

using GestionConcours.Models;
using Rotativa.Options;

using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;

namespace GestionConcours.Controllers
{
    public class HomeController : Controller
    {

        GestionConcourDbContext cl = new GestionConcourDbContext();
        private GestionConcourDbContext db = new GestionConcourDbContext();
        private Candidat candidat;
        public ActionResult Index()
        {
            if (Session["cne"] == null)
            {
                return RedirectToAction("Login", "Auth");
            }

            candidat = db.Candidats.Find(Session["cne"]);
            Session["photo"] = candidat.Photo;
            Session["nom"] = candidat.Nom;
            Session["prenom"] = candidat.Prenom;
            return View();
        }

        public ActionResult Profil()
        {
            return View();
        }

        [HttpGet]
        public ActionResult ModifierPersonel()
        {
            if (Session["cne"] == null)
            {
                return RedirectToAction("Login", "Auth");
            }

            Candidat candidat = db.Candidats.Find(Session["cne"]);
            Session["photo"] = candidat.Photo;
            return View(candidat);
        }
        [HttpPost]
        public ActionResult ModifierPersonel(Candidat candidat)
        {
            var originalCandiat = (from c in db.Candidats where c.Cne == candidat.Cne select c).First();
            originalCandiat.Nom = candidat.Nom;
            originalCandiat.Prenom = candidat.Prenom;
            originalCandiat.Cin = candidat.Cin;
            originalCandiat.DateNaissance = candidat.DateNaissance;
            originalCandiat.LieuNaissance = candidat.LieuNaissance;
            originalCandiat.Nationalite = candidat.Nationalite;
            originalCandiat.Gsm = candidat.Gsm;
            originalCandiat.Telephone = candidat.Telephone;
            originalCandiat.Adresse = candidat.Adresse;
            originalCandiat.Ville = candidat.Ville;
            originalCandiat.Email = candidat.Email;
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult ModifierDiplome()
        {
			ViewBag.e = Session["niveau"];
			return View();			
        }
        [HttpPost]
        public ActionResult ModifierDiplome(Diplome diplome, AnneeUniversitaire uni)
        {
            GestionConcourDbContext db = new GestionConcourDbContext();
            string cne = Session["cne"].ToString();
            var x = db.Diplomes.Where(c => c.Cne == cne).SingleOrDefault();
            x.Type = diplome.Type;
            x.Etablissement = diplome.Etablissement;
            x.VilleObtention = diplome.VilleObtention;
            x.NoteDiplome = diplome.NoteDiplome;
            x.Specialite = diplome.Specialite;
            db.SaveChanges();

            var y = db.AnneeUniversitaires.Where(a => a.Cne == cne).SingleOrDefault();
            y.Semestre1 = uni.Semestre1;
            y.Semestre2 = uni.Semestre2;
            y.Semestre3 = uni.Semestre3;
            y.Semestre4 = uni.Semestre4;
            y.Redoublant1 = uni.Redoublant1;
            y.Redoublant2 = uni.Redoublant2;
            y.AnneUni1 = uni.AnneUni1;
            y.AnneUni2 = uni.AnneUni2;
            if (Session["niveau"].ToString() == "4")
            {
                y.Semestre5 = uni.Semestre5;
                y.Semestre6 = uni.Semestre6;
                y.Redoublant3 = uni.Redoublant3;
                y.AnneUni3 = uni.AnneUni3;
            }
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult ModifierBac()
        {
            if (Session["cne"] == null)
            {
                return RedirectToAction("Login", "Auth");
            }

            Baccalaureat bac = db.Baccalaureats.Find(Session["cne"]);

            List<SelectListItem> listTypeBac = new List<SelectListItem>
            {
                new SelectListItem{Text="SMA", Value="SMA"},
                new SelectListItem{Text="SMB", Value="SMB"},
                new SelectListItem{Text="SVT", Value="SVT"},
                new SelectListItem{Text="PC", Value="PC"}
            };
            List<SelectListItem> listMention = new List<SelectListItem>
            {
                new SelectListItem{Text="Très Bien", Value="Très Bien"},
                new SelectListItem{Text="Bien", Value="Bien"},
                new SelectListItem{Text="Assez Bien", Value="Assez Bien"},
                new SelectListItem{Text="Passable", Value="Passable"}
            };

            ViewBag.typeBac = listTypeBac;
            ViewBag.mention = listMention;

            return View(bac);
        }

        [HttpPost]
        public ActionResult ModifierBac([Bind(Include = "Cne,TypeBac,DateObtentionBac,NoteBac,MentionBac")]Baccalaureat bac)
        {
            if (ModelState.IsValid)
            {
                db.Entry(bac).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(bac);


        }


        public ActionResult ModifierFiliere()
        {
            return View();
        }

		
		public JsonResult Image(HttpPostedFileBase file)
        {
            string response=" ";
            if (file != null && file.ContentLength > 0)
                try
                {
                    String extension = Path.GetExtension(file.FileName);
                    Random r = new Random();
                    int rInt = r.Next(0, 10000);
                    string fileName = rInt.ToString() + extension.ToLower();
                    string path = Path.Combine(Server.MapPath("~/Pictures/userPic"), fileName);
                    file.SaveAs(path);
                    string cne = Session["cne"].ToString();
                    GestionConcourDbContext db = new GestionConcourDbContext();
                    var x = db.Candidats.Where(c => c.Cne == cne).SingleOrDefault();
                    x.Photo = fileName;
                    db.SaveChanges();
                    Session["photo"] = fileName;
                    response = fileName;
                }
                catch (Exception ex)
                {
                    response = "icon.png";
                }
            else
            {
                response = "icon.png";
            }
            return Json(response, JsonRequestBehavior.AllowGet);
        }

        
        // une fois on clique sur le lien download on appelle cette action qui va telecharger le fichier 
        // que son nom est passé en parametre depuis le dossier Epreuves
        public FileResult Download(string fichier)
        {
            
            string fullName = Server.MapPath("~/Epreuves/" + fichier);
            byte[] fileBytes = System.IO.File.ReadAllBytes(fullName);
            return File(fileBytes, System.Net.Mime.MediaTypeNames.Application.Octet, fichier);
        }


       // affiche tous les donnes extstantes dans la table épreuve
        public ActionResult Epreuve()
        {
            var data = cl.Epreuves;
            return View(data);
        }

        // Afficher le contenue de la convocation 
        public ActionResult Fiche(string id,string click="empty")
        {
            // Pour supprimer le header de la page de la convocation
            if(click.Equals("imprimer"))
            {
                ViewBag.Imprimer = "imprimer";
            }
        
            if(id==null)
            {
                id = Session["cne"].ToString();
            }
             
            Candidat data = GetCandidat(id);
            return View(data);
        }

       

        public Candidat GetCandidat(string cne)
        {
            
            var candidat = cl.Candidats.Include("Filiere").Include("Diplome").Where(c => c.Cne == cne).SingleOrDefault();
            return candidat as Candidat;
        }


        // Responsable d'imprimer le fiche mais les sessions ne marchent pas lors de l'appel de Fiche()
         public ActionResult ImprimerConvocation()
         {
           return new Rotativa.ActionAsPdf("Fiche", new { id = Session["cne"], click = "imprimer" })
           {
               PageSize = Size.A4,
               CustomSwitches = "--disable-smart-shrinking",
               PageOrientation = Orientation.Portrait,
               PageMargins = new Margins(0, 0, 0, 0),
               PageWidth = 210,
               PageHeight = 220
           };
         }

        public ActionResult Deconnexion()
        {
            return View();
        }

    }
}