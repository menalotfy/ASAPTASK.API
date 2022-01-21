using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ASAPTASK.Core.Enitities.AreaEntity;
using ASAPTASK.Core.Interfaces.MainInterface;

namespace ASAPTASK.Core.Interfaces.AreaInterface
{
   public interface IRegionRepository : IRepository<Region>
    {
        object SetObjectWithLanguage(object obj, string language);

        List<Region> GetAllRegionByCountryID(int CountryID);
        object SetRegionDTO(object region);
    }

}
