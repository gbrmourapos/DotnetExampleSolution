using DotnetExample.Contexto;
using DotnetExample.DTO;
using DotnetExample.Models;

namespace DotnetExample.Service
{
    public class CarService
    {
        private readonly CarContext _context;

        public CarService() 
        {
            _context = CarContext.GetInstance();
        } 

        public CarResponse get(Guid id)
        {
            var car = _context.get(id);
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

            _context.insert(model);

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
            var responses = _context.list().Select((element) =>
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
            _context.delete(id);    
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

            _context.update(id, model);
            
            var updatedModel = _context.get(id);
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
