
using System;
using System.Linq;


using ASAPTASK.Core.Interfaces;
using ASAPTASK.Core.Enitities.AreaEntity;

namespace ASAPTASK.Infrastructure.Data
{
   public class DBInitializer : IDBInitializer
    {
        private readonly ASAPContext _db;


        public DBInitializer(ASAPContext db)
        {
            _db = db;
       
        }

        public async void Initialize()
        {
               
            _db.ChangeTracker.AutoDetectChangesEnabled = true;
            Seed(_db);

           
        }

        public void Seed(ASAPContext context)
        {
            // Seed initial data

            if (!context.Countries.Any())
            {
                context.Countries.AddRange(
                    new Country() { NameEN = "Egypt", NameAR= "مصر",DailCode="02",ImagePath="", CreatedDate = DateTime.Now, IsDeleted = false },
                    new Country (){ NameEN = "lebanon", NameAR= "لبنان",DailCode="05",ImagePath="", CreatedDate = DateTime.Now, IsDeleted = false }
                    );
                context.SaveChanges();
            }

            if (!context.Regions.Any())
            {
                context.Regions.AddRange(
                    new Region() { NameEN = "Cairo", NameAR = "القاهرة", CountryID = 1, CreatedDate = DateTime.Now, IsDeleted = false },
                    new Region() { NameEN = "Beirut", NameAR = "بيروت", CountryID = 2, CreatedDate = DateTime.Now, IsDeleted = false }
                    );
                context.SaveChanges();
            }
            if (!context.Cities.Any())
            {
                context.Cities.AddRange(
                    new City() { NameEN = "zaito", NameAR = "زيتون", RegionID = 1, CreatedDate = DateTime.Now, IsDeleted = false },
                    new City() { NameEN = "Beirut City", NameAR = "بيروت", RegionID = 2, CreatedDate = DateTime.Now, IsDeleted = false }
                    );
                context.SaveChanges();
            }





        }
    }
}
