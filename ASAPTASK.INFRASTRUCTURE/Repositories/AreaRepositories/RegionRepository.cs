using AutoMapper;

using System.Collections.Generic;
using System.Linq;

using ASAPTASK.Core.DTOs.AreaEntitiesDTO;
using ASAPTASK.Core.Enitities.AreaEntity;
using ASAPTASK.Core.Helper;
using ASAPTASK.Core.Interfaces.AreaInterface;
using ASAPTASK.Infrastructure.Data;

namespace ASAPTASK.Infrastructure.Repositories.AreaRepositories
{
 
    public class RegionRepository : Repository<Region>, IRegionRepository
    {
        private readonly ASAPContext _dbContext;
        private readonly IMapper _mapper;
        public RegionRepository(ASAPContext dbContext , IMapper mapper) : base(dbContext)
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
                        _ = ((List<Region>)obj).All(x => { x.Name = x.NameEN; return true; });
                    else
                        _ = ((List<Region>)obj).All(x => { x.Name = x.NameAR; return true; });
                }
                else
                {
                    if (language == "en")
                    {
                        ((Region)obj).Name = ((Region)obj).NameEN;
                    }
                    else
                    {
                        ((Region)obj).Name = ((Region)obj).NameAR;
                    }
                }
            }
            return obj;
        }

        public List<Region> GetAllRegionByCountryID(int CountryID)
        {
            return Get(x => x.IsDeleted != true && x.CountryID == CountryID, new List<string> {}).ToList();
        }
        public object SetRegionDTO(object obj)
        {
            if (obj != null)
            {
                if (Helper.IsList(obj))
                {
                    List<RegionDTO> RegionDTOs = new List<RegionDTO>();
                    foreach (var RegionyEntity in (List<Region>)obj)
                    {
                        RegionDTO RegionDTO = SetObjectDTO(RegionyEntity);
                        RegionDTOs.Add(RegionDTO);
                    }
                    return RegionDTOs;
                }
                else
                {

                    return SetObjectDTO((Region)obj);
                }
            }
            return obj;
        }

        private RegionDTO SetObjectDTO(Region Region)
        {
            RegionDTO RegionDTO = _mapper.Map<RegionDTO>(Region);
            if (Region.Country != null)
            {
                RegionDTO.Country = (CountryDTO)new CountryRepository(_dbContext, _mapper).SetCountryDTO(Region.Country);
            }


            return RegionDTO;
        }
    }
}
