using Microsoft.AspNetCore.Authorization;

using Microsoft.AspNetCore.Mvc;
using System;

using System.Collections.Generic;

using System.Linq;

using ASAPTASK.API.Helpers;
using ASAPTASK.Core.DTOs.AreaEntitiesDTO;
using ASAPTASK.Core.Enitities.AreaEntity;
using ASAPTASK.Core.Interfaces.AreaInterface;

using System.Net;

using Microsoft.AspNetCore.Mvc.Infrastructure;


namespace ASAPTASK.API.Areas.API.Controllers.AreaControllers
{
    [Route("API/[controller]/[action]")]

    [ApiController]
    public class CountryController : ControllerBase
    {
        private readonly ICountryRepository _dbContext;
       
        
        public CountryController(ICountryRepository CountryRepository  )
        {
            _dbContext = CountryRepository;
          
        }

        [HttpGet]
        [AllowAnonymous]
       
        public IActionResult GetAllCountries([FromHeader] string language = "en")
        {
            List<Country> Countries = _dbContext.GetAll().OrderBy(x => x.NameEN).ToList();
            return Ok(ResponseHelper.Success(data: _dbContext.SetCountryDTO((List<Country>)_dbContext.SetObjectWithLanguage(Countries, language)), language: language));
        }


  

        
        [HttpGet]
        [AllowAnonymous]
        public ActionResult<Country> GetCountry(int ID, [FromHeader] string language = "en")
        {
            var country = _dbContext.GetById(ID);

            if (country == null)
            {
                return NotFound(ResponseHelper.Fail(messageEN: "Record you look for not found", messageAR: "سجل تبحث عنه غير موجود"
              , language: language));
            }

            return Ok(ResponseHelper.Success(data:_dbContext.SetCountryDTO((Country)_dbContext.SetObjectWithLanguage(country, language)), language: language));
        }

        // Post: api/Country/5
        [HttpPost]
        public IActionResult UpdateCountry([FromBody] CountryDTO CountrtyDTO, [FromHeader] string language = "en")
        {
            Country country = _dbContext.GetById(CountrtyDTO.ID);
            try
            {
                country.NameAR = CountrtyDTO.NameAR;
                country.NameEN = CountrtyDTO.NameEN;
              
                country.ImagePath = CountrtyDTO.ImagePath;
                country.DailCode = CountrtyDTO.DailCode;
             
                _dbContext.Update(country);
            }
            catch (Exception ex)
            {
                if (!CountryExists(CountrtyDTO.ID))
                {
                    return NotFound(ResponseHelper.Fail(messageEN: "Record you look for not found", messageAR: "سجل تبحث عنه غير موجود", language: language));

                }
                else
                {
                    return BadRequest(ResponseHelper.Fail(messageEN: ex.ToString(), language: language));
                }
            }

            return Ok(ResponseHelper.Success(data: _dbContext.SetCountryDTO((Country)_dbContext.SetObjectWithLanguage(country, language)), language: language));
        }

        // POST: api/Country
        [HttpPost]
        public ActionResult<Country> AddCountry([FromBody] CountryDTO CountryDTO, [FromHeader] string language = "en")
        {
            try
            {
                Country country = new Country();
                country.NameAR = CountryDTO.NameAR;
                country.NameEN = CountryDTO.NameEN;
                
                country.ImagePath = CountryDTO.ImagePath;
                country.DailCode = CountryDTO.DailCode;

                _dbContext.Add(country);

                return Ok(ResponseHelper.Success(data: _dbContext.SetCountryDTO((Country)_dbContext.SetObjectWithLanguage(country, language)), language: language));
            }
            catch (Exception ex)
            {
                return BadRequest(ResponseHelper.Fail(messageEN: ex.ToString(), language: language));
            }
        }

        // DELETE: api/Country/5
        [HttpDelete]
        public ActionResult<Country> DeleteCountry(int ID, [FromHeader] string language = "en")
        {
            var Country = _dbContext.GetById(ID);
            if (Country == null)
            {
                return NotFound(ResponseHelper.Fail(messageEN: "Record you look for not found", messageAR: "سجل تبحث عنه غير موجود", language: language));
            }

            _dbContext.Delete(Country);
            return Ok(ResponseHelper.Success(data: _dbContext.SetCountryDTO((Country)_dbContext.SetObjectWithLanguage(Country, language)), language: language));
        }

        private bool CountryExists(int ID)
        {
            return _dbContext.Exists(ID);
        }

        
     




    }
}
