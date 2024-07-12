using DotnetExample.Contexto;
using DotnetExample.Domain.DAL;
using DotnetExample.Domain.DTO;
using DotnetExample.Domain.Models;

namespace DotnetExample.Service
{
    public class CarService
    {
        private readonly CarDAL _dal;

        public CarService(DotnetExampleContext context) 
        {
            _dal = new CarDAL(context);
        } 

        public CarResponse get(Guid id)
        {
            var car = _dal.Select(id);
            var response = new CarResponse()
            {
                Id = car.Id,
                Fuel = car.Fuel,
                Horsepower = car.Horsepower,
                Model = car.Model,
                Name = car.Name,
                TankVolume = car.TankVolume
            };

            return response;
        }

        public CarResponse insert(CarRequest payload)
        {
            var model = new CarModel()
            {
                Id = Guid.NewGuid(),
                Fuel = payload.Fuel,
                Horsepower = payload.Horsepower,
                Model = payload.Model,
                Name = payload.Name,
                TankVolume = payload.TankVolume
            };

            _dal.Insert(model);

            var response = new CarResponse()
            {
                Id = model.Id,
                Fuel = model.Fuel,
                Model = model.Model,
                Name = model.Name,
                TankVolume = model.TankVolume
            };

            return response;
        }

        public List<CarResponse> list()
        {
            var responses = _dal.Select().Select((element) =>
            {
                var respone = new CarResponse()
                {
                    Id = element.Id,
                    Model = element.Model,
                    Name = element.Name,
                    TankVolume = element.TankVolume,
                    Horsepower = element.Horsepower,
                    Fuel = element.Fuel,
                };

                return respone;
            }).ToList();

            return responses;
        }

        public void delete(Guid id)
        {
            _dal.Delete(id);    
        }

        public CarResponse update(CarRequest payload, Guid id)
        {
            var model = new CarModel()
            {
                Fuel = payload.Fuel,
                Horsepower = payload.Horsepower,
                Model = payload.Model,
                Name = payload.Name,
                TankVolume = payload.TankVolume
            };

            _dal.Update(id, model);
            
            var updatedModel = _dal.Select(id);
            var response = new CarResponse()
            {
                Id = updatedModel.Id,
                Fuel = updatedModel.Fuel,
                Model = updatedModel.Model,
                Name = updatedModel.Name,
                TankVolume = updatedModel.TankVolume,
                Horsepower = updatedModel.Horsepower,
            };

            return response;
        }
    }
}
