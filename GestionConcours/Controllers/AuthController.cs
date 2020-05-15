using GestionConcours.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net.Mail;
using System.Net;
using System.Data.Entity;

namespace GestionConcours.Controllers
{
	public class AuthController : Controller
	{
        private GestionConcourDbContext db = new GestionConcourDbContext();
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

        public ActionResult Step1()
        {
            if (Session["cne"] != null)
            {
                Candidat candidat = db.Candidats.Find(Session["cne"]);
                if (candidat.Verified == 1)
                {
                    return RedirectToAction("Index", "Home");
                }
                InfoPersoModel info = new InfoPersoModel()
                {
                    Adresse = candidat.Adresse,
                    Ville = candidat.Ville,
                    LieuNaissance = candidat.LieuNaissance,
                    Telephone = candidat.Telephone,
                    Nationalite = candidat.Nationalite,
                    Gsm = candidat.Gsm,
                    DateNaissance = candidat.DateNaissance,
                    Sexe=candidat.Sexe
                };
                return View(info);
            }
            return RedirectToAction("Login", "Auth");
        }

        [HttpPost]
        public ActionResult Step1(InfoPersoModel candidat)
        {
            string cne = Session["cne"].ToString();
            if (ModelState.IsValid)
            {
                var originalCandiat = (from c in db.Candidats where c.Cne == cne select c).First();
                originalCandiat.DateNaissance = candidat.DateNaissance;
                originalCandiat.LieuNaissance = candidat.LieuNaissance;
                originalCandiat.Sexe = candidat.Sexe;
                originalCandiat.Nationalite = candidat.Nationalite;
                originalCandiat.Gsm = candidat.Gsm;
                originalCandiat.Telephone = candidat.Telephone;
                originalCandiat.Adresse = candidat.Adresse;
                originalCandiat.Ville = candidat.Ville;
                db.SaveChanges();
                return RedirectToAction("Step2");
            }
            return View(candidat);
        }

