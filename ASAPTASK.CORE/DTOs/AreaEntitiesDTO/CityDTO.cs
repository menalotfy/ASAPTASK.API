using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASAPTASK.Core.DTOs.AreaEntitiesDTO
{
    public class CityDTO
    {
        public int ID { get; set; }
        public string NameEN { get; set; }
        public string NameAR { get; set; }
        public string Name { get; set; } 
        public string ImagePath { get; set; }
        public int RegionID { get; set; }
        public RegionDTO Region { get; set; }
    }
}
