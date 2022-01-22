
using ASAPTASK.Core.Enitities.MainEntity;
using System.Collections.Generic;

namespace ASAPTASK.Core.Interfaces.MainInterface
{
    public interface IPersonRepository : IRepository<Person>
    {
        object SetObjectWithLanguage(object obj, string language);
        List<Person> GetPersons();
        object SetPersonDTO(object obj);
    }
}
