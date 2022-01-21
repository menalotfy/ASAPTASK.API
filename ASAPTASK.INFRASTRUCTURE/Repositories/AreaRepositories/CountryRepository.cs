using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using ASAPTASK.Core.DTOs.AreaEntitiesDTO;
using ASAPTASK.Core.Enitities.AreaEntity;

using ASAPTASK.Core.Helper;
using ASAPTASK.Core.Interfaces.AreaInterface;

using ASAPTASK.Infrastructure.Data;

namespace ASAPTASK.Infrastructure.Repositories.AreaRepositories
{
 
    public class CountryRepository : Repository<Country>, ICountryRepository
    {
        private readonly ASAPContext _dbContext;
        private readonly IMapper _mapper;
        public CountryRepository(ASAPContext dbContext , IMapper mapper) : base(dbContext)
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
                        _ = ((List<Country>)obj).All(x => { x.Name = x.NameEN; return true; });
                    else
                        _ = ((List<Country>)obj).All(x => { x.Name = x.NameAR; return true; });
                }
                else
                {
                    if (language == "en")
                    {
                        ((Country)obj).Name = ((Country)obj).NameEN;
                    }
                    else
                    {
                        ((Country)obj).Name = ((Country)obj).NameAR;
                    }
                }
            }
            return obj;
        }



        public object SetCountryDTO(object country)
        {
            if (country != null)
            {
                if (Helper.IsList(country))
                {
                    List<CountryDTO> CountriesDTO = _mapper.Map<List<CountryDTO>>((List<Country>)country);
                    return CountriesDTO;
                }
                else
                {
                    CountryDTO CountryDTO = _mapper.Map<CountryDTO>((Country)country);
                    return CountryDTO;
                }
               
            }
            else
                return null;
        }
    
    }
}
