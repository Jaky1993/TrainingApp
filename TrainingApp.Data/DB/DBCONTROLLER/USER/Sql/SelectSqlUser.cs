using Microsoft.Data.SqlClient;
using System.Data;
using TrainingAppData.DB.CONTROLLER;
using TrainingAppData.DB.INTERFACE;
using TrainingAppData.MODEL;

namespace TrainingAppData.DB.DBCONTROLLER.USER.Sql
{
    public class SelectSqlUser : ISelect<User>
    {
        public User Select(Guid guid)
        {
            User user = new User();

            EntitySqlController entitySqlController = new EntitySqlController();

            SqlConnection connection = new SqlConnection(EntitySqlController._connectionDatabaseString);
            connection.Open();

            SqlCommand command = connection.CreateCommand();

            command.Connection = connection;
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "SelectUserByGuid";

            command.Parameters.AddWithValue("@Guid", guid);

            try
            {
                //Execute the command and process the results
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        user.Id = reader.GetInt32(reader.GetOrdinal("Id"));
                        user.Guid = reader.GetGuid(reader.GetOrdinal("Guid"));
                        user.Name = reader.GetString(reader.GetOrdinal("Name"));
                        user.Description = reader.GetString(reader.GetOrdinal("Description"));
                        user.CreateDate = reader.GetDateTime(reader.GetOrdinal("CreateDate"));
                        user.UpdateDate = reader.GetDateTime(reader.GetOrdinal("UpdateDate"));
                        user.DeleteDate = reader.GetDateTime(reader.GetOrdinal("DeleteDate"));
                        user.Email = reader.GetString(reader.GetOrdinal("Email"));
                        user.UserName = reader.GetString(reader.GetOrdinal("UserName"));
                        user.Age = reader.GetInt16(reader.GetOrdinal("Age"));
                        //user.password TODO
                        user.VersionId = reader.GetInt32(reader.GetOrdinal("VersionId"));
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                connection.Close();
            }

            return user;
        }

        public User Select(int id)
        {
            User user = new User();

            return user;
        }

        public List<User> SelectList()
        {
            List<User> userList = new();

            return userList;
        }
    }
}
