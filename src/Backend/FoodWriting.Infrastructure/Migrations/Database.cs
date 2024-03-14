using Dapper;
using MySqlConnector;

namespace FoodWriting.Infrastructure.Migrations;

public static class Database
{
    public static void CreateDatabase(string connectionString, string database)
    {
        using var myConnection = new MySqlConnection(connectionString);
        
        var parameteres = new DynamicParameters();
        parameteres.Add("name", database);
        
        var registers =  myConnection.Query("SELECT * FROM INFORMATION_SCHEMA.SCHEMATA WHERE SCHEMA_NAME = @name", parameteres);

        if(!registers.Any())
        {
            myConnection.Execute($"CREATE DATABASE {database}");
        }
    }
}
