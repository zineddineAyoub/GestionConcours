using GestionConcours.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace GestionConcours.Services
{
    public class EpreuveServicceImp : IEpreuveService
    {

        public string uploadEpreuve(UploadModel epreuve)
        {
            HttpPostedFileBase file = epreuve.file;
            string matiere = epreuve.matiere;
            string annee = epreuve.annee;
            string response = "1";
            if (file != null && file.ContentLength > 0)
                try
                {
                    string path = Path.Combine(System.Web.HttpContext.Current.Server.MapPath("~/Epreuves/"), file.FileName);
                    file.SaveAs(path);
                    GestionConcourDbContext db = new GestionConcourDbContext();
                    Epreuves epr = new Epreuves();
                    epr.NomFichier = file.FileName;
                    epr.Matiere = matiere;
                    epr.Annee = annee;
                    db.Epreuves.Add(epr);
                    db.SaveChanges();
                }
                catch (Exception ex)
                {
                    response = ex.Message;
                }
            else
            {
                response = "0";
            }
            return response;
        }
    }
}