using Microsoft.Data.SqlClient;
using System.Data;
using TrainingAppData.DB.CONTROLLER;
using TrainingAppData.DB.INTERFACE;
using TrainingAppData.MODEL;

namespace TrainingAppData.DB.DBCONTROLLER.USER.Sql
{
    public class DeleteSqlUser : IDelete<User>
    {
        public void Delete(Guid guid)
        {
            throw new NotImplementedException();
        }

        public void Delete(int id, DateTime deleteDate)
        {
            EntitySqlController entitySqlController = new EntitySqlController();

            SqlConnection connection = new SqlConnection(EntitySqlController._connectionDatabaseString);

            connection.Open();

            SqlTransaction transaction = connection.BeginTransaction();

            SqlCommand command = connection.CreateCommand();
            command.Connection = connection;
            command.Transaction = transaction;

            command.CommandType = CommandType.StoredProcedure;

            command.CommandText = "DeleteUser";
            command.Parameters.AddWithValue("@Id", id);
            command.Parameters.AddWithValue("@DeleteDate", deleteDate);

            try
            {
                //Esegue la query e non ritorna niente
                command.ExecuteNonQuery();

                transaction.Commit();
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);

                transaction.Rollback();
            }
            finally
            {
                connection.Close();
                transaction.Dispose();
            }
        }
    }
}
