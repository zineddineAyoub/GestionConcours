using GestionConcours.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace GestionConcours.Services
{
    public interface IEpreuveService
    {
        string uploadEpreuve(UploadModel epreuve);
        IEnumerable<DiplomeFichierModel> diplomeFile(string cne,int niveau);
    }
}
