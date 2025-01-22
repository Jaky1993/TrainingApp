using Microsoft.Data.SqlClient;
using System.Data;
using System.IO;
using System.Transactions;
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

        public bool IsTableAlreadyExist(string tableName, string schema)
        {
            SqlConnection connection = new SqlConnection(EntitySqlController._connectionDatabaseString);
            connection.Open();

            SqlCommand command = connection.CreateCommand();
            command.Connection = connection;

            string query = @"SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_SCHEMA = '@Schema' AND  TABLE_NAME = '@TableName'";

            command.CommandText = query;
            command.Parameters.AddWithValue("@Schema", schema);
            command.Parameters.AddWithValue("@tableName", tableName);

            using (SqlDataReader reader = command.ExecuteReader())
            {
                return reader.HasRows;
            }
        }

        public bool IsAlreadyExistStoreProcedure(string storeProcedureName)
        {
            bool result = false;

            EntitySqlController entitySqlController = new EntitySqlController();

            SqlConnection connection = new SqlConnection(EntitySqlController._connectionDatabaseString);
            connection.Open();

            SqlCommand command = connection.CreateCommand();
            command.Connection = connection;

            command.CommandText = $"SELECT OBJECT_ID('@storeProcedureName', 'P')";
            command.Parameters.AddWithValue("@storeProcedureName", storeProcedureName);

            using (SqlDataReader reader = command.ExecuteReader())
            {
                /*
                Yes, reader.IsDBNull(0) is a method that checks whether the column at the specified ordinal 
                (in this case, the first column) contains a database null (DBNull) value.
                It returns true if the column contains a null value, otherwise it returns false.   
                */
                if (reader.Read() && !reader.IsDBNull(0))
                {
                    result = true;
                }
            }

            return result;
        }
        public void GenerateTableFromFile(string path, string tableName, string schema)
        {
            if (!IsTableAlreadyExist(tableName, schema))
            {
                SqlConnection connection = new SqlConnection(EntitySqlController._connectionDatabaseString);
                connection.Open();

                SqlTransaction transaction = connection.BeginTransaction();

                try
                {
                    string query = File.ReadAllText(path);

                    //That's correct! By splitting the SQL script into batches using the GO delimiter
                    //StringSplitOptions.RemoveEmptyEntries: the resulting array will contain only non-empty strings
                    List<string> batchList = query.Split("GO", StringSplitOptions.RemoveEmptyEntries).ToList();

                    foreach (string batch in batchList)
                    {
                        using (SqlCommand command = new SqlCommand(batch, connection, transaction))
                        {
                            command.ExecuteNonQuery();
                        }
                    }

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

        public void AddSelectMaxVersionId(string path)
        {
            if (!IsAlreadyExistStoreProcedure("SelectMaxVersionId"))
            {
                EntitySqlController entitySqlController = new EntitySqlController();

                SqlConnection connection = new SqlConnection(EntitySqlController._connectionDatabaseString);
                connection.Open();

                string query = File.ReadAllText(path);

                List<string> batchList = query.Split("GO", StringSplitOptions.RemoveEmptyEntries).ToList();

                try
                {
                    foreach (string batch in batchList)
                    {
                        using (SqlCommand command = new SqlCommand(batch, connection))
                        {
                            command.ExecuteNonQuery();
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
            }
        }
    }
}
