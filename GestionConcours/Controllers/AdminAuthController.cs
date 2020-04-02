using GestionConcours.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GestionConcours.Controllers
{
    public class AdminAuthController : Controller
    {
        //Context
        GestionConcourDbContext db = new GestionConcourDbContext();

        public ActionResult Index()
        {
            if(Session["admin"] != null)
            {
                if (Session["admin"].Equals(true))
                    return RedirectToAction("Index","Admin");
                
            }
                return Redirect("Error");
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(Admin admin)
        {
            if (ModelState.IsValid)
            {
                Admin adminSaisi = db.Admins.Where(a => a.Username.Equals(admin.Username) && a.Password.Equals(admin.Password)).SingleOrDefault();
                if(adminSaisi != null)
                {
                    Session["admin"] = true;
                    return RedirectToAction("Index", "Admin");
                }

                ViewBag.error = "Incorrect username or password !";

            }
            return View();
        }

        public ActionResult LogOut()
        {
            Session["admin"] = null;
            return Redirect("Login");
        }


        public ActionResult Error()
        {
            return View();
        }


    }
}