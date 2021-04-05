using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace gestion_articles.Models
{
    public class Categorie
    {
        [Key]
        public long Id { get; set; }
        public String nom {get ; set ;}

        public List<Article> articles { get; set; }
    }
}