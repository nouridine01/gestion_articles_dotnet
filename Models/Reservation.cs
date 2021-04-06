using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
namespace outils_dotnet.Models
{
    public class Reservation
    {
        [Key]
        public long Id { get; set; }
        public String Type { get; set; }
        public DateTime Date { get; set; }
        public DateTime Date_recup { get; set; }
        public Boolean Satisfaite = false;
        public int Quantity { get; set; }
        public int ArticleId { get; set; }
        public Article Article { get; set; }
        public int ClientId { get; set; }
        public Client Client { get; set; }

    }
}
