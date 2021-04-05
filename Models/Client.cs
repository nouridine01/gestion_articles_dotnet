using outils_dotnet.Areas.Identity.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
namespace gestion_articles.Models
{
    public class Client
    {
        [Key]
        public long Id { get; set; }
        public string UserId { get; set; }
        public User user { get; set; }
        public ICollection<Achat> achats { get; set; }
        public ICollection<Location> locations  { get; set; }
        public ICollection<Reservation> reservations { get; set; }



    }
}