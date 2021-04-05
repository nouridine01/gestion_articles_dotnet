using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
namespace outils_dotnet.Models
{
    public class Article
    {
        [Key]
        private long id { get; set; }
        private String nom { get; set; }
        private Categorie categorie { get; set; }
        private double prix { get; set; }
        private int quantite { get; set; }
        private User createBy { get; set; }
        private Boolean louable { get; set; }
        private Boolean achetable { get; set; }

        private List<Reservation> reservationEnCours = new List<Reservation>();
        private List<Achat> achatEffectues = new List<Achat>();    
        private List<Location> locationEnCours = new List<Location>();
  
      
        public List<Reservation> getReservationEnCours()
        {
            return reservationEnCours;
        }

        public void setReservationEnCours(List<Reservation> reservationEnCours)
        {
            this.reservationEnCours = reservationEnCours;
        }

        public List<Achat> getAchatEffectues()
        {
            return achatEffectues;
        }

        public void setAchatEffectues(List<Achat> achatEffectues)
        {
            this.achatEffectues = achatEffectues;
        }

        public List<Location> getLocationEnCours()
        {
            return locationEnCours;
        }

        public void setLocationEnCours(List<Location> locationEnCours)
        {
            this.locationEnCours = locationEnCours;
        }
    }
}
