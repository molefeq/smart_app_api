using Microsoft.AspNetCore.Mvc;
using SmartData.Data.ViewModels;
using SmartData.Service.ReferenceData;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SmartData.Api.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]/[action]")]
    public class ReferenceDataController : Controller
    {
        private IReferenceDataService referenceDataService;

        public ReferenceDataController(IReferenceDataService referenceDataService)
        {
            this.referenceDataService = referenceDataService;
        }

        [HttpGet]
        [ProducesResponseType(typeof(StaticDataModel), 200)]
        public IActionResult GetStaticData()
        {
            return Ok(referenceDataService.GetStaticData());
        }

        [HttpGet]
        [ProducesResponseType(typeof(List<ReferenceDataModel>), 200)]
        public IActionResult GetCountries()
        {
            return Ok(referenceDataService.GetCountries());
        }

        [HttpPost]
        public async Task<IActionResult> BulkInsertCountries()
        {
            await referenceDataService.BulkInsertCountries();

            return Ok();
        }

        [HttpPost("{baseCurrency}")]
        public async Task<IActionResult> BulkInsertExchangeRates([FromRoute] string baseCurrency)
        {
            await referenceDataService.BulkInsertExchangeRates(baseCurrency);

            return Ok();
        }

        //[HttpGet]
        //[ProducesResponseType(typeof(List<ReferenceDataModel>), 200)]
        //public IActionResult GetProvinces()
        //{
        //    return Ok(referenceDataService.GetProvinces());
        //}


        //[HttpGet]
        //[ProducesResponseType(typeof(List<ReferenceDataModel>), 200)]
        //public IActionResult GeTitles()
        //{
        //    return Ok(referenceDataService.GeTitles());
        //}

        //[HttpGet]
        //[ProducesResponseType(typeof(List<ReferenceDataModel>), 200)]
        //public IActionResult GetMaritalStatuses()
        //{
        //    return Ok(referenceDataService.GetMaritalStatuses());
        //}

        //[HttpGet]
        //[ProducesResponseType(typeof(List<ReferenceDataModel>), 200)]
        //public IActionResult GetEthnicGroups()
        //{
        //    return Ok(referenceDataService.GetEthnicGroups());
        //}

        //[HttpGet]
        //[ProducesResponseType(typeof(List<ReferenceDataModel>), 200)]
        //public IActionResult GetLanguages()
        //{
        //    return Ok(referenceDataService.GetLanguages());
        //}
    }
}