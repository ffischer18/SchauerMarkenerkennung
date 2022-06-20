
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
            modelBuilder.Entity<ST_ADRESSE>().HasData(new ST_ADRESSE
            {
                AD_ADRESS_NR = 11000069,
                AD_ADRESS_ID = "AD_11000069_ID",
                AD_FIRMEN_BEZEICHNUNG = "SCHAUER Agrotronic GmbH",
                AD_STRASSE = "Passauerstraße",
                AD_POSTLEITZAHL = "4731",
                AD_ORT = "Prambachkirchen",
                AD_LANDNAME = "AUSTRIA",
                AD_NATIONALITAETS_KZ = "AUT"
            });

            modelBuilder.Entity<ST_ADRESSE>().HasData(new ST_ADRESSE
            {
                AD_ADRESS_NR = 11000068,
                AD_ADRESS_ID = "AD_11000068_ID",
                AD_FIRMEN_BEZEICHNUNG = "HTL GKR",
                AD_STRASSE = "Passauerstraße",
                AD_POSTLEITZAHL = "4731",
                AD_ORT = "Prambachkirchen",
                AD_LANDNAME = "AUSTRIA",
                AD_NATIONALITAETS_KZ = "AUT"
            });


            modelBuilder.Entity<Ohrmarke>().HasData(new Ohrmarke
            {
                Id = 1,
                MarkenNummer = "091063412848",
                Beschreibung = "Umtausch",
                Datum = DateTime.Now,
                KundeAD_ADRESS_ID = "AD_11000068_ID",
                Kommissionierer = "Schauer",
                Markentyp = "Kuh",
                
            });
        }
    }
}
