using System;
using System.Data.Entity;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Movies.Domain;

namespace Movies.DatabaseModel
{
    public class MovieDbContext : DbContext
    {
        public MovieDbContext() : base("DefaultConnection")
        {
        }

        public DbSet<Movie> Movies { get; set; }
    }
}
