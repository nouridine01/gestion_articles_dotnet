using outils_dotnet.Areas.Identity.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
namespace outils_dotnet.Models
{
    public class Location
    {
        [Key]
        public long Id { get; set; }
        public DateTime Date { get; set; }
        public DateTime Date_retour { get; set; }
        public int Quantite { get; set; }
        public int ArticleId { get; set; }
        public Article Article { get; set; }
        public int ClientId { get; set; }
        public Client Client { get; set; }
    }
}