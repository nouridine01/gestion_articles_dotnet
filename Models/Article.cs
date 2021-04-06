using outils_dotnet.Areas.Identity.Data;
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
        public long Id { get; set; }
        public String Nom { get; set; }
        public Categorie Categorie { get; set; }
        public double Prix { get; set; }
        public int Quantite { get; set; }
        public User CreateBy { get; set; }
        public Boolean Louable { get; set; }
        public Boolean Achetable { get; set; }

        public ICollection<Reservation> ReservationEnCours { get; set; }
        public ICollection<Achat> AchatEffectues { get; set; }
        public ICollection<Location> LocationEnCours { get; set; }


    }
}
