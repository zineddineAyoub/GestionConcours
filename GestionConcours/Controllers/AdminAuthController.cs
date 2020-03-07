using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GestionConcours.Controllers
{
    public class AdminAuthController : Controller
    {
        // GET: AdminAuth
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Login()
        {
            return View();
        }

        public ActionResult TestAutrePage()
        {
            return View("Enregistrement");
        }

        public ActionResult TestRecherche()
        {
            return View("Recherche");
        }

        
    }
}