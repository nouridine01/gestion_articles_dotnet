using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace outils_dotnet.Models
{
    public class Role
    {
                [Key]
        public int Id { get; set; }
        public string Nom { get; set; }
    }
}
