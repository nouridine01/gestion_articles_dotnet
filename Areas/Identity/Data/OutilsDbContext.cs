﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using outils_dotnet.Areas.Identity.Data;
using outils_dotnet.Models;

namespace outils_dotnet.Areas.Identity.Data
{
    public class OutilsDbContext : IdentityDbContext<User>
    {
        
        public OutilsDbContext(DbContextOptions<OutilsDbContext> options)
            : base(options)
        {
            //Database.SetInitializer<OutilsDbContext>(new Initializer());
            //http://localhost:23726/api/users
            //http://localhost:23726/identity/account/login
        }
       
       
        protected override void OnModelCreating(ModelBuilder builder)
        {
           
            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);
        }
    }
}
