using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using TrainingAppData.DB.INTERFACE;

namespace TrainingAppData.DB.CONTROLLER
{
    public class EntitySqlController : DatabaseController, IDatabaseController
    {
        public static string _connectionDatabaseString;
        public static string _connectionSqlServerCreateDB;
        public static string _databaseName;

        public EntitySqlController()
        {
            IConfiguration configuration = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true) // Add a JSON configuration file
            .Build(); // Build the configuration

            _connectionDatabaseString = configuration.GetValue<string>("ConnectionStrings:SQLDatabaseConnection");
            _connectionSqlServerCreateDB = configuration.GetValue<string>("ConnectionStrings:SQLServerConnectionCreateDB");
            _databaseName = configuration.GetValue<string>("DatabaseName:SqlServerDbName");
        }

        public static bool IsDatabaseExist(string databaseName)
        {
            bool result;

            try
            {
                SqlConnection connection = new SqlConnection(_connectionDatabaseString);
                connection.Open();

                SqlCommand sqlCommand = connection.CreateCommand();
                sqlCommand.Connection = connection;

                string query = ($"SELECT db_id('{databaseName}')");

                SqlCommand command = new SqlCommand(query, connection);

                result = command.ExecuteScalar() != DBNull.Value;
                /*
                command.ExecuteScalar() executes the query and returns the first column of the first row in the result set.
                DBNull.Value represents a nonexistent value (similar to NULL in the database)

                If the result is not NULL, the comparison returns true, indicating that the database or record exists.
                If the result is NULL, the comparison returns false, indicating that the database or record does not exist.
                */
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                result = false;
            }

            return result;
        }
    }
}
