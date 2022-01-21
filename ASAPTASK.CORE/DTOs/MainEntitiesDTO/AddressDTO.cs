using ASAPTASK.Core.DTOs.AreaEntitiesDTO;



namespace ASAPTASK.CORE.DTOs.MainEntitiesDTO
{
    public class AddressDTO
    {
        public int ID { get; set; }
        public string DescriptionEN { get; set; }
        public string DescriptionAR { get; set; }

        public string PostalCode { get; set; }
        public int? CityID { get; set; }

        public CityDTO City { get; set; }


        public string Description { get; set; }
    }
}
