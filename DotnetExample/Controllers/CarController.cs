using DotnetExample.Contexto;
using DotnetExample.DTO;
using DotnetExample.Models;
using DotnetExample.Service;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Serilog;

namespace DotnetExample.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CarController : ControllerBase
    {
        private readonly CarService _service;

        public CarController(CarService service)
        {
            _service = service;
        }

        [HttpGet("{id}")]
        public CarResponse Get(Guid id)
        {
            Log.Information("[GET::CarController] Car request parameters {0}", id);
            CarResponse response = new CarResponse();
            try
            {
                response = _service.get(id);
                Log.Information("[GET::CarController] Car response {0}", JsonConvert.SerializeObject(response));
            }
            catch (Exception ex)
            {
                Log.Error("[GET::CarController] Internal Server Error, {0}", ex);
            }
            return response;
        }

        [HttpGet]
        public IEnumerable<CarResponse> Get()
        {
            IEnumerable<CarResponse> response = new List<CarResponse>();
            try
            {
                response = _service.list();
                Log.Information("[GET::CarController] Car list response {0}", JsonConvert.SerializeObject(response));
            }
            catch (Exception ex)
            {
                Log.Error("[GET::CarController] Internal Server Error - {0}", ex);
            }
            return response;
        }

        [HttpPost]
        public CarResponse Post(CarRequest payload)
        {
            Log.Information("[POST::CarController] Car request parameters {0}", JsonConvert.SerializeObject(payload));
            CarResponse response = new CarResponse();

            try
            {
                response = _service.insert(payload);
                Log.Information("[POST::CarController] Car insert response {0}", JsonConvert.SerializeObject(response));
            }
            catch (Exception ex)
            {
                Log.Error("[POST::CarController] Internal Server Error {0}", ex);
            }
            return response;
        }

        [HttpDelete("{id}")]
        public void Delete(Guid id)
        {
            Log.Information("[DELETE::CarController] Car request parameters {0}", id);

            try
            {
                _service.delete(id);
                Log.Information("[DELETE::CarController] Car deleted registry {0}", id);
            }
            catch (Exception ex)
            {
                Log.Error("[DELETE::CarController] Internal Server Error {0}", ex);
            }
        }

        [HttpPut("{id}")]
        public CarResponse Put(CarRequest payload, Guid id)
        {
            Log.Information("[POST::CarController] Car request parameters {0} {1}", id, JsonConvert.SerializeObject(payload));
            CarResponse response = new CarResponse();
            try
            {
                response = _service.update(payload, id);
                Log.Information("[POST::CarController] Car update registry {0}", JsonConvert.SerializeObject(response));
            }
            catch (Exception ex)
            {
                Log.Error("[POST::CarController] Internal Server Error {0}", ex);
            }
            return response;
        }
    }
}
