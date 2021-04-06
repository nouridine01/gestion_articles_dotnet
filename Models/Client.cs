using outils_dotnet.Areas.Identity.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
namespace outils_dotnet.Models
{
    public class Client
    {
        [Key]
        public long Id { get; set; }
        public string UserId { get; set; }
        public User User { get; set; }
        public ICollection<Achat> Achats { get; set; }
        public ICollection<Location> Locations  { get; set; }
        public ICollection<Reservation> Reservations { get; set; }



    }
}