using DotnetExample.Domain.Models;
using MySql.Data.MySqlClient;

namespace DotnetExample.Contexto
{
    public class DotnetExampleContext
    {
        private MySqlConnection _connection;
        private string _server = "localhost";
        private string _database = "DotnetExample";
        private string _user = "root";
        private string _password = "root";

        public DotnetExampleContext()
        {
            _connection = new MySqlConnection($"SERVER={_server}; DATABASE={_database}; UID={_user}; PASSWORD={_password};");
        }

        public MySqlConnection GetConnection()
        {
            return _connection;
        }

        public bool OpenConnection()
        {
            try
            {
                _connection.Open();
                return true;
            }
            catch (MySqlException ex)
            {
                Console.WriteLine(ex.ToString());
                return false;
            }
        }

        public bool CloseConnection()
        {
            try
            {
                _connection.Close();
                return true;
            }
            catch (MySqlException ex)
            {
                Console.WriteLine(ex.ToString());
                return false;
            }
        }
    }
}
