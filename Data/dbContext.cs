using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using outils_dotnet.Models;

namespace outils_dotnet.Data
{
    public class dbContext : DbContext
    {
        public dbContext (DbContextOptions<dbContext> options)
            : base(options)
        {
        }

        public DbSet<outils_dotnet.Models.Categorie> Categorie { get; set; }

        public DbSet<outils_dotnet.Models.Client> Client { get; set; }

        public DbSet<outils_dotnet.Models.Article> Article { get; set; }

        public DbSet<outils_dotnet.Models.Reservation> Reservation { get; set; }

        public DbSet<outils_dotnet.Models.Location> Location { get; set; }

        public DbSet<outils_dotnet.Models.Achat> Achat { get; set; }
    }
}
