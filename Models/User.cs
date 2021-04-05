using System;
using System.Collections;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace gestion_articles.Models
{
    public class User
    {
        private long id { get; set; }
        private String login { get; set; }
        private String password { get; set; }
        private String pays { get; set; }
        private String lastName { get; set; }
        private String firstName { get; set; }
        private Boolean active { get; set; }
        private Client client { get; set; }

        private List<Role> roles = new List<Role>();

        public List<Role> getRoles()
        {
            return roles;
        }

        public void setRoles(List<Role> roles)
        {
            this.roles = roles;
        }
    }
}
