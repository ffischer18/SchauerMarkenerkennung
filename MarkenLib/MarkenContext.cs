using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarkenLib
{
    public class MarkenContext : DbContext
    {
        public MarkenContext(DbContextOptions<MarkenContext> options) : base(options)
        {

        }
        public MarkenContext()
        {

        }

        public DbSet<Ohrmarke> Ohrmarken { get; set; }
        public DbSet<ST_ADRESSE> ST_ADRESSEN { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            Console.WriteLine($"Db OnConfiguring: IsConfiguring: IsConfigured={optionsBuilder.IsConfigured}");
            if (!optionsBuilder.IsConfigured)
            {
                string connectionString = @"server=(LocalDB)\mssqllocaldb;attachdbfilename=C:\Temp\Ohrmarken.mdf;database=Ohrmarken10;integrated security=True;MultipleActiveResultSets=True;";
                Console.WriteLine($"    Using connectionString {connectionString}");
                optionsBuilder.UseSqlServer(connectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Seed();
        }

    }
}
