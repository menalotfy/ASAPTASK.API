using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;


namespace ASAPTASK.Core.Enitities.AreaEntity
{
    public class City : BaseEntity
    {
        public string NameEN { get; set; }
        public string NameAR { get; set; }
        [NotMapped]
        public string Name { get; set; }
        public string ImagePath { get; set; }
        public int RegionID { get; set; }
        public Region Region { get; set; }
     
    }
}
