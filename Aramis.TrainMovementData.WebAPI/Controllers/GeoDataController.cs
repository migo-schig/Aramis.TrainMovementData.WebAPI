using Aramis.TrainMovementData.Data;
using Aramis.TrainMovementData.WebAPI.Data.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Aramis.TrainMovementData.WebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class GeoDataController : ControllerBase
    {
        private readonly IGeoDataRepository geoDataRepository;

        public GeoDataController(IGeoDataRepository geoDataRepository)
        {
            this.geoDataRepository = geoDataRepository;
        }

        [HttpGet("all")]
        public async Task<List<GeoData>> GetAllAsync()
        {
            return await geoDataRepository.GetAllAsync();
        }

        [HttpGet("station/{station}")]
        public async Task<GeoData> GetByStationAsync(string station)
        {
            return await geoDataRepository.GetAsync(station);
        }

        [HttpGet("station/autocomplete/{station}")]
        public async Task<List<GeoData>> GetByStationAutocompleteAsync(string station)
        {
            return await geoDataRepository.GetLikeAsync(station);
        }

        [HttpGet("trainnumber/{trainnumber}/date/{date}")]
        public async Task<List<GeoData>> GetByStationAsync(string trainnumber, DateTime date)
        {
            return await geoDataRepository.GetAsync(trainnumber, date);
        }

        [HttpGet("filters")]
        public async Task<List<GeoData>> GetFiltered(string orderer, string operatorString, string tractionprovider, string traincategory, int min, int max, DateTime from, DateTime to)
        {
            return await geoDataRepository.GetFilteredAsync(min, max, orderer, operatorString, tractionprovider, traincategory, from, to);
        }
    }
}
