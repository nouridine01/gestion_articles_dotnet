using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
namespace gestion_articles.Models
{
    public class Location
    {
            [Key]
        private long id { get; set; }
        private DateTime date { get; set; }
        private DateTime date_retour { get; set; }
        private User createBy { get; set; }
        private int quantite { get; set; }
        Article article { get; set; }
        Client client { get; set; }
    }
}