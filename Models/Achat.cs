using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
namespace outils_dotnet.Models
{
    public class Achat
    {
       
            [Key]
        private long id { get; set; }
        private DateTime date { get; set; }
        private Article article { get; set; }
        private int quantite { get; set; }
        private Client client { get; set; }
        private User createBy { get; set; }

    }


}

