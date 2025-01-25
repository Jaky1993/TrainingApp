using Microsoft.Data.SqlClient;
using System.Data;
using TrainingAppData.DB.CONTROLLER;
using TrainingAppData.DB.INTERFACE;
using TrainingAppData.MODEL;

namespace TrainingAppData.DB.DBCONTROLLER.USER.Sql
{
    public class CreateSqlUser : ICreate<User>
    {
        public async Task Create(User entity)
        {
            EntitySqlController entitySqlController = new EntitySqlController();
            SqlConnection connection = new SqlConnection(EntitySqlController._connectionDatabaseString);
            connection.Open();

            SqlTransaction transaction = connection.BeginTransaction();
            
            SqlCommand command = connection.CreateCommand();
            command.Connection = connection;
            command.Transaction = transaction;

            try
            {
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "CreateUser";

                if(entity.UpdateDate != DateTime.MinValue)
                {
                    command.Parameters.AddWithValue("Id", entity.Id);
                }

                if (entity.UpdateDate != DateTime.MinValue)
                {
                    command.Parameters.AddWithValue("Guid", entity.Guid);
                }
                else
                {
                    command.Parameters.AddWithValue("Guid", Guid.NewGuid());
                }

                command.Parameters.AddWithValue("Name", entity.Name);
                command.Parameters.AddWithValue("Description", entity.Description);

                if (!(entity.UpdateDate == DateTime.MinValue))
                {
                    command.Parameters.AddWithValue("UpdateDate", entity.UpdateDate);
                }
                else
                {
                    //DbNull.Value is a special singleton object in .NET used to represent
                    //a missing or nonexistent value in database fields
                    command.Parameters.AddWithValue("UpdateDate", DBNull.Value);
                }

                if(!(entity.DeleteDate == DateTime.MinValue))
                {
                    command.Parameters.AddWithValue("DeleteDate", entity.DeleteDate);
                }
                else
                {
                    command.Parameters.AddWithValue("DeleteDate", DBNull.Value);
                }
                
                command.Parameters.AddWithValue("Email", entity.Email);
                command.Parameters.AddWithValue("UserName", entity.UserName);
                command.Parameters.AddWithValue("Age", entity.Age);
                command.Parameters.AddWithValue("Password", "testPassword");
                command.Parameters.AddWithValue("VersionId", entity.VersionId);

                await command.ExecuteNonQueryAsync();

                transaction.Commit();
            }
            catch (Exception ex)
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
