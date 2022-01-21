using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ASAPTASK.Core.Enitities.AreaEntity;
using ASAPTASK.Core.Interfaces.MainInterface;


namespace ASAPTASK.Core.Interfaces.AreaInterface
{
   public interface ICountryRepository: IRepository<Country>
    {
        object SetObjectWithLanguage(object obj, string language);
   

        object SetCountryDTO(object country);
    }
}
