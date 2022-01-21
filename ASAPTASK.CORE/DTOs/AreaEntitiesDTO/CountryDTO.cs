
using System.Collections.Generic;

 
namespace ASAPTASK.Core.DTOs.AreaEntitiesDTO
{
    public class CountryDTO
    {
        public int ID { get; set; }
        public string NameEN { get; set; }
        public string NameAR { get; set; }

 
        public string ImagePath { get; set; }
        public string Name { get; set; }

        public string DailCode { get; set; }



        public List<RegionDTO> Regions { get; set; }

    }
}
