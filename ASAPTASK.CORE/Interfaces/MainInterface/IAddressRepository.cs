
using ASAPTASK.Core.Enitities.MainEntity;
using System.Collections.Generic;

namespace ASAPTASK.Core.Interfaces.MainInterface
{
    public interface IAddressRepository : IRepository<Address>
    {
        object SetObjectWithLanguage(object obj, string language);
        object SetAddressDTO(object obj);
         List<Address> getAllAddress();
        Address getAddressByID(int ID);
    }
}
