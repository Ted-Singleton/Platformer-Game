using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using PlayerUtils;

public class DatabaseManagerTests
{
    private int testPlayerID;

    [SetUp]
    public void SetUp()
    {
        testPlayerID = 0;
        //clean up from any previous tests
        DatabaseManager.WipePlayerTimes(testPlayerID);
    }

    [Test]
    public void ExecuteQuery_Success()
    {
        // Arrange
        string query = "INSERT INTO TestTable (Column1) VALUES ('TestData')";
        bool queryExecuted = false;

        // Act
        try
        {
            DatabaseManager.ExecuteQuery(query);
            queryExecuted = true;
        }
        catch (System.Exception)
        {
            // Catch any exceptions thrown by ExecuteQuery
            queryExecuted = false;
        }

        // Assert
        Assert.IsTrue(queryExecuted, "Query should execute successfully");
    }

    [Test]
    public void GetPlayerTime_Success()
    {
        // Arrange
        string levelName = "Level1-1";
        float levelTime = 10.0f;
        float timeRetrieved = 0.0f;

        // Act
        try
        {
            DatabaseManager.WriteTimeToDatabase(levelName, testPlayerID, levelTime);
            timeRetrieved = DatabaseManager.GetPlayerTime(levelName, testPlayerID.ToString()).levelTime;
        }
        catch (System.Exception)
        {
            // Catch any exceptions thrown by GetPlayerTime
            timeRetrieved = 0.0f;
        }

        // Assert
        Assert.AreEqual(levelTime, timeRetrieved, "Time should be retrieved successfully");
    }

    [Test]
    public void WriteTimeToDatabase_Success()
    {
        // Arrange
        string levelName = "Level1-1";
        int testPlayerID = 0;
        float levelTime = 10.0f;
        bool timeWritten = false;

        // Act
        try
        {
            DatabaseManager.WriteTimeToDatabase(levelName, testPlayerID, levelTime);
            timeWritten = true;
        }
        catch (System.Exception)
        {
            // Catch any exceptions thrown by WriteTimeToDatabase
            timeWritten = false;
        }

        // Assert
        Assert.IsTrue(timeWritten, "Time should be written to the database successfully");
    }

    [Test]
    public void WriteTimeToDatabase_InvalidLevel()
    {
        // Arrange
        string levelName = "InvalidLevel";
        int testPlayerID = 0;
        float levelTime = 10.0f;
        bool timeWritten = false;

        // Act
        try
        {
            DatabaseManager.WriteTimeToDatabase(levelName, testPlayerID, levelTime);
            timeWritten = true;
        }
        catch (System.Exception)
        {
            // Catch any exceptions thrown by WriteTimeToDatabase
            timeWritten = false;
        }

        // Assert
        Assert.IsFalse(timeWritten, "Time should not be written to the database for an invalid level");
    }

    [Test]
    public void WriteTimeToDatabase_NoChangeIfNotBetter()
    {
        //Arrange
        string levelName = "Level1-1";
        int testPlayerID = 0;
        float firstTime = 10f;
        float secondTime = 20f;

        float currentTime = 0f;
        float newTime = 0f;

        //Act
        try
        {
            DatabaseManager.WriteTimeToDatabase(levelName, testPlayerID, firstTime);
            currentTime = DatabaseManager.GetPlayerTime(levelName, testPlayerID.ToString()).levelTime;
            DatabaseManager.WriteTimeToDatabase(levelName, testPlayerID, secondTime);
            newTime = DatabaseManager.GetPlayerTime(levelName, testPlayerID.ToString()).levelTime;
        }
        catch (System.Exception)
        {
            Assert.Fail("An exception was thrown when it should not have been");
        }

        //Assert
        Assert.AreEqual(currentTime, newTime, "Time should not be updated if it is not better");
    }

    [Test]
    public void WriteTimeToDatabase_UpdateIfBetter()
    {
        //Arrange
        string levelName = "Level1-1";
        int testPlayerID = 0;
        float originalTime = 10f;
        float betterTime = 0.1f;

        float currentTime = 0f;
        float newTime = 0f;

        //Act
        try
        {
            DatabaseManager.WriteTimeToDatabase(levelName, testPlayerID, originalTime);
            currentTime = DatabaseManager.GetPlayerTime(levelName, testPlayerID.ToString()).levelTime;
            DatabaseManager.WriteTimeToDatabase(levelName, testPlayerID, betterTime);
            newTime = DatabaseManager.GetPlayerTime(levelName, testPlayerID.ToString()).levelTime;
        }
        catch (System.Exception)
        {
            Assert.Fail("An exception was thrown when it should not have been");
        }

        if(currentTime == 0 || newTime == 0)
        {
            Assert.Fail("Current time should not be 0");
        }

        //Assert
        Assert.AreNotEqual(currentTime, newTime, "Time should be updated if it is better");
    }

    [Test]
    public void GetTop100_Success()
    {
        // Arrange
        string levelName = "Level1-1";
        int testPlayerID = 0;
        float levelTime = 10.0f;
        List<PlayerTime> playerTimes = new List<PlayerTime>();

        // Act
        try
        {
            DatabaseManager.WriteTimeToDatabase(levelName, testPlayerID, levelTime);
            playerTimes = DatabaseManager.GetTop100(levelName);
        }
        catch (System.Exception)
        {
            // Catch any exceptions thrown by GetTop100
            playerTimes = new List<PlayerTime>();
        }

        // Assert
        Assert.IsTrue(playerTimes.Count > 0, "Player times should be retrieved successfully");
    }

    [Test]
    public void GetTop100_NoResults()
    {
        // Arrange
        string levelName = "Level1-1";
        List<PlayerTime> playerTimes = new List<PlayerTime>();

        // Act
        try
        {
            playerTimes = DatabaseManager.GetTop100(levelName);
        }
        catch (System.Exception)
        {
            // Catch any exceptions thrown by GetTop100
            playerTimes = new List<PlayerTime>();
        }

        // Assert
        Assert.IsTrue(playerTimes.Count == 0, "No player times should be retrieved");
    }

    [Test]
    public void GetTop100_Over100Results()
    {
        // Arrange
        string levelName = "Level1-1";
        float levelTime = 10.0f;
        List<PlayerTime> playerTimes = new List<PlayerTime>();

        // Act
        try
        {
            for (int i = 0; i < 110; i++)
            {
                //we execute a direct query to add a time to the database - this is a test, so we don't need to worry about having multiple of the same ID
                string query = $@"
                    INSERT INTO PlayerTimes (PlayerID, LevelID, CompletionTime)
                    VALUES ('{testPlayerID}', '{levelName}', '{levelTime}')";
                DatabaseManager.ExecuteQuery(query);
            }
            playerTimes = DatabaseManager.GetTop100(levelName);
        }
        catch (System.Exception)
        {
            // Catch any exceptions thrown by GetTop100
            playerTimes = new List<PlayerTime>();
        }

        // Assert
        Assert.IsTrue(playerTimes.Count == 100, "Only the top 100 player times should be retrieved");
    }

    [TearDown]
    public void TearDown()
    {
        //clean up from any previous tests
        DatabaseManager.WipePlayerTimes(testPlayerID);
    }
}
