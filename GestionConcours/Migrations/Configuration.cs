namespace GestionConcours.Migrations
{
    using GestionConcours.Models;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<GestionConcours.Models.GestionConcourDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(GestionConcours.Models.GestionConcourDbContext context)
        {
            //  This method will be called after migrating to the latest version.

           // var Candidats = new List<Candidat>();
           // Candidats.Add(new Candidat() { Cne="1111",Cin="E1111",Nom="dariaoui",Prenom="oussama",Email="oussama@gmail.com",Adresse="adresse Test",LieuNaissance="Lieu de Naissance test",Telephone="0606060606",Nationalite="marocain",Sexe="M",Gsm="06020210",Conv"")
        }
    }
}
