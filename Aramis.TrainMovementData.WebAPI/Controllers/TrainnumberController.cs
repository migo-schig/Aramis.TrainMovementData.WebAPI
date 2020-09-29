using Aramis.TrainMovementData.WebAPI.Data.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

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
        public async Task<List<string>> GetLikeAsync(string trainnumber, DateTime dateFrom, DateTime dateTo)
        {
            return await trainnumberRepository.GetLikeAsync(trainnumber, dateFrom, dateTo);
        }

        [HttpGet]
        public async Task<List<string>> GetAsync(string stationShort, DateTime dateFrom, DateTime dateTo)
        {
            return await trainnumberRepository.GetAsync(stationShort, dateFrom, dateTo);
        }
    }
}
