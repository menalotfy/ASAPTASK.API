using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ASAPTASK.Core.Enitities.AreaEntity
{
    public class Country : BaseEntity
    {
        public string NameEN { get; set; }
        public string NameAR { get; set; }

        public string DailCode { get; set; }

      
        [NotMapped]
        public string Name { get; set; }

        public string ImagePath { get; set; }
       
    }
}
