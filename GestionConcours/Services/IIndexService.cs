using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GestionConcours.Services
{
    public interface IIndexService
    {
        int getNbr_filiere(int fil, int niv);

        int getNbr_Corbeille(int niv);

        int getAll();

        int getInscrits_niv(int niv);
    }
}
