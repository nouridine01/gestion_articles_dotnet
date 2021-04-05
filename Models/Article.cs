using outils_dotnet.Areas.Identity.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
namespace gestion_articles.Models
{
    public class Article
    {
        [Key]
        public long Id { get; set; }
        public String nom { get; set; }
        public Categorie categorie { get; set; }
        public double prix { get; set; }
        public int quantite { get; set; }
        public User createBy { get; set; }
        public Boolean louable { get; set; }
        public Boolean achetable { get; set; }

        public ICollection<Reservation> reservationEnCours { get; set; }
        public ICollection<Achat> achatEffectues { get; set; }
        public ICollection<Location> locationEnCours { get; set; }


    }
}
