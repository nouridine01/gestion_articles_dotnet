
using outils_dotnet.Areas.Identity.Data; 
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace gestion_articles.DAL
{
    public class Initializer
    {
        public static void Seed(OutilsDbContext context)
        {
            context.Database.EnsureCreated();
           
            // Look for any students.
            if (context.Users.Any())
            {
                return;   // DB has been seeded
            }

            var users = new List<User>
            {
            new User{Nom="Oumarou",Prenom="Nouridine"},
            new User{Nom="Nour",Prenom="Alexander"},
            new User{Nom="Carson",Prenom="Alexander"},
            };

            //users.ForEach(s => context.Users.Add(s));
            context.SaveChanges();
        }
    }

    
}
