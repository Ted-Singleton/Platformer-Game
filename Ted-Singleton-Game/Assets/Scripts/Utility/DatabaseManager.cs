using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using UnityEngine;

public class DatabaseManager : MonoBehaviour
{
    public static void ExecuteQuery(string query)
    {
        string connectionString = "Server=localhost,1433;Database=GameDB;User Id=SA;Password=Password1!;";
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            connection.Open();
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.ExecuteNonQuery();
            }
        }
    }

    public static int GetLevelID(string levelName)
    {
        string connectionString = "Server=localhost,1433;Database=GameDB;User Id=SA;Password=Password1!;";
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            connection.Open();
            string query = $"SELECT LevelID FROM Levels WHERE DisplayName = '{levelName}'";
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return reader.GetInt32(0);
                    }
                }
            }
        }
        return -1;
    }
}
