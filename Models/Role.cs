using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace outils_dotnet.Models
{ 
    public class Role : IdentityRole
    {
        
        public string Nom { get; set; }
    }
}
