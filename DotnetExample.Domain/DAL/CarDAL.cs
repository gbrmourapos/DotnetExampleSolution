using DotnetExample.Contexto;
using DotnetExample.Domain.Models;
using MySql.Data.MySqlClient;
using System.Text;

namespace DotnetExample.Domain.DAL
{
    public class CarDAL
    {
        private DotnetExampleContext _context;

        public CarDAL(DotnetExampleContext context)
        {
            _context = context;
        }

        public void Insert(CarModel value)
        {
            try
            {
                string query = "INSERT INTO Car";
                query += "(Id, Name, Horsepower, Fuel, Model, TankVolume)";
                query += "VALUES";
                query += "(@Id, @Name, @Horsepower, @Fuel, @Model, @TankVolume)";

                if (_context.OpenConnection())
                {
                    MySqlCommand cmd = new MySqlCommand();
                    cmd.CommandText = query;
                    cmd.Connection = _context.GetConnection();

                    cmd.Parameters.AddWithValue("@Id", Guid.NewGuid().ToString());
                    cmd.Parameters.AddWithValue("@Name", value.Name);
                    cmd.Parameters.AddWithValue("@Horsepower", value.Horsepower);
                    cmd.Parameters.AddWithValue("@Fuel", value.Fuel);
                    cmd.Parameters.AddWithValue("@Model", value.Model);
                    cmd.Parameters.AddWithValue("@TankVolume", value.TankVolume);

                    cmd.ExecuteNonQuery();
                }
            }
            finally
            {
                _context.CloseConnection();
            }
        }

        public CarModel Select(Guid id)
        {
            try
            {
                CarModel? car = null;
                string query = $"SELECT * FROM Car WHERE id=@Id";

                if (_context.OpenConnection())
                {
                    MySqlCommand cmd = new MySqlCommand();
                    cmd.CommandText = query;
                    cmd.Connection = _context.GetConnection();
                    cmd.Parameters.AddWithValue("@Id", id.ToString());
                    cmd.ExecuteNonQuery();

                    MySqlDataReader reader = cmd.ExecuteReader();

                    if (!reader.Read())
                    {
                        _context.CloseConnection();
                        return null;
                    }

                    car = new CarModel
                    {
                        Id = reader.IsDBNull(0) ? Guid.Empty : Guid.Parse(reader.GetString(0)),
                        Name = reader.IsDBNull(1) ? "" : reader.GetString(1),
                        Model = reader.IsDBNull(2) ? "" : reader.GetString(2),
                        Horsepower = reader.IsDBNull(3) ? "" : reader.GetString(3),
                        Fuel = reader.IsDBNull(4) ? "" : reader.GetString(4),
                        TankVolume = reader.IsDBNull(5) ? "" : reader.GetString(5)
                    };
                }

                return car;
            }
            finally
            {
                _context.CloseConnection();
            }
        }

        public List<CarModel> Select()
        {
            try
            {
                List<CarModel> cars = new List<CarModel>();
                string query = $"SELECT * FROM Car";

                if (_context.OpenConnection())
                {
                    MySqlCommand cmd = new MySqlCommand();
                    cmd.CommandText = query;
                    cmd.Connection = _context.GetConnection();
                    cmd.ExecuteNonQuery();


                    MySqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        cars.Add(new CarModel
                        {
                            Id = reader.IsDBNull(0) ? Guid.Empty : Guid.Parse(reader.GetString(0)),
                            Name = reader.IsDBNull(1) ? "" : reader.GetString(1),
                            Model = reader.IsDBNull(2) ? "" : reader.GetString(2),
                            Horsepower = reader.IsDBNull(3) ? "" : reader.GetString(3),
                            Fuel = reader.IsDBNull(4) ? "" : reader.GetString(4),
                            TankVolume = reader.IsDBNull(5) ? "" : reader.GetString(5)
                        });
                    }
                }

                return cars;
            }
            finally
            {
                _context.CloseConnection();
            }
        }

        public void Update(Guid id, CarModel value)
        {
            try
            {
                string query = "UPDATE Car SET ";
                query += $"Name=@Name, Fuel=@Fuel, Model=@Model, Horsepower=@Horsepower, TankVolume=@TankVolume ";
                query += $"WHERE id=@Id";

                if (_context.OpenConnection())
                {
                    MySqlCommand cmd = new MySqlCommand();
                    cmd.CommandText = query;
                    cmd.Connection = _context.GetConnection();

                    cmd.Parameters.AddWithValue("@Name", value.Name);
                    cmd.Parameters.AddWithValue("@Fuel", value.Fuel);
                    cmd.Parameters.AddWithValue("@Model", value.Model);
                    cmd.Parameters.AddWithValue("@Horsepower", value.Horsepower);
                    cmd.Parameters.AddWithValue("@TankVolume", value.TankVolume);
                    cmd.Parameters.AddWithValue("@Id", id.ToString());

                    cmd.ExecuteNonQuery();
                }
            }
            finally
            {
                _context.CloseConnection();
            }
        }

        public void Delete(Guid id)
        {
            try
            {
                string query = $"DELETE FROM Car WHERE id=@Id";

                if (_context.OpenConnection())
                {
                    MySqlCommand cmd = new MySqlCommand();
                    cmd.CommandText = query;
                    cmd.Connection = _context.GetConnection();

                    cmd.Parameters.AddWithValue("@Id", id.ToString());

                    cmd.ExecuteNonQuery();
                }
            }
            finally
            {
                _context.CloseConnection();
            }
        }
    }
}
