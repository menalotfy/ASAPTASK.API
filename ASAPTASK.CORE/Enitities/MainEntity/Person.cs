using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;


namespace ASAPTASK.Core.Enitities.MainEntity
{
    public class Person : BaseEntity
    {
        public string NameEN { get; set; }
        public string NameAR { get; set; }
        public string FamilyName { get; set; }
        public string ProfileImagePath { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }


     
        public int? AddressID { get; set; }
        public Address Address { get; set; }
      

        [NotMapped]
        public string Name { get; set; }

    }
}
