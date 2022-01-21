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
    public class AddressController : ControllerBase
    {
        private readonly IAddressRepository _dbContext;

        public AddressController(IAddressRepository AddressRepository)
        {
            _dbContext = AddressRepository;
            
        }
        [HttpGet]
        [AllowAnonymous]
        [ResponseCache(Duration = 5000)]
        public IActionResult GetAllAddresss([FromHeader] string language = "en")
        {
            List<Address> Addresss = _dbContext.GetAll().ToList();

            return Ok(ResponseHelper.Success(data: _dbContext.SetObjectWithLanguage(Addresss, language), language: language));
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult<Address> GetAddress(int ID, [FromHeader] string language = "en")
        {
            var Address = _dbContext.GetById(ID);

            if (Address == null)
            {
                return NotFound(ResponseHelper.Fail(messageEN: "Record you look for not found", messageAR: "سجل تبحث عنه غير موجود"
              , language: language));
            }

            return Ok(ResponseHelper.Success(data: _dbContext.SetObjectWithLanguage(Address, language), language: language));
        }

        // PUT: api/Address/5
        [HttpPost]
        public IActionResult UpdateAddress([FromBody] AddressDTO AddressDto, [FromHeader] string language = "en")
        {
            Address Address = _dbContext.GetById(AddressDto.ID);
            try
            {
                Address.DescriptionAR = AddressDto.DescriptionAR;
                Address.DescriptionEN = AddressDto.DescriptionEN;
                Address.CityID = AddressDto.CityID;
                Address.PostalCode = AddressDto.PostalCode;
                       _dbContext.Update(Address);
            }
            catch (Exception ex)
            {
                if (!AddressExists(AddressDto.ID))
                {
                    return NotFound(ResponseHelper.Fail(messageEN: "Record you look for not found", messageAR: "سجل تبحث عنه غير موجود", language: language));

                }
                else
                {
                    return BadRequest(ResponseHelper.Fail(messageEN: ex.ToString(), language: language));
                }
            }

            return Ok(ResponseHelper.Success(data: _dbContext.SetObjectWithLanguage(Address, language), language: language));
        }

        // POST: api/Address
        [HttpPost]
        public ActionResult<Address> AddAddress([FromBody] AddressDTO AddressDto, [FromHeader] string language = "en")
        {
            try
            {
                Address Address = new Address();
                Address.DescriptionAR = AddressDto.DescriptionAR;
                Address.DescriptionEN = AddressDto.DescriptionEN;
                Address.CityID = AddressDto.CityID;
                Address.PostalCode = AddressDto.PostalCode;
                _dbContext.Add(Address);

                return Ok(ResponseHelper.Success(data: _dbContext.SetObjectWithLanguage(Address, language), language: language));
            }
            catch (Exception ex)
            {
                return BadRequest(ResponseHelper.Fail(messageEN: ex.ToString(), language: language));
            }
        }

        // DELETE: api/Address/5
        [HttpDelete]
        public ActionResult<Address> DeleteAddress(int ID, [FromHeader] string language = "en")
        {
            var Address = _dbContext.GetById(ID);
            if (Address == null)
            {
                return NotFound(ResponseHelper.Fail(messageEN: "Record you look for not found", messageAR: "سجل تبحث عنه غير موجود", language: language));
            }

            _dbContext.Delete(Address);
            return Ok(ResponseHelper.Success(data: _dbContext.SetObjectWithLanguage(Address, language), language: language));
        }

        private bool AddressExists(int ID)
        {
            return _dbContext.Exists(ID);
        }
    }
}
