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

        /*[HttpGet("{id}")]
        [Authorize]
        public User GetOne(int id)
        {
            return service.Users.FirstOrDefault(s=>s.Id==id);
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            User user =service.Users.FirstOrDefault(s => s.Id == id);
            service.Users.Remove(user);
            service.SaveChanges();
        }


        [HttpPut("{id}")]
        public User Update(int id, [FromBody] User user)
        {
            user.Id= id;
            //service.Users.(user);
            service.SaveChanges();
            return user;
        }


        // POST api/<controller>
        [HttpPost]
        public User Post([FromBody] User user)
        {
            service.Users.Add(user);
            service.SaveChanges();
            return user;
        }*/


    }
}
