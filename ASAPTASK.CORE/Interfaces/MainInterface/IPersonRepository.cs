
using ASAPTASK.Core.Enitities.MainEntity;

namespace ASAPTASK.Core.Interfaces.MainInterface
{
    public interface IPersonRepository : IRepository<Person>
    {
        object SetObjectWithLanguage(object obj, string language);
    }
}
