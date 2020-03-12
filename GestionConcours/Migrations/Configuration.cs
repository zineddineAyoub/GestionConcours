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
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(GestionConcours.Models.GestionConcourDbContext context)
        {
            var admins = new List<Admin>();

            admins.Add(new Admin()
            {
                ID = 1,
                Username = "admin",
                Password = "admin"
            });

            context.Admins.AddRange(admins);
            base.Seed(context);
        }
    }
}
