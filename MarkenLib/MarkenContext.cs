using MarkenverwaltungLib;
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
        public DbSet<Kunde> Kunden { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            Console.WriteLine($"Db OnConfiguring: IsConfiguring: IsConfigured={optionsBuilder.IsConfigured}");
            if (!optionsBuilder.IsConfigured)
            {
                string connectionString = @"server=(LocalDB)\mssqllocaldb;attachdbfilename=C:\Temp\Ohrmarken.mdf;database=Ohrmarken;integrated security=True;MultipleActiveResultSets=True;";
                Console.WriteLine($"    Using connectionString {connectionString}");
                optionsBuilder.UseSqlServer(connectionString);
            }
        }

    }
}
