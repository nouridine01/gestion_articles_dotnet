using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace outils_dotnet.Models
{
    public class Categorie
    {
        [Key]
        public long Id { get; set; }
        public String Nom {get ; set ;}

        public List<Article> Articles { get; set; }
    }
}