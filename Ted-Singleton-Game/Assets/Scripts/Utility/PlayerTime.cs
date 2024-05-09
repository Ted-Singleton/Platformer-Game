using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlayerUtils
{
    public class PlayerTime
    {
        //this class is used to store the player's time for a level
        //these details will be filled in by the database manager for the leaderboard
        public string playerName { get; }
        public float levelTime { get; }

        public PlayerTime(string playerName, float levelTime)
        {
            this.playerName = playerName;
            this.levelTime = levelTime;
        }
    }
}