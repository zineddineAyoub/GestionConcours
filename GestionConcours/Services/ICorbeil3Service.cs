using GestionConcours.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionConcours.Services
{
    public interface ICorbeil3Service
    {
        IEnumerable<SearchModel3> afficheCorbeil(int niveau);
        IEnumerable<SearchModel3> restoreCorbeil(string cne,int niveau);
        IEnumerable<SearchModel3> searchCriteria(string Criteria, string Key, string Diplome, string Filiere,int niveau);
    }
}
