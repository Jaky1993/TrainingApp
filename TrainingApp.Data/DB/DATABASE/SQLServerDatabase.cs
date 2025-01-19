using Microsoft.Data.SqlClient;
using TrainingAppData.DB.CONTROLLER;
using TrainingAppData.DB.Interface;

namespace TrainingAppData.DB.SQLDatabase
{
    public class SQLServerDatabase : IDatabase
    {
        public void CreateDatabase(string databaseName)
        {
            EntitySqlController entitySqlController = new EntitySqlController();

            if (!EntitySqlController.IsDatabaseExist(EntitySqlController._databaseName))
            {
                SqlConnection connection = new SqlConnection(EntitySqlController._connectionSqlServerCreateDB);
                connection.Open();

                SqlCommand sqlCommand = connection.CreateCommand();

                sqlCommand.Connection = connection;

                string query = $"CREATE DATABASE {databaseName}";

                sqlCommand.CommandText = query;

                try
                {
                       /*
                       ExecuteNonQuery():
                       Executes a Transact-SQL statement against the connection and returns the number of rows affected.
                       Used for INSERT, UPDATE, DELETE, and DDL (CREATE TABLE, ALTER, DROP, TRUNCATE) statements.
                       WHY use ExecuteNonQuery to create database
                       Using ExecuteNonQuery is appropriate for executing SQL statements that do not return result sets.
                       This includes Data Definition Language (DDL) statements, which are used to create, alter, or drop database objects.
                       When creating a database, you're essentially issuing a DDL statement.

                       ExecuteReader():
                       Executes the CommandText against the Connection and returns a SqlDataReader.
                       Used for SELECT queries that return rows.

                       ExecuteScalar():
                       Executes the query and returns the first column of the first row in the result set.
                       Used for queries that return a single value.
                       */

                    sqlCommand.ExecuteNonQuery();              

                    Console.WriteLine("DataBase is Created Successfully");
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                finally
                {
                    connection.Close();
                    sqlCommand.Dispose();
                }
            }
        }
    }
}
