using System;
using System.Collections.Generic;
using System.Text;

namespace ASAPTASK.Core.Enitities
{
    public abstract class BaseEntity
    {
        public int ID { get; set; }
        public bool? IsDeleted { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }


       
    }
}
