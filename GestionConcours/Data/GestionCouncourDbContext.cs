using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

using GestionConcours.Models;

namespace GestionConcours.Data
{
    public class GestionCouncourDbContext : DbContext
    {
        
        public DbSet<AnneeUniversitaire> AnneeUniversitaires { get; set; }
        public DbSet<Baccalaureat> Baccalaureats { get; set; }
        public DbSet<Candidat> Candidats { get; set; }
        public DbSet<ConfigurationPreselection> ConfigurationPreselections { get; set; }
        public DbSet<ConfigurationSelection> ConfigurationSelections { get; set; }
        public DbSet<Corbeille> Corbeilles { get; set; }
        public DbSet<CouncourEcrit> CouncourEcrits { get; set; }
        public DbSet<CouncourOral> CouncourOrals { get; set; }
        public DbSet<Diplome> Diplomes { get; set; }
        public DbSet<Filiere> Filieres { get; set; }

        public GestionCouncourDbContext() : base("GestionCouncourDbContext")
        {

        }
    }
}