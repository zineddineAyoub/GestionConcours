using GestionConcours.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GestionConcours.Controllers
{
    public class HomeController : Controller
    {
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
			ViewBag.e = Session["niveau"];
			return View();			
        }
		[HttpPost]
		public ActionResult ModifierDiplome(Diplome diplome, AnneeUniversitaire uni)
		{
			GestionConcourDbContext db = new GestionConcourDbContext();
			diplome.Cne = Session["cne"].ToString();
			db.Diplomes.Add(diplome);
			db.SaveChanges();

			uni.Cne = Session["cne"].ToString();
			db.AnneeUniversitaires.Add(uni);
			db.SaveChanges();
			return View(Index());
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