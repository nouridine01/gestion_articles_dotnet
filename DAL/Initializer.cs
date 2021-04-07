
using Microsoft.AspNetCore.Identity;
using outils_dotnet.Areas.Identity.Data;
using outils_dotnet.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace outils_dotnet.DAL
{
    public class Initializer
    {
        
        public  static void Seed(OutilsDbContext context)
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
            User u = new User { Nom = "Oumarou", Prenom = "Nouridine" ,UserName="noor",PasswordHash="passer"};

            //Role role = new Role { Name = "CLIENT" };
            //Role role2 = new Role { Name = "VENDEUR" };
            //Role role3 = new Role { Name = "ADMIN" };
            //context.Roles.Add(role);
            //context.Roles.Add(role3);
            //context.Roles.Add(role2);
            //context.Users.Add(u);
            //context.UserRoles.Add(new UserRoles(context.Users.First<User>().Id, context.Roles.First<Role>().Id));
            //users.ForEach(s => context.Users.Add(s));
            context.SaveChanges();
        }
    }

    
}
