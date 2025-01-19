using Microsoft.Data.SqlClient;
namespace TrainingAppData.DB.Interface
{
    public interface IDatabase
    {
        void CreateDatabase(string databaseName);
    }
}
