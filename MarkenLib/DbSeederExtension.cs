
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarkenLib
{
    public static class DbSeederExtension
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Kunde>().HasData(new Kunde
            {
                Id = 1,
                AdAdressNr = 11000068,
                AdFirmenBezeichnung = "SCHAUER Agrotronic GmbH",
                AdStrasse = "Passauerstraße",
                AdPostleitzahl = "4731",
                AdOrt = "Prambachkirchen",
                AdLandname = "AUSTRIA",
                AdNationalitaetsKz = "AUT"
                });

            modelBuilder.Entity<Kunde>().HasData(new Kunde

            {
                Id = 2,
                AdAdressNr = 11000068,
                AdFirmenBezeichnung = "SCHAUER Agrotronic GmbH",
                AdStrasse = "Passauerstraße",
                AdPostleitzahl = "4731",
                AdOrt = "Prambachkirchen",
                AdLandname = "AUSTRIA",
                AdNationalitaetsKz = "AUT"
            });


            modelBuilder.Entity<Ohrmarke>().HasData(new Ohrmarke
            {
                Id = 1,
                Beschreibung = "Umtausch",
                Datum = DateTime.Now,
                KundeId = 1,
                Lieferant = "Seppl",
                Markentyp = "noamla mokn"
            });
        }
    }
}
