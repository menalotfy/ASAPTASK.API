

namespace ASAPTASK.Core.DTOs.AreaEntitiesDTO
{
    public class RegionDTO
    {
        public int ID { get; set; }
        public string NameEN { get; set; }
        public string NameAR { get; set; }
        public string Name { get; set; }
        public int CountryID { get; set; }
        public CountryDTO Country { get; set; }
    }
}
