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
            EntitySqlController entitySqlController = new EntitySqlController();

            SqlConnection connection = new SqlConnection(EntitySqlController._connectionDatabaseString);
            connection.Open();

            SqlCommand command = connection.CreateCommand();
            command.Connection = connection;

            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "SelectUserList";

            List<User> userList = new();

            try
            {
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    //reader.Read() -> the loop will correctly iterate through each row in the SqlDataReader
                    /*
                        The reader.Read() method advances the SqlDataReader to the next record. Each time you call Read(),
                        it moves the reader to the next row, if there is one, and returns true. When there are no more rows,
                        it returns false, causing the while loop to terminate.
                        When you use reader.Read(), it moves the SqlDataReader to the next row in your result set, allowing you to access the data in that row
                    */
                    while (reader.Read())
                    {
                        User user = new User();

                        user.Id = reader.GetInt32(reader.GetOrdinal("Id"));
                        user.Guid = reader.GetGuid(reader.GetOrdinal("Guid"));
                        user.Name = reader.GetString(reader.GetOrdinal("Name"));
                        user.Description = reader.GetString(reader.GetOrdinal("Description"));
                        user.CreateDate = reader.GetDateTime(reader.GetOrdinal("CreateDate"));

                        /*
                        DBNull.Value è usato per rappresentare un valore NULL nei database. Tuttavia, 
                        non puoi assegnare direttamente DBNull.Value a una variabile di tipo DateTime in C#. 
                        Il tipo DateTime non accetta DBNull.Value o null perché è un tipo di valore (value type)
                        */

                        //Il metodo IsDBNull è utilizzato per verificare se il valore di una specifica colonna nel data reader è NULL
                        if (!reader.IsDBNull(reader.GetOrdinal("UpdateDate")))
                        {
                            user.UpdateDate = reader.GetDateTime(reader.GetOrdinal("UpdateDate"));
                        }

                        if (!reader.IsDBNull(reader.GetOrdinal("DeleteDate")))
                        {
                            user.DeleteDate = reader.GetDateTime(reader.GetOrdinal("DeleteDate"));
                        }

                        user.Email = reader.GetString(reader.GetOrdinal("Email"));
                        user.UserName = reader.GetString(reader.GetOrdinal("UserName"));

                        //The tinyint data type in SQL corresponds to a single byte (8-bit) value,
                        //and the GetByte method retrieves this value correctly.
                        user.Age = reader.GetByte(reader.GetOrdinal("Age"));

                        //user.password TODO
                        user.VersionId = reader.GetInt16(reader.GetOrdinal("VersionId"));

                        userList.Add(user);
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

            return userList;
        }
    }
}
