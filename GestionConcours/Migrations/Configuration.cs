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
            
            if (!context.Filieres.Any())
            {
                var filieres = new List<Filiere>();
                filieres.Add(new Filiere() { Nom = "Genie Informatique" });
                filieres.Add(new Filiere() { Nom = "Génie Réseaux et Télécoms" });
                filieres.Add(new Filiere() { Nom = "Génie Industriel" });
                filieres.Add(new Filiere() { Nom = "Génie des Procédés et M.C	" });

                context.Filieres.AddRange(filieres);
            }
            if (!context.Admins.Any())
            {
                var admins = new List<Admin>();
                admins.Add(new Admin() { Username="admin", Password="admin"});

                context.Admins.AddRange(admins);
            }

            base.Seed(context);
        }
    }
}