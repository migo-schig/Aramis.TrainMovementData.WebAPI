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

        [HttpGet("vehicles/{trainnumber}/date/{date}")]
        public IEnumerable<Vehicle> GetVehicles(string trainnumber, DateTime date)
        {
            return vehicleRepository.GetVehicles(trainnumber, date);
        }

        [HttpGet("basicdata/{trainnumber}/date/{date}")]
        public IEnumerable<Notification> GetNotification(string trainnumber, DateTime date)
        {
            return notificationRepository.GetNotifications(trainnumber, date);
        }

        [HttpGet("traincategory/{traincategory}")]
        public IEnumerable<string> GetTrainCategory(string traincategory)
        {
            return basicDataRepository.GetTrainCategory(traincategory);
        }

        [HttpGet("orderer/{orderer}")]
        public IEnumerable<string> GetOrderer(string orderer)
        {
            return basicDataRepository.GetOrderer(orderer);
        }

        [HttpGet("operator/{operatorString}")]
        public IEnumerable<string> GetOperator(string operatorString)
        {
            return basicDataRepository.GetOperator(operatorString);
        }

        [HttpGet("tractionprovider/{tractionprovider}")]
        public IEnumerable<string> GetTractionProvider(string tractionprovider)
        {
            return basicDataRepository.GetTractionProvider(tractionprovider);
        }
        
        
    }
}
