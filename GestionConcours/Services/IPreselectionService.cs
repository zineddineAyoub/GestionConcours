using GestionConcours.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GestionConcours.Services
{
    public interface IPreselectionService
    {
        IEnumerable<PreselectionModel> getAll(int niveau);
        IEnumerable<PreselectionModel> calculerPreselec(string fil, string diplome, int niveau);
        void setConfig(ConfigurationPreselection config, int niveau);

    }
}