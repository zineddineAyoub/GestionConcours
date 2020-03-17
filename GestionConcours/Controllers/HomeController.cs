using GestionConcours.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GestionConcours.Controllers
{
    public class HomeController : Controller
    {
        private GestionConcourDbContext db = new GestionConcourDbContext();
        private Candidat candidat;

        public ActionResult Index()
        {
            if (Session["cne"] == null)
            {
                return RedirectToAction("Login", "Auth");
            }

            candidat = db.Candidats.Find(Session["cne"]);
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

        public ActionResult Image()
        {
            return View();
        }

        public ActionResult Epreuve()
        {
            return View();
        }

        public ActionResult Fiche()
        {
            return View();
        }

        public ActionResult Deconnexion()
        {
            return View();
        }



    }
}