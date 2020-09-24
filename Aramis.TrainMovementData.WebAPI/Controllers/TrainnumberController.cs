using Aramis.TrainMovementData.WebAPI.Data.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace Aramis.TrainMovementData.WebAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class TrainnumberController : ControllerBase
    {
        private readonly ITrainnumberRepository trainnumberRepository;
        public TrainnumberController(ITrainnumberRepository trainnumberRepository)
        {
            this.trainnumberRepository = trainnumberRepository;
        }

        [HttpGet("{trainnumber}/from/{dateFrom}/to/{dateTo}")]
        public IEnumerable<string> GetLike(string trainnumber, DateTime dateFrom, DateTime dateTo)
        {
            return trainnumberRepository.GetLike(trainnumber, dateFrom, dateTo);
        }

        // GET: api/<TrainnumberController>
        [HttpGet]
        public IEnumerable<string> Get(string stationShort, DateTime dateFrom, DateTime dateTo)
        {
            return trainnumberRepository.Get(stationShort, dateFrom, dateTo);
        }

        // GET api/<TrainnumberController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }
    }
}
