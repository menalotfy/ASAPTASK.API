using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASAPTASK.CORE.DTOs.MainEntitiesDTO
{
    public class PersonDTO
    {
        public int ID { get; set; }
        public string NameEN { get; set; }
        public string NameAR { get; set; }
        public string Name { get; set; }

        public string Email { get; set; }
        public string Phone { get; set; }


        public int CountryID { get; set; }
        public int RegionID { get; set; }

        public int? CityID { get; set; }

        public int? AddressID { get; set; }
        public AddressDTO Address { get; set; }

    }
}
