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