using Microsoft.AspNetCore.Authorization;

using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

using ASAPTASK.API.Helpers;

using ASAPTASK.Core.Enitities.MainEntity;
using ASAPTASK.Core.Interfaces.MainInterface;
using ASAPTASK.CORE.DTOs.MainEntitiesDTO;

namespace ASAPTASK.API.Areas.API.Controllers.MainControllers
{
    [Route("API/[controller]/[action]")]
   
    [ApiController]
    public class PersonController : ControllerBase
    {
        private readonly IPersonRepository _dbContext;

        public PersonController(IPersonRepository PersonRepository)
        {
            _dbContext = PersonRepository;
            
        }
        [HttpGet]
        [AllowAnonymous]
        [ResponseCache(Duration = 5000)]
        public IActionResult GetAllPersons([FromHeader] string language = "en")
        {
            List<Person> Persons = _dbContext.GetPersons().OrderBy(x => x.NameEN).ToList();

            return Ok(ResponseHelper.Success(data:_dbContext.SetPersonDTO( _dbContext.SetObjectWithLanguage(Persons, language)), language: language));
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult<Person> GetPerson(int ID, [FromHeader] string language = "en")
        {
            var Person = _dbContext.GetById(ID);

            if (Person == null)
            {
                return NotFound(ResponseHelper.Fail(messageEN: "Record you look for not found", messageAR: "سجل تبحث عنه غير موجود"
              , language: language));
            }

            return Ok(ResponseHelper.Success(data: _dbContext.SetObjectWithLanguage(Person, language), language: language));
        }

        // PUT: api/Person/5
        [HttpPost]
        public IActionResult UpdatePerson([FromBody] PersonDTO PersonDto, [FromHeader] string language = "en")
        {
            Person Person = _dbContext.GetById(PersonDto.ID);
            try
            {
                Person.NameAR = PersonDto.NameAR;
                Person.NameEN = PersonDto.NameEN;
                Person.Email = PersonDto.Email;
                Person.AddressID = PersonDto.AddressID;
                Person.Phone = PersonDto.Phone;
                _dbContext.Update(Person);
            }
            catch (Exception ex)
            {
                if (!PersonExists(PersonDto.ID))
                {
                    return NotFound(ResponseHelper.Fail(messageEN: "Record you look for not found", messageAR: "سجل تبحث عنه غير موجود", language: language));

                }
                else
                {
                    return BadRequest(ResponseHelper.Fail(messageEN: ex.ToString(), language: language));
                }
            }

            return Ok(ResponseHelper.Success(data: _dbContext.SetObjectWithLanguage(Person, language), language: language));
        }

        // POST: api/Person
        [HttpPost]
        public ActionResult<Person> AddPerson([FromBody] PersonDTO PersonDto, [FromHeader] string language = "en")
        {
            try
            {
                Person Person = new Person();
                Person.NameAR = PersonDto.NameAR;
                Person.NameEN = PersonDto.NameEN;
                Person.Email = PersonDto.Email;
                Person.AddressID = PersonDto.AddressID;
                Person.Phone = PersonDto.Phone;
              
                _dbContext.Add(Person);

                return Ok(ResponseHelper.Success(data: _dbContext.SetObjectWithLanguage(Person, language), language: language));
            }
            catch (Exception ex)
            {
                return BadRequest(ResponseHelper.Fail(messageEN: ex.ToString(), language: language));
            }
        }

        // DELETE: api/Person/5
        [HttpDelete]
        public ActionResult<Person> DeletePerson(int ID, [FromHeader] string language = "en")
        {
            var Person = _dbContext.GetById(ID);
            if (Person == null)
            {
                return NotFound(ResponseHelper.Fail(messageEN: "Record you look for not found", messageAR: "سجل تبحث عنه غير موجود", language: language));
            }

            _dbContext.Delete(Person);
            return Ok(ResponseHelper.Success(data: _dbContext.SetObjectWithLanguage(Person, language), language: language));
        }

        private bool PersonExists(int ID)
        {
            return _dbContext.Exists(ID);
        }
    }
}
