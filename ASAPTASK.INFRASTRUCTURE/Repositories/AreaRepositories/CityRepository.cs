using AutoMapper;

using System.Collections.Generic;
using System.Linq;


using ASAPTASK.Infrastructure.Data;
using ASAPTASK.Core.Enitities.AreaEntity;
using ASAPTASK.Core.Interfaces.AreaInterface;
using ASAPTASK.Core.Helper;
using ASAPTASK.Core.DTOs.AreaEntitiesDTO;


namespace ASAPTASK.Infrastructure.Repositories.AreaRepositories
{
 

    public class CityRepository : Repository<City>, ICityRepository
    {
        private readonly ASAPContext _dbContext;
        private readonly IMapper _mapper;

        public CityRepository(ASAPContext dbContext , IMapper mapper) : base(dbContext)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }
        public object SetObjectWithLanguage(object obj, string language)
        {
            if (obj != null)
            {
                if (Helper.IsList(obj))
                {
                    if (language == "en")
                        _ = ((List<City>)obj).All(x => { x.Name = x.NameEN; return true; });
                    else
                        _ = ((List<City>)obj).All(x => { x.Name = x.NameAR; return true; });
                }
                else
                {
                    if (language == "en")
                    {
                        ((City)obj).Name = ((City)obj).NameEN;
                    }
                    else
                    {
                        ((City)obj).Name = ((City)obj).NameAR;
                    }
                }
            }
            return obj;
        }

        
        public List<City> GetAllCitiesByCountryID(int countryID)
        {
            return  Get(x => x.IsDeleted != true && x.Region.CountryID == countryID, new List<string>{ "Region" }).ToList();

        }
        public List<City> GetAllCitiesByRegionID(int RegionID)
        {
            return Get(x => x.IsDeleted != true && x.RegionID == RegionID, new List<string> { "Region" }).ToList();
        }

        public object SetCityDTO(object obj)
        {
            if (obj != null)
            {
                if (Helper.IsList(obj))
                {
                    List<CityDTO> CityDTOs = new List<CityDTO>();
                    foreach (var CityyEntity in (List<City>)obj)
                    {
                        CityDTO CityDTO = SetObjectDTO(CityyEntity);
                        CityDTOs.Add(CityDTO);
                    }
                    return CityDTOs;
                }
                else
                {

                    return SetObjectDTO((City)obj);
                }
            }
            return obj;
        }

        private CityDTO SetObjectDTO(City City)
        {
            CityDTO CityDTO = _mapper.Map<CityDTO>(City);
            if (City.Region != null)
            {
                CityDTO.Region = (RegionDTO)new RegionRepository(_dbContext, _mapper).SetRegionDTO(City.Region);

            }


            return CityDTO;
        }
    }
}
