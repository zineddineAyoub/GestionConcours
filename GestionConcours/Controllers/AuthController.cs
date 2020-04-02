using GestionConcours.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net.Mail;
using System.Net;

namespace GestionConcours.Controllers
{
    public class AuthController : Controller
    {
        // GET: Auth
        public ActionResult Index()
        {
            if (Session["cne"] != null)
            {
                return RedirectToAction("Index", "Home");
            }
            return RedirectToAction("Login", "Auth");
        }
        public ActionResult Verify(string cne)
        {
            GestionConcourDbContext db = new GestionConcourDbContext();
            var x = db.Candidats.Where(c => c.Cne==cne).SingleOrDefault();
            x.Verified = 1;
            db.SaveChanges();
            TempData["message"] = "Account Verified Succefully";
            return Redirect("Login");
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(Candidat candidat)
        {
            GestionConcourDbContext db = new GestionConcourDbContext();
            var x=db.Candidats.Where(c => c.Cne == candidat.Cne && c.Cin == candidat.Cin && c.Password == candidat.Password).SingleOrDefault();
            if (x == null)
            {
                TempData["error"] = "False Credential";
                return Redirect("Login");
            }
            else if (x.Verified == 0)
            {
                TempData["error"] = "Verify Youre Email";
                return Redirect("Login");
            }
            Session["cne"] = candidat.Cne;
			Session["niveau"] = x.Niveau;
            Session["role"] = "user";
            Session["photo"] = candidat.Photo;
            return RedirectToAction("Index", "Home");
        }

        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(Candidat candidat)
        {
            GestionConcourDbContext db = new GestionConcourDbContext();
            if (candidat.Niveau == 0)
            {
                ModelState.AddModelError("selectNiveau", "Selectionner un niveau");
            }
            var y = db.Candidats.Where(c => c.Cne == candidat.Cne).SingleOrDefault();
            if (y != null)
            {
                ModelState.AddModelError("UniqueCne", "Cne need to be unique");
            }
            var z = db.Candidats.Where(c => c.Cin == candidat.Cin).SingleOrDefault();
            if (z != null)
            {
                ModelState.AddModelError("UniqueCin", "Cin need to be unique");
            }
            /*var w = db.Candidats.Where(c => c.Email == candidat.Email).SingleOrDefault();
            if (w != null)
            {
                ModelState.AddModelError("UniqueEmail", "Email need to be unique");
            }*/

            if (ModelState.IsValid)
            {
                candidat.DateInscription = DateTime.Now;
                candidat.DateNaissance = DateTime.Now;
                Random random = new Random();
                const string pool = "abcdefghijklmnopqrstuvwxyz0123456789";
                var chars = Enumerable.Range(0, 7)
                    .Select(x => pool[random.Next(0, pool.Length)]);
                candidat.Password=new string(chars.ToArray());
                candidat.Verified = 0;
                candidat.Photo = "icon.jpg";
                db.Candidats.Add(candidat);
                db.SaveChanges();

                Diplome dip = new Diplome();
                AnneeUniversitaire annUn = new AnneeUniversitaire();
                Baccalaureat bac = new Baccalaureat();
                CouncourEcrit concE = new CouncourEcrit();
                CouncourOral concO = new CouncourOral();

                //add row in diplome
                dip.Cne = candidat.Cne;
                db.Diplomes.Add(dip);
                db.SaveChanges();

                //add row in anne
                annUn.Cne = candidat.Cne;
                db.AnneeUniversitaires.Add(annUn);
                db.SaveChanges();

                //add row in bac
                bac.Cne = candidat.Cne;
                //bac.DateObtentionBac = DateTime.Now;
                db.Baccalaureats.Add(bac);
                db.SaveChanges();

                //add in concours ecrit
                concE.Cne = candidat.Cne;
                db.CouncourEcrits.Add(concE);
                db.SaveChanges();

                //add in concours oral
                concO.Cne = candidat.Cne;
                db.CouncourOrals.Add(concO);
                db.SaveChanges();

                var fromAddress = new MailAddress("tarik.ouhamou@gmail.com", "From Name");
                var toAddress = new MailAddress(candidat.Email, "To Name");
                const string fromPassword = "dragonballz123+";
                const string subject = "test";
                string body = "<a href=\"http://localhost:49969/Auth/Verify?cne="+candidat.Cne+" \">Link</a><br /><p> this is the password : "+candidat.Password+"</p>";

                var smtp = new SmtpClient
                {
                    Host = "smtp.gmail.com",
                    Port = 587,
                    EnableSsl = true,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    Credentials = new NetworkCredential(fromAddress.Address, fromPassword),
                    Timeout = 60000
                };
                using (var message = new MailMessage(fromAddress, toAddress)
                {
                    Subject = subject,
                    Body = body
                })
                {
                    message.IsBodyHtml = true;
                    smtp.Send(message);
                }
                TempData["message"] = "Verify Email To get Password";
                return Redirect("Login");
            }
            return View(candidat);
        }
    }
}