        public ActionResult Step2()
        {
            if (Session["cne"] != null)
            {
                Candidat candidat = db.Candidats.Find(Session["cne"]);
                if (candidat.Verified == 1)
                {
                    return RedirectToAction("Index", "Home");
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
                return View(bac);
            }
            return RedirectToAction("Login", "Auth");
        }

        [HttpPost]
        public ActionResult Step2([Bind(Include = "Cne,TypeBac,DateObtentionBac,NoteBac,MentionBac")]Baccalaureat bac)
        {
            if (ModelState.IsValid)
            {
                db.Entry(bac).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Step3");
            }
            return View(bac);
        }

        public ActionResult Step3()
        {
            if (Session["cne"] != null)
            {
                Candidat candidat = db.Candidats.Find(Session["cne"]);
                if (candidat.Verified == 1)
                {
                    return RedirectToAction("Index", "Home");
                }
                var diplome = db.Diplomes.Find(Session["cne"]);
                var anne = db.AnneeUniversitaires.Find(Session["cne"]);
                DiplomeNote dipNote = new DiplomeNote()
                {
                    Type = diplome.Type,
                    Etablissement = diplome.Etablissement,
                    VilleObtention = diplome.VilleObtention,
                    NoteDiplome = diplome.NoteDiplome,
                    Specialite = diplome.Specialite,
                    Semestre1 = anne.Semestre1,
                    Semestre2 = anne.Semestre2,
                    Semestre3 = anne.Semestre3,
                    Semestre4 = anne.Semestre4,
                    Semestre5 = anne.Semestre5,
                    Semestre6 = anne.Semestre6,
                    Redoublant1 = anne.Redoublant1,
                    Redoublant2 = anne.Redoublant2,
                    Redoublant3 = anne.Redoublant3,
                    AnneUni1 = anne.AnneUni1,
                    AnneUni2 = anne.AnneUni2,
                    AnneUni3 = anne.AnneUni3
                };
                var fromAddress = new MailAddress("tarik.ouhamou@gmail.com", "From ENSAS");
                var toAddress = new MailAddress(candidat.Email, "To Name");
                const string fromPassword = "dragonballz123+";
                const string subject = "Création de compte de postulation au concours ENSAS";
                //string body = "<a href=\"http://localhost:49969/Auth/Verify?cne="+candidat.Cne+" \">Link</a><br /><p> this is the password : "+candidat.Password+"</p>";
                string body = "<div class=\"container\"><div class=\"row\"><img src=\"https://lh3.googleusercontent.com/proxy/g_QnANEsQGJPGvR4haGBTi-kr2n32DU-eArBRKuJWtpgPCHQbz-RINzL6FzIc1TQs0a80Vfkaew6umTHHPQgHTE4l_g \" /></div><div class=\"row text-center\"><h2>Vous avez créer un compte dans la platforme d'acces au cycle d'ingénieur a ENSAS </h2></div><div class=\"alert alert-danger\"><strong><span style=\"color:'red'\">Vous trouverez votre mot de pass au dessouss</span></strong><br></div><div class=\"row\"><div class=\"card\" style=\"width: 18rem;\"><div class=\"card-body\"><strong>Nom :</strong><span>" + candidat.Nom + "</span><br /><strong>Prenom : </strong><span>" + candidat.Prenom + "</span><br /><strong>CNE : </strong><span>" + candidat.Cne + "</span><br /><strong>CIN : </strong><span>" + candidat.Cin + "</span><br /><strong>Password : </strong><span>" + candidat.Password + "</span><br /><strong>Filiere Choisie : </strong><span>" + candidat.Filiere.Nom + "</span><br /></div></div></div></div>";


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
                return View(dipNote);
            }
            return RedirectToAction("Login", "Auth");
        }

        [HttpPost]
        public ActionResult Step3(DiplomeNote diplome)
        {
            string cne = Session["cne"].ToString();
            if (ModelState.IsValid)
            {
                var x = db.Diplomes.Where(c => c.Cne == cne).SingleOrDefault();
                x.Type = diplome.Type;
                x.Etablissement = diplome.Etablissement;
                x.VilleObtention = diplome.VilleObtention;
                x.NoteDiplome = diplome.NoteDiplome;
                x.Specialite = diplome.Specialite;
                db.SaveChanges();

                var y = db.AnneeUniversitaires.Where(a => a.Cne == cne).SingleOrDefault();
                y.Semestre1 = diplome.Semestre1;
                y.Semestre2 = diplome.Semestre2;
                y.Semestre3 = diplome.Semestre3;
                y.Semestre4 = diplome.Semestre4;
                y.Semestre5 = diplome.Semestre5;
                y.Semestre6 = diplome.Semestre6;
                y.Redoublant1 = diplome.Redoublant1;
                y.Redoublant2 = diplome.Redoublant2;
                y.Redoublant3 = diplome.Redoublant3;
                y.AnneUni1 = diplome.AnneUni1;
                y.AnneUni2 = diplome.AnneUni2;
                y.AnneUni3 = diplome.AnneUni3;

                db.SaveChanges();
                Candidat candidat = db.Candidats.Find(Session["cne"]);
                candidat.Verified = 1;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(diplome);
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
            else
            {
                Session["cne"] = candidat.Cne;

                Session["niveau"] = x.Niveau;
                Session["role"] = "user";
                Session["photo"] = candidat.Photo;

                if (x.Verified == 1)
                {
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    return RedirectToAction("Step1", "Auth");
                }

            }
		}


        public ActionResult PasswordOublie()
        {
            return View();
        }

        [HttpPost]
        public ActionResult PasswordOublie(string email)
        {
            GestionConcourDbContext db = new GestionConcourDbContext();
            Candidat candidat = db.Candidats.Where(c => c.Email==email).SingleOrDefault();
            Random random = new Random();
            const string pool = "abcdefghijklmnopqrstuvwxyz0123456789";
            var chars = Enumerable.Range(0, 7)
                .Select(x => pool[random.Next(0, pool.Length)]);
            string password = new string(chars.ToArray());
            try
            {
                candidat.Password = password;
                db.SaveChanges();
                var fromAddress = new MailAddress("tarik.ouhamou@gmail.com", "From ENSAS ");
                var toAddress = new MailAddress(email, "To Name");
                const string fromPassword = "dragonballz123+";
                const string subject = "Restauration de mot de pass";
                //string body = "<a href=\"http://localhost:49969/Auth/Verify?cne="+candidat.Cne+" \">Link</a><br /><p> this is the password : "+candidat.Password+"</p>";
                string body = "<div class=\"container\"><div class=\"row\"><img src=\"https://lh3.googleusercontent.com/proxy/g_QnANEsQGJPGvR4haGBTi-kr2n32DU-eArBRKuJWtpgPCHQbz-RINzL6FzIc1TQs0a80Vfkaew6umTHHPQgHTE4l_g \" /></div><br><div class=\"alert alert-danger\"><strong><span style=\"color:'red'\">Vous trouverez votre nouveau mot de passe au dessous</span></strong><br></div><div class=\"row\"><div class=\"card\" style=\"width: 18rem;\"><div class=\"card-body\"><strong>Nom :</strong><span>" + candidat.Nom + "</span><br /><strong>Prenom : </strong><span>" + candidat.Prenom + "</span><br /><strong>CNE : </strong><span>" + candidat.Cne + "</span><br /><strong>CIN : </strong><span>" + candidat.Cin + "</span><br /><strong>Password : </strong><span>" + candidat.Password + "</span><br /></div></div></div></div>";
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

            } catch(Exception ex)
            {
                TempData["error"] = "Entrer des données valides";
                return View();
            }
            
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
			var w = db.Candidats.Where(c => c.Email == candidat.Email).SingleOrDefault();
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
                candidat.Matricule = new string(charsMatricule.ToArray()).ToUpper();
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
                const string subject = "Récupération du mot de passe";
                //string body = "<a href=\"http://localhost:49969/Auth/Verify?cne="+candidat.Cne+" \">Link</a><br /><p> this is the password : "+candidat.Password+"</p>";
                string body = "<div class=\"container\"><div class=\"row\"><img src=\"https://lh3.googleusercontent.com/proxy/g_QnANEsQGJPGvR4haGBTi-kr2n32DU-eArBRKuJWtpgPCHQbz-RINzL6FzIc1TQs0a80Vfkaew6umTHHPQgHTE4l_g \" /></div><div class=\"row text-center\"><h2>Vous avez créer un compte dans la platforme d'acces au cycle d'ingénieur a ENSAS .</h2></div><div class=\"alert alert-danger\"><strong><span style=\"color:'red'\">Vous trouverez votre mot de pass au dessouss</span></strong><br></div><div class=\"row\"><div class=\"card\" style=\"width: 18rem;\"><div class=\"card-body\"><strong>Nom :</strong><span>" + candidat.Nom + "</span><br /><strong>Prenom : </strong><span>" + candidat.Prenom + "</span><br /><strong>CNE : </strong><span>" + candidat.Cne + "</span><br /><strong>CIN : </strong><span>" + candidat.Cin + "</span><br /><strong>Password : </strong><span>" + candidat.Password + "</span><br /></div></div></div></div>";


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
                TempData["message"] = "Votre mot de passe est : '" + candidat.Password + "'." + " Vous le trouverez sur votre email aussi.";
				return Redirect("Login");
			}
			return View(candidat);
		} 
	}
}