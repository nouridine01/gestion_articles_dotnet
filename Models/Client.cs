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
        private long id { get; set; }
        private User user { get; set; }
        List<Achat> achats = new List<Achat>();
        List<Location> locations = new List<Location>();
        List<Reservation> reservations = new List<Reservation>();


        public List<Achat> getAchats()
        {
            return achats;
        }

        public void setAchats(List<Achat> achats)
        {
            this.achats = achats;
        }

        public List<Location> getLocations()
        {
            return locations;
        }

        public void setLocations(List<Location> locations)
        {
            this.locations = locations;
        }

        public List<Reservation> getReservations()
        {
            return reservations;
        }

        public void setReservations(List<Reservation> reservations)
        {
            this.reservations = reservations;
        }

    }
}