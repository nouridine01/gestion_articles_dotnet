using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
namespace gestion_articles.Models
{
    public class Reservation
    {
        [Key]
        public long Id { get; set; }
        public String type { get; set; }
        public DateTime date { get; set; }
        public DateTime date_recup { get; set; }
        public Boolean satisfaite = false;
        public int quantity { get; set; }
        public int ArticleId { get; set; }
        public Article article { get; set; }
        public int ClientId { get; set; }
        public Client client { get; set; }

    }
}
