﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace gestion_articles.Models
{ 
    public class Role
    {
                [Key]
        public int Id { get; set; }
        public string Nom { get; set; }
    }
}
