using System.Collections.Generic;

using ASAPTASK.Core.Enitities.AreaEntity;
using ASAPTASK.Core.Interfaces.MainInterface;

namespace ASAPTASK.Core.Interfaces.AreaInterface
{

    public interface ICityRepository : IRepository<City>
    {
        object SetObjectWithLanguage(object obj, string language);
     

        List<City> GetAllCitiesByCountryID(int countryID);
          
        List<City> GetAllCitiesByRegionID(int RegionID);
        object SetCityDTO(object obj);
    }
}
