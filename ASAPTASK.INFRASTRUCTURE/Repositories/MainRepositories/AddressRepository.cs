using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ASAPTASK.Core.DTOs.AreaEntitiesDTO;
using ASAPTASK.Core.Enitities.MainEntity;
using ASAPTASK.Core.Helper;
using ASAPTASK.Core.Interfaces.MainInterface;
using ASAPTASK.CORE.DTOs.MainEntitiesDTO;
using ASAPTASK.Infrastructure.Data;
using ASAPTASK.Infrastructure.Repositories.AreaRepositories;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace ASAPTASK.Infrastructure.Repositories.MainRepositories
{


    public class AddressRepository : Repository<Address>, IAddressRepository
{
    private readonly ASAPContext _dbContext;
    private readonly IMapper _mapper;
    public AddressRepository(ASAPContext dbContext, IMapper mapper) : base(dbContext)
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
                    _ = ((List<Address>)obj).All(x => { x.Description = x.DescriptionEN; return true; });
                else
                    _ = ((List<Address>)obj).All(x => { x.Description = x.DescriptionAR; return true; });
            }
            else
            {
                if (language == "en")
                {
                    ((Address)obj).Description = ((Address)obj).DescriptionEN;
                }
                else
                {
                    ((Address)obj).Description = ((Address)obj).DescriptionAR;
                }
            }
        }
        return obj;
    }

        public List<Address> getAllAddress()
        {
            return _dbContext.Addresses.Where(x => x.IsDeleted != true).Include(x => x.City).ThenInclude(y => y.Region.Country).ToList();
        }    
        public Address getAddressByID(int ID)
        {
            return _dbContext.Addresses.Where(x => x.IsDeleted != true && x.ID==ID).Include(x => x.City).ThenInclude(y => y.Region.Country).FirstOrDefault();
        }
    public object SetAddressDTO(object obj)
    {
        if (obj != null)
        {
            if (Helper.IsList(obj))
            {
                List<AddressDTO> AddressDTOs = new List<AddressDTO>();
                foreach (var AddressyEntity in (List<Address>)obj)
                {
                    AddressDTO AddressDTO = SetObjectDTO(AddressyEntity);
                    AddressDTOs.Add(AddressDTO);
                }
                return AddressDTOs;
            }
            else
            {

                return SetObjectDTO((Address)obj);
            }
        }
        return obj;
    }

    private AddressDTO SetObjectDTO(Address Address)
    {
        AddressDTO AddressDTO = _mapper.Map<AddressDTO>(Address);
        if (Address.City != null)
        {
            AddressDTO.City = (CityDTO)new CityRepository(_dbContext, _mapper).SetCityDTO(Address.City);
                if (Address.City.Region != null)
                {
                    AddressDTO.RegionID = Address.City.Region.ID;
                    if (Address.City.Region.Country != null)
                        AddressDTO.CountryID= Address.City.Region.Country.ID;
                }
        }


        return AddressDTO;
    }


    }
}
