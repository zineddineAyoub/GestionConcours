using GestionConcours.Models;
using Rotativa.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GestionConcours.Controllers
{
    public class HomeController : Controller
    {
        GestionConcourDbContext cl = new GestionConcourDbContext();

        public ActionResult Index()
        {
            if (Session["cne"] == null)
            {
                return RedirectToAction("Login", "Auth");
            }
            return View();
        }

        public ActionResult Profil()
        {
            return View();
        }

        public ActionResult ModifierPersonel()
        {
            return View();
        }

        public ActionResult ModifierDiplome()
        {
            return View();
        }

        public ActionResult ModifierBac()
        {
            return View();
        }

        public ActionResult ModifierFiliere()
        {
            return View();
        }

        public ActionResult Image()
        {
            return View();
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