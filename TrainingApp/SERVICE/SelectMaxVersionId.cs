using Microsoft.Data.SqlClient;
using System.Data;
using TrainingAppData.DB.CONTROLLER;

namespace TrainingApp.SERVICE
{
    public class SelectMaxVersionId
    {
        public static int Select(int userId)
        {
            int maxVersionId = 0;

            EntitySqlController entitySqlController = new EntitySqlController();

            SqlConnection connection = new SqlConnection(EntitySqlController._connectionDatabaseString);

            connection.Open();

            SqlCommand command = connection.CreateCommand();
            command.Connection = connection;
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "SelectMaxVersionId";
            command.Parameters.AddWithValue("@UserId", userId);

            try
            {
                SqlDataReader reader = command.ExecuteReader();

                maxVersionId = reader.GetInt32(0);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                connection.Close();
            }
            

            return maxVersionId;
        }
    }
}
