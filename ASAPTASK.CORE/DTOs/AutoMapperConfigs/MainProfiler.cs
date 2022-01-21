using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ASAPTASK.Core.DTOs.AreaEntitiesDTO;

using ASAPTASK.Core.Enitities.AreaEntity;
using ASAPTASK.Core.Enitities.MainEntity;
using ASAPTASK.CORE.DTOs.MainEntitiesDTO;

namespace ASAPTASK.Core.DTOs.AutoMapperConfigs
{
    public class MainProfiler : Profile
    {
        public MainProfiler()
        {
            //areaaEntity
            CreateMap<Country, CountryDTO>().ReverseMap();
            CreateMap<City, CityDTO>().ReverseMap();
            CreateMap<Region, RegionDTO>().ReverseMap();

            CreateMap<Person, PersonDTO>().ReverseMap();
            CreateMap<Address, AddressDTO>().ReverseMap();


            
        }
    }
}
