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
        private long id { get; set; }
        private String type { get; set; }
        private DateTime date { get; set; }
        private DateTime date_recup { get; set; }
        private Boolean satisfaite = false;
        private int quantity { get; set; }
        Article article { get; set; }
        Client client { get; set; }

        public Boolean getSatisfaite()
        {
            return satisfaite;
        }

        public void setSatisfaite(Boolean satisfaite)
        {
            this.satisfaite = satisfaite;
        }
    }
}
