using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using GestionConcours.Data;
using GestionConcours.Models;

namespace GestionConcours.Controllers
{
    public class CandidatsController : Controller
    {
        private GestionCouncourDbContext db = new GestionCouncourDbContext();

        // GET: Candidats
        public ActionResult Index()
        {
            var candidats = db.Candidats.Include(c => c.Baccalaureat).Include(c => c.CouncourEcrit).Include(c => c.CouncourOral).Include(c => c.Diplome).Include(c => c.Filiere);
            return View(candidats.ToList());
        }

        // GET: Candidats/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Candidat candidat = db.Candidats.Find(id);
            if (candidat == null)
            {
                return HttpNotFound();
            }
            return View(candidat);
        }

        // GET: Candidats/Create
        public ActionResult Create()
        {
            ViewBag.Cne = new SelectList(db.Baccalaureats, "Cne", "Type");
            ViewBag.Cne = new SelectList(db.CouncourEcrits, "Cne", "Cne");
            ViewBag.Cne = new SelectList(db.CouncourOrals, "Cne", "Cne");
            ViewBag.Cne = new SelectList(db.Diplomes, "Cne", "Type");
            ViewBag.ID = new SelectList(db.Filieres, "ID", "Nom");
            return View();
        }

        // POST: Candidats/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Cne,Cin,Nom,Prenom,Email,Adresse,LieuNaissance,Telephone,Nationalite,Num_dossier,Sexe,Gsm,DateInscription,Photo,Convoque,Admis,Password,ID")] Candidat candidat)
        {
            if (ModelState.IsValid)
            {
                db.Candidats.Add(candidat);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Cne = new SelectList(db.Baccalaureats, "Cne", "Type", candidat.Cne);
            ViewBag.Cne = new SelectList(db.CouncourEcrits, "Cne", "Cne", candidat.Cne);
            ViewBag.Cne = new SelectList(db.CouncourOrals, "Cne", "Cne", candidat.Cne);
            ViewBag.Cne = new SelectList(db.Diplomes, "Cne", "Type", candidat.Cne);
            ViewBag.ID = new SelectList(db.Filieres, "ID", "Nom", candidat.ID);
            return View(candidat);
        }

        // GET: Candidats/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Candidat candidat = db.Candidats.Find(id);
            if (candidat == null)
            {
                return HttpNotFound();
            }
            ViewBag.Cne = new SelectList(db.Baccalaureats, "Cne", "Type", candidat.Cne);
            ViewBag.Cne = new SelectList(db.CouncourEcrits, "Cne", "Cne", candidat.Cne);
            ViewBag.Cne = new SelectList(db.CouncourOrals, "Cne", "Cne", candidat.Cne);
            ViewBag.Cne = new SelectList(db.Diplomes, "Cne", "Type", candidat.Cne);
            ViewBag.ID = new SelectList(db.Filieres, "ID", "Nom", candidat.ID);
            return View(candidat);
        }

        // POST: Candidats/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Cne,Cin,Nom,Prenom,Email,Adresse,LieuNaissance,Telephone,Nationalite,Num_dossier,Sexe,Gsm,DateInscription,Photo,Convoque,Admis,Password,ID")] Candidat candidat)
        {
            if (ModelState.IsValid)
            {
                db.Entry(candidat).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Cne = new SelectList(db.Baccalaureats, "Cne", "Type", candidat.Cne);
            ViewBag.Cne = new SelectList(db.CouncourEcrits, "Cne", "Cne", candidat.Cne);
            ViewBag.Cne = new SelectList(db.CouncourOrals, "Cne", "Cne", candidat.Cne);
            ViewBag.Cne = new SelectList(db.Diplomes, "Cne", "Type", candidat.Cne);
            ViewBag.ID = new SelectList(db.Filieres, "ID", "Nom", candidat.ID);
            return View(candidat);
        }

        // GET: Candidats/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Candidat candidat = db.Candidats.Find(id);
            if (candidat == null)
            {
                return HttpNotFound();
            }
            return View(candidat);
        }

        // POST: Candidats/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            Candidat candidat = db.Candidats.Find(id);
            db.Candidats.Remove(candidat);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
