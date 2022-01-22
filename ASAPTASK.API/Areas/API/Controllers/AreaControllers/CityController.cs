using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using ASAPTASK.API.Helpers;
using ASAPTASK.Core.DTOs.AreaEntitiesDTO;
using ASAPTASK.Core.Enitities.AreaEntity;
using ASAPTASK.Core.Interfaces.AreaInterface;

namespace ASAPTASK.API.Areas.API.Controllers.AreaControllers
{

    [Route("API/[controller]/[action]")]
    [ApiController]
    public class CityController : ControllerBase
    {
        private readonly ICityRepository _dbContext;
        private readonly IMapper _mapper;

        public CityController(ICityRepository CityRepository, IMapper mapper)
        {
            _dbContext = CityRepository;
            _mapper = mapper;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="language"></param>
        /// <returns></returns>
       
        [HttpGet]
        [AllowAnonymous]
        public IActionResult GetAllCities([FromHeader] string language = "en")
        {
          

            List<City> Cities = _dbContext.GetAll().OrderBy(x => x.NameEN).ToList();
            return Ok(ResponseHelper.Success(data: _dbContext.SetCityDTO( _dbContext.SetObjectWithLanguage(Cities, language)), language: language));
        }  
        
        //mi
        [HttpGet]
        [AllowAnonymous]
        public IActionResult GetAllCitiesByCountryID(int CountryID,[FromHeader] string language = "en")
        {
            List<City> Cities = _dbContext.GetAllCitiesByCountryID(CountryID).OrderBy(x => x.NameEN).ToList();
            Cities=(List<City>)_dbContext.SetObjectWithLanguage(Cities, language);
            List<CityDTO> CityDTOs = _mapper.Map<List<CityDTO>>(Cities);
            return Ok(ResponseHelper.Success(data: CityDTOs, language: language));
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult GetAllCitiesByRegionID(int RigonID, [FromHeader] string language = "en")
        {
            List<City> Cities = _dbContext.GetAllCitiesByRegionID(RigonID).OrderBy(x => x.NameEN).ToList();
            return Ok(ResponseHelper.Success(data: _dbContext.SetCityDTO( _dbContext.SetObjectWithLanguage(Cities, language)), language: language));
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult GetCity(int ID, [FromHeader] string language = "en")
        {
            var city = _dbContext.GetById(ID);

            if (city == null)
            {
                return NotFound(ResponseHelper.Fail(messageEN: "Record you look for not found", messageAR: "سجل تبحث عنه غير موجود"
              , language: language));
            }

            return Ok(ResponseHelper.Success(data:_dbContext.SetCityDTO( _dbContext.SetObjectWithLanguage(city, language)), language: language));
        }

        // PUT: api/DocumentType/5
        [HttpPost]
        public IActionResult UpdateCity([FromBody] CityDTO cityDTO, [FromHeader] string language = "en")
        {
            City city = _dbContext.GetById(cityDTO.ID);
            try
            {
                city.NameAR = cityDTO.NameAR;
                city.NameEN = cityDTO.NameEN;
                city.RegionID = cityDTO.RegionID;
                city.ImagePath = cityDTO.ImagePath;


                _dbContext.Update(city);
            }
            catch (Exception ex)
            {
                if (!CityExists(cityDTO.ID))
                {
                    return NotFound(ResponseHelper.Fail(messageEN: "Record you look for not found", messageAR: "سجل تبحث عنه غير موجود", language: language));

                }
                else
                {
                    return BadRequest(ResponseHelper.Fail(messageEN: ex.ToString(), language: language));
                }
            }

            return Ok(ResponseHelper.Success(data: _dbContext.SetObjectWithLanguage(city, language), language: language));
        }

        // POST: api/DocumentType
        [HttpPost]
        public ActionResult AddCity([FromBody] CityDTO cityDTO, [FromHeader] string language = "en")
        {
            try
            {
                City city = new City();
                city.NameAR = cityDTO.NameAR;
                city.NameEN = cityDTO.NameEN;
                city.RegionID = cityDTO.RegionID;

                city.ImagePath = cityDTO.ImagePath;
                _dbContext.Add(city);

                return Ok(ResponseHelper.Success(data: _dbContext.SetObjectWithLanguage(city, language), language: language));
            }
            catch (Exception ex)
            {
                return BadRequest(ResponseHelper.Fail(messageEN: ex.ToString(), language: language));
            }
        }

        // DELETE: api/DocumentType/5
        [HttpDelete]
        public ActionResult<City> DeleteCity(int ID, [FromHeader] string language = "en")
        {
            var city = _dbContext.GetById(ID);
            if (city == null)
            {
                return NotFound(ResponseHelper.Fail(messageEN: "Record you look for not found", messageAR: "سجل تبحث عنه غير موجود", language: language));
            }

            _dbContext.Delete(city);
            return Ok(ResponseHelper.Success(data: _dbContext.SetObjectWithLanguage(city, language), language: language));
        }

        private bool CityExists(int ID)
        {
            return _dbContext.Exists(ID);
        }
    }
}
