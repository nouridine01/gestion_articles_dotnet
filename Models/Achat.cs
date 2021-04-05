using outils_dotnet.Areas.Identity.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
namespace gestion_articles.Models
{
    public class Achat
    {

        [Key]
        public long Id { get; set; }
        public DateTime date { get; set; }
        public int quantite { get; set; }
        public int ArticleId { get; set; }
        public Article article { get; set; }
        public int ClientId { get; set; }
        public Client client { get; set; }

    }


}

