using DotnetExample.Models;

namespace DotnetExample.Contexto
{
    public sealed class CarContext
    {
        private CarContext() 
        {
            _context.AddRange([
            new CarModel() 
            {
                Id = Guid.NewGuid(),
                Fuel = "Gasolina",
                Model = "Classe C Sedan",
                Name = "C 180",
                Horsepower = "204cv",
                TankVolume = "47l"
            }, new CarModel()
            {
                Id = Guid.NewGuid(),
                Fuel = "Gasolina/Etanol",
                Model = "Spirit",
                Name = "Celta",
                Horsepower = "78cv",
                TankVolume = "45l"
            }, new CarModel()
            {
                Id = Guid.NewGuid(),
                Fuel = "Gasolina/Etanol",
                Model = "Strada",
                Name = "Fiat Strada",
                Horsepower = "107cv",
                TankVolume = "50l"
            }]);

        }

        private static CarContext _instance;
        private static List<CarModel> _context = new List<CarModel>();

        public static CarContext GetInstance()
        {
            if (_instance == null)
            {
                _instance = new CarContext();
            }

            return _instance;
        }

        public CarModel get(Guid id)
        {
            var model = _context.FirstOrDefault(x => x.Id == id);

            if (model == null)
            {
                return new CarModel();
            }

            return model;
        }

        public void insert(CarModel payload)
        {
            _context.Add(payload);
        }

        public List<CarModel> list()
        {
            return _context.ToList();
        }

        public void delete(Guid id)
        {
            var model = _context.FirstOrDefault(x => x.Id == id);

            if (model != null)
            {
                _context.Remove(model);
            }
        }

        public void update(Guid id, CarModel payload)
        {
            _context.Where(element => element.Id == id).ToList()
                    .ForEach((element) =>
                    {
                        element.Fuel = payload.Fuel;
                        element.Horsepower = payload.Horsepower;
                        element.TankVolume = payload.TankVolume;
                        element.Name = payload.Name;
                        element.Model = payload.Model;
                    });
        }
    }
}
