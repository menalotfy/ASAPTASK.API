using ASAPTASK.Core.Enitities.AreaEntity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;


namespace ASAPTASK.Core.Enitities.MainEntity
{
    public class Address : BaseEntity
    {
        public string DescriptionEN { get; set; }
        public string DescriptionAR { get; set; }

        public string PostalCode { get; set; }
        public int? CityID { get; set; }
        public City City { get; set; }

        [NotMapped]
        public string Description { get; set; }

       
    }
}
