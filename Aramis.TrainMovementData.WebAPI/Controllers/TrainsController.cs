using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Aramis.TrainMovementData.Data;
using Aramis.TrainMovementData.WebAPI.Data.Interfaces;
using Aramis.TrainMovementData.WebAPI.Data.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Aramis.TrainMovementData.WebAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class TrainsController : ControllerBase
    {
        private readonly IVehicleRepository vehicleRepository;
        private readonly IBasicDataRepository basicDataRepository;
        private readonly INotificationRepository notificationRepository;

        public TrainsController(IVehicleRepository vehicleRepository,
            IBasicDataRepository basicDataRepository,
            INotificationRepository notificationRepository)
        {
            this.vehicleRepository = vehicleRepository;
            this.basicDataRepository = basicDataRepository;
            this.notificationRepository = notificationRepository;
        }

        [HttpGet("vehicle/trainnumber/{trainnumber}/date/{date}")]
        public async Task<List<Vehicle>> GetVehiclesAsync(string trainnumber, DateTime date)
        {
            return await vehicleRepository.GetVehiclesAsync(trainnumber, date);
        }

        [HttpGet("basicdata/trainnumber/{trainnumber}/date/{date}")]
        public async Task<BasicData> GetBasicDataAsync(string trainnumber, DateTime date)
        {
            return await basicDataRepository.GetBasicDataAsync(trainnumber, date);
        }

        [HttpGet("notification/trainnumber/{trainnumber}/date/{date}")]
        public async Task<List<Notification>> GetNotification(string trainnumber, DateTime date)
        {
            return await notificationRepository.GetNotificationsAsync(trainnumber, date);
        }

        [HttpGet("traincategory/{traincategory}")]
        public async Task<List<string>> GetTrainCategory(string traincategory)
        {
            return await basicDataRepository.GetTrainCategoryAsync(traincategory);
        }

        [HttpGet("orderer/{orderer}")]
        public async Task<List<string>> GetOrderer(string orderer)
        {
            return await basicDataRepository.GetOrdererAsync(orderer);
        }

        [HttpGet("operator/{operatorString}")]
        public async Task<List<string>> GetOperator(string operatorString)
        {
            return await basicDataRepository.GetOperatorAsync(operatorString);
        }

        [HttpGet("tractionprovider/{tractionprovider}")]
        public async Task<List<string>> GetTractionProvider(string tractionprovider)
        {
            return await basicDataRepository.GetTractionProviderAsync(tractionprovider);
        }
    }
}
