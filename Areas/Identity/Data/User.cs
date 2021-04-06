using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace outils_dotnet.Areas.Identity.Data
{
    // Add profile data for application users by adding properties to the User class
    public class User : IdentityUser
    {
        [Key]
        public override string Id { get; set; }
        public string Nom { get; set; }

        public string Prenom { get; set; }
        public Models.Client client { get; set; }
        
        public override string UserName {get;set;}
       
    }
}
