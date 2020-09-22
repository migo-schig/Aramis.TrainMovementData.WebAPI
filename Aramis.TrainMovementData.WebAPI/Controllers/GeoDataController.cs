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

        //// GET api/<ValuesController>/5
        //[HttpGet("{id}")]
        //public string Get(int id)
        //{
        //    return "value";
        //}

        //// POST api/<ValuesController>
        //[HttpPost]
        //public void Post([FromBody] string value)
        //{
        //}

        //// PUT api/<ValuesController>/5
        //[HttpPut("{id}")]
        //public void Put(int id, [FromBody] string value)
        //{
        //}

        //// DELETE api/<ValuesController>/5
        //[HttpDelete("{id}")]
        //public void Delete(int id)
        //{
        //}
    }
}
