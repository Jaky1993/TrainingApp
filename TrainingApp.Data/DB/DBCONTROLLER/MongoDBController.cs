using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using TrainingAppData.DB.INTERFACE;

namespace TrainingAppData.DB.CONTROLLER
{
    public class MongoDBController : DatabaseController, IDatabaseController
    {
        public bool IsDatabaseExist(SqlConnection connection, string databaseName)
        {
            throw new NotImplementedException();
        }
    }
}
