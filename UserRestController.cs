using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using outils_dotnet.Areas.Identity.Data;
using outils_dotnet.Data;
using outils_dotnet.Models;


// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace gestion_articles
{
    
    [Route("api/users")]
    public class UserRestController : Controller
    {
        private readonly OutilsDbContext service;
        private readonly dbContext service2;
        public UserRestController(OutilsDbContext ser)
        {
            this.service = ser;
        }
        // GET: api/<controller>
        [HttpGet]
        public IEnumerable<User> Get()
        {
            return service.Users;
        }

        [Route("/cat")]
        [HttpGet]
        public IEnumerable<Categorie> Getc()
        {
            return service2.Categorie;
        }



    }
}
