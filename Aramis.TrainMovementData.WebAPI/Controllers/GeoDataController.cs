using Aramis.TrainMovementData.Data;
using Aramis.TrainMovementData.WebAPI.Data.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using System;
using System.Collections.Generic;

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
        public IEnumerable<GeoData> GetAll()
        {
            return geoDataRepository.GetAll();
        }

        [HttpGet("station/{station}")]
        public GeoData GetByStation(string station)
        {
            return geoDataRepository.Get(station);
        }

        [HttpGet("station/autocomplete/{station}")]
        public IEnumerable<GeoData> GetByStationAutocomplete(string station)
        {
            return geoDataRepository.GetLike(station);
        }

        [HttpGet("trainnumber/{trainnumber}/date/{date}")]
        public IEnumerable<GeoData> GetByStation(string trainnumber, DateTime date)
        {
            return geoDataRepository.Get(trainnumber, date);
        }

        [HttpGet("filters")]
        public IEnumerable<GeoData> GetFiltered(string orderer, string operatorString, string tractionprovider, string traincategory, int min, int max, DateTime from, DateTime to)
        {
            return geoDataRepository.GetFiltered(min, max, orderer, operatorString, tractionprovider, traincategory, from, to);
        }
    }
}
