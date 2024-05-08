using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using UnityEngine;
using PlayerUtils;

public class DatabaseManager : MonoBehaviour
{
    //used for running queries that don't return data
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
            connection.Close();
        }
    }

    public static void WriteTimeToDatabase(string levelName, int playerID, float levelTime)
    {
        //we use the database manager to save the level completion data to the database
        /* As a note, the intent here is to only save the player's time to the database, not whether they gathered the collectible
           the collectible is meant as a personal challenge for the player, and not a requirement to complete the level and compete with other players*/

        string connectionString = "Server=localhost,1433;Database=GameDB;User Id=SA;Password=Password1!;";

        //we'll need to run a query to check if the player has already completed the level, and what their currently saved time was
        string checkBestQuery = $"SELECT CompletionTime FROM PlayerTimes WHERE LevelID = '{levelName}' AND PlayerID = '{playerID}'";

        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            connection.Open();

            //check the current time for the level and player
            using (SqlCommand commandCheckTime = new SqlCommand(checkBestQuery, connection))
            {
                SqlDataReader reader = commandCheckTime.ExecuteReader();

                //if the query returns a result, we check if the new time is better than the current time
                if (reader.Read())
                {
                    //get the current time from the database
                    //while the data type in the database is a float, double is more compatible here, so we use that
                    double currentTime = reader.GetDouble(0);

                    //check if the new time is better than the current time
                    if (levelTime < currentTime || currentTime == 0)
                    {
                        //if the new time is better, update the time in the database
                        string queryUpdate = $"UPDATE PlayerTimes SET CompletionTime = '{levelTime}' WHERE LevelID = '{levelName}' AND PlayerID = '{playerID}'";
                        ExecuteQuery(queryUpdate);
                    }
                }
                else
                {
                    //if there is no time recorded, we insert the new time
                    string queryInsert = $"INSERT INTO PlayerTimes(LevelID, PlayerID, CompletionTime) VALUES ('{levelName}', '{playerID}', '{levelTime}')";
                    ExecuteQuery(queryInsert);
                }
            }
        }
    }

    public List<PlayerTime> GetTop100(string levelName)
    {
        //we want to run a query that returns the top 100 player times for a given level
        string connectionString = "Server=localhost,1433;Database=GameDB;User Id=SA;Password=Password1!;";
        //we'll use a premade query to get the top 100 times for a level, with the player's name and time, sorted by time
        //we just need to replace the level name in the query with the one we want
        string query = $@"
            SELECT TOP 100 Players.Username, PlayerTimes.CompletionTime
            FROM PlayerTimes
            JOIN Players ON PlayerTimes.PlayerID = Players.PlayerID
            WHERE PlayerTimes.LevelID = '{levelName}'
            ORDER BY PlayerTimes.CompletionTime ASC;
        ";
        //as a note, if we used a playerID system like Steam's, we would be able to display other player data here, like profile pictures, country flags, etc.
        //we could also filter the list to only show friends, or players from the same country, etc.

        //we create a list of the top 100 player times
        List<PlayerTime> top100 = new List<PlayerTime>();

        //we open a connection to the database and run the query
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            connection.Open();
            using(SqlCommand command = new SqlCommand(query, connection))
            {
                SqlDataReader reader = command.ExecuteReader();
                //we read the results of the query and add them to the list
                while(reader.Read())
                {
                    string playerName = reader.GetString(0);
                    float levelTime = (float)reader.GetDouble(1);
                    PlayerTime playerTime = new PlayerTime(playerName, levelTime);
                    top100.Add(playerTime);
                }
            }
        }
        return top100;
    }
}
