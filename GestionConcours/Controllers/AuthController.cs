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
                TempData["error"] = "Verify Your Email";
                return Redirect("Login");
            }
            Session["cne"] = candidat.Cne;

			Session["niveau"] = x.Niveau;
			Session["role"] = "user";
			Session["photo"] = candidat.Photo;
			return RedirectToAction("Index", "Home");
		}


        public ActionResult PasswordOublie()
        {
            return View();
        }

        [HttpPost]
        public ActionResult PasswordOublie(string email)
        {
            GestionConcourDbContext db = new GestionConcourDbContext();
            Candidat candidat = db.Candidats.Where(c => c.Email==email).First();
            Random random = new Random();
            const string pool = "abcdefghijklmnopqrstuvwxyz0123456789";
            var chars = Enumerable.Range(0, 7)
                .Select(x => pool[random.Next(0, pool.Length)]);
            string password = new string(chars.ToArray());
            candidat.Password = password;
            db.SaveChanges();
            var fromAddress = new MailAddress("tarik.ouhamou@gmail.com", "From ENSAS ");
            var toAddress = new MailAddress(email, "To Name");
            const string fromPassword = "dragonballz123+";
            const string subject = "Restauration de mot de pass";
            //string body = "<a href=\"http://localhost:49969/Auth/Verify?cne="+candidat.Cne+" \">Link</a><br /><p> this is the password : "+candidat.Password+"</p>";
            string body = "<div class=\"container\"><div class=\"row\"><img src=\"https://lh3.googleusercontent.com/proxy/hC9cwJR36bnSWiwqQdIH-xbphsS52akOONW7LPoGCIVLPrBrTpXfdV7PbHe6SsI5gWYfV6nUjY6dys8N8c7IUIk4uw8 \" /></div><br><div class=\"alert alert-danger\"><strong><span style=\"color:'red'\">Vous trouverez votre nouveau mot de passe au dessous</span></strong><br></div><div class=\"row\"><div class=\"card\" style=\"width: 18rem;\"><div class=\"card-body\"><strong>Nom :</strong><span>" + candidat.Nom + "</span><br /><strong>Prenom : </strong><span>" + candidat.Prenom + "</span><br /><strong>CNE : </strong><span>" + candidat.Cne + "</span><br /><strong>CIN : </strong><span>" + candidat.Cin + "</span><br /><strong>Password : </strong><span>" + candidat.Password + "</span><br /></div></div></div></div>";
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
            TempData["message"] = "Email Sent Succefully";
            return View();
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
			var w = db.Candidats.Where(c => c.Email == candidat.Email).First();
			if (w != null)
			{
				ModelState.AddModelError("UniqueEmail", "Email need to be unique");
			}


            if (ModelState.IsValid)
            {
                candidat.DateInscription = DateTime.Now;
                candidat.DateNaissance = DateTime.Now;
                Random random = new Random();
                const string pool = "abcdefghijklmnopqrstuvwxyz0123456789";
                var chars = Enumerable.Range(0, 7)
                    .Select(x => pool[random.Next(0, pool.Length)]);
                var charsMatricule = Enumerable.Range(0, 8)
                    .Select(ww => pool[random.Next(0, pool.Length)]);
                candidat.Matricule = new string(charsMatricule.ToArray());
                candidat.Password=new string(chars.ToArray());
                candidat.Verified = 0;
                candidat.Photo = "icon.jpg";
                db.Candidats.Add(candidat);
                db.SaveChanges();

                Diplome dip = new Diplome();
                AnneeUniversitaire annUn = new AnneeUniversitaire();
                Baccalaureat bac = new Baccalaureat();
                ConcourEcrit concE = new ConcourEcrit();
                ConcourOral concO = new ConcourOral();


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


                var fromAddress = new MailAddress("tarik.ouhamou@gmail.com", "From ENSAS");
                var toAddress = new MailAddress(candidat.Email, "To Name");
                const string fromPassword = "dragonballz123+";
                const string subject = "Verification de compte de postulation au concours ENSAS";
                //string body = "<a href=\"http://localhost:49969/Auth/Verify?cne="+candidat.Cne+" \">Link</a><br /><p> this is the password : "+candidat.Password+"</p>";
                string body = "<div class=\"container\"><div class=\"row\"><img src=\"https://lh3.googleusercontent.com/proxy/hC9cwJR36bnSWiwqQdIH-xbphsS52akOONW7LPoGCIVLPrBrTpXfdV7PbHe6SsI5gWYfV6nUjY6dys8N8c7IUIk4uw8 \" /></div><div class=\"row text-center\"><h2>Vous avez créer un compte dans la platforme d'acces au cycle d'ingénieur a ENSAS Veuillez Vérifier Votre Compt en appuiyant sur le lien ci dessous.</h2><a href =\"http://localhost:49969/Auth/Verify?cne=" + candidat.Cne + " \">Lien de vérification</a></div><div class=\"alert alert-danger\"><strong><span style=\"color:'red'\">Vous trouverez votre mot de pass au dessouss</span></strong><br></div><div class=\"row\"><div class=\"card\" style=\"width: 18rem;\"><div class=\"card-body\"><strong>Nom :</strong><span>" + candidat.Nom + "</span><br /><strong>Prenom : </strong><span>" + candidat.Prenom + "</span><br /><strong>CNE : </strong><span>" + candidat.Cne + "</span><br /><strong>CIN : </strong><span>" + candidat.Cin + "</span><br /><strong>Password : </strong><span>" + candidat.Password + "</span><br /></div></div></div></div>";


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