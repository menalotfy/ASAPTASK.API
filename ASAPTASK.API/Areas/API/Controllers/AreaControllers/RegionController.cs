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
    [Authorize(AuthenticationSchemes = "Bearer")]
    [ApiController]
    public class RegionController : ControllerBase
    {
        private readonly IRegionRepository _dbContext;

        public RegionController(IRegionRepository RegionRepository)
        {
            _dbContext = RegionRepository;
            try
            {

            }

            catch (Exception)
            {
            }
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult GetAllRegions([FromHeader] string language = "en")
        {
            List<Region> regions = _dbContext.GetAll().OrderBy(x => x.NameEN).ToList();

            return Ok(ResponseHelper.Success(data:_dbContext.SetRegionDTO(  _dbContext.SetObjectWithLanguage(regions, language)), language: language));
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult<City> GetRegion(int ID, [FromHeader] string language = "en")
        {
            var region = _dbContext.GetById(ID);

            if (region == null)
            {
                return NotFound(ResponseHelper.Fail(messageEN: "Record you look for not found", messageAR: "سجل تبحث عنه غير موجود"
              , language: language));
            }

            return Ok(ResponseHelper.Success(data: _dbContext.SetRegionDTO(_dbContext.SetObjectWithLanguage(region, language)), language: language));
        }


        [HttpGet]
        [AllowAnonymous]
        public IActionResult GetAllRegionByCountryID(int CountryID, [FromHeader] string language = "en")
        {
            List<Region> Regions = _dbContext.GetAllRegionByCountryID(CountryID).OrderBy(x => x.NameEN).ToList();
            return Ok(ResponseHelper.Success(data: _dbContext.SetRegionDTO(_dbContext.SetObjectWithLanguage(Regions, language)), language: language));
        }

        // PUT: api/DocumentType/5
        [HttpPost]
        public IActionResult UpdateRegion([FromBody] RegionDTO regionDTO, [FromHeader] string language = "en")
        {
            Region region = _dbContext.GetById(regionDTO.ID);
            try
            {
                region.NameAR = regionDTO.NameAR;
                region.NameEN = regionDTO.NameEN;
                region.CountryID = regionDTO.CountryID;


                _dbContext.Update(region);
            }
            catch (Exception ex)
            {
                if (!RegionExists(regionDTO.ID))
                {
                    return NotFound(ResponseHelper.Fail(messageEN: "Record you look for not found", messageAR: "سجل تبحث عنه غير موجود", language: language));

                }
                else
                {
                    return BadRequest(ResponseHelper.Fail(messageEN: ex.ToString(), language: language));
                }
            }

            return Ok(ResponseHelper.Success(data: _dbContext.SetObjectWithLanguage(region, language), language: language));
        }

        // POST: api/DocumentType
        [HttpPost]
        public ActionResult<Region> AddRegion([FromBody] RegionDTO regionDTO, [FromHeader] string language = "en")
        {
            try
            {
                Region region = new Region();
                region.NameAR = regionDTO.NameAR;
                region.NameEN = regionDTO.NameEN;
                region.CountryID = regionDTO.CountryID;
                _dbContext.Add(region);

                return Ok(ResponseHelper.Success(data: _dbContext.SetObjectWithLanguage(region, language), language: language));
            }
            catch (Exception ex)
            {
                return BadRequest(ResponseHelper.Fail(messageEN: ex.ToString(), language: language));
            }
        }

        // DELETE: api/DocumentType/5
        [HttpDelete]
        public ActionResult<Region> DeleteRegion(int ID, [FromHeader] string language = "en")
        {
            var region = _dbContext.GetById(ID);
            if (region == null)
            {
                return NotFound(ResponseHelper.Fail(messageEN: "Record you look for not found", messageAR: "سجل تبحث عنه غير موجود", language: language));
            }

            _dbContext.Delete(region);
            return Ok(ResponseHelper.Success(data: _dbContext.SetObjectWithLanguage(region, language), language: language));
        }

        private bool RegionExists(int ID)
        {
            return _dbContext.Exists(ID);
        }
    }
}
