using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;


namespace ASAPTASK.Core.Enitities.AreaEntity
{
    public class Region : BaseEntity
    {
        public string NameEN { get; set; }
        public string NameAR { get; set; }
        [NotMapped]
        public string Name { get; set; }
        public int CountryID { get; set; }
        public Country Country { get; set; }
      
    }
}
