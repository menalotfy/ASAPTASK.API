
using ASAPTASK.Core.Enitities.MainEntity;

namespace ASAPTASK.Core.Interfaces.MainInterface
{
    public interface IAddressRepository : IRepository<Address>
    {
        object SetObjectWithLanguage(object obj, string language);
    }
}
