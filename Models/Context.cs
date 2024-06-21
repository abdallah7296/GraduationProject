using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System;

namespace GraduationProject.Models
{
    public class Context : IdentityDbContext<User>
    {

        public Context()
        {
        }
        public Context(DbContextOptions<Context> options) : base(options)
        { 
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=db5794.databaseasp.net; Database=db5794; User Id=db5794 ;Password=s=8C6nE?t_3Z ; Encrypt=False; MultipleActiveResultSets=True ");
            }
        }

        internal object include(Func<object, object> value)
        {
            throw new NotImplementedException();
        }

        public DbSet<User> Users { get; set; }
         public DbSet<Images> images { get; set; }
 
        public DbSet<Restaurant> Restaurants { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<Menu> Menus { get; set; }
        public DbSet<MenuItem> MenuItems { get; set; }
        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<PlayStation> playStations { get; set; }
        public DbSet<Games> games { get; set; }
        public DbSet<Workspace> workspaces { get; set; }
         public DbSet<Pharmacies> pharmacies { get; set; }
        public DbSet<SuperMarket> SuperMarkets { get; set; }
        public DbSet<AnalysisCenters> analysisCenters { get; set; } 
 
    }
}
