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
        private long id { get; set; }
        private String nom {get ; set ;}
        
        private List<Article> articles = new List<Article>();

        
        public List<Article> getArticles()
        {
            return articles;
        }

        public void setArticles(List<Article> articles)
        {
            this.articles = articles;
        }
    }
}