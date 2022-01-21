
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
                context.AddRange(
                    new Country() { NameEN = "Egypt", NameAR= "مصر",DailCode="02",ImagePath="", CreatedDate = DateTime.Now, IsDeleted = false },
                    new Country (){ NameEN = "lebanon", NameAR= "لبنان",DailCode="05",ImagePath="", CreatedDate = DateTime.Now, IsDeleted = false }
                    );
                context.SaveChanges();
            }


            
        
            
        }
    }
}
