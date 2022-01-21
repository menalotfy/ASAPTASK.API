using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ASAPTASK.Core.Enitities.MainEntity;
using ASAPTASK.Core.Helper;
using ASAPTASK.Core.Interfaces.MainInterface;
using ASAPTASK.CORE.DTOs.MainEntitiesDTO;
using ASAPTASK.Infrastructure.Data;
using AutoMapper;

namespace ASAPTASK.Infrastructure.Repositories.MainRepositories
{
 
    public class PersonRepository : Repository<Person>, IPersonRepository
    {
        private readonly ASAPContext _dbContext;
        private readonly IMapper _mapper;
        public PersonRepository(ASAPContext dbContext,IMapper mapper ) : base(dbContext)
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
                        _ = ((List<Person>)obj).All(x => { x.Name = x.NameEN; return true; });
                    else
                        _ = ((List<Person>)obj).All(x => { x.Name = x.NameAR; return true; });
                }
                else
                {
                    if (language == "en")
                    {
                        ((Person)obj).Name = ((Person)obj).NameEN;
                    }
                    else
                    {
                        ((Person)obj).Name = ((Person)obj).NameAR;
                    }
                }
            }
            return obj;
        }


        public object SetPersonDTO(object obj)
        {
            if (obj != null)
            {
                if (Helper.IsList(obj))
                {
                    List<PersonDTO> PersonDTOs = new List<PersonDTO>();
                    foreach (var PersonyEntity in (List<Person>)obj)
                    {
                        PersonDTO PersonDTO = SetObjectDTO(PersonyEntity);
                        PersonDTOs.Add(PersonDTO);
                    }
                    return PersonDTOs;
                }
                else
                {

                    return SetObjectDTO((Person)obj);
                }
            }
            return obj;
        }

        private PersonDTO SetObjectDTO(Person Person)
        {
            PersonDTO PersonDTO = _mapper.Map<PersonDTO>(Person);
            if (Person.Address != null)
            {
                PersonDTO.Address =(AddressDTO) new AddressRepository(_dbContext,_mapper).SetAddressDTO(Person.Address);
            }

            return PersonDTO;
        }

    }
}
