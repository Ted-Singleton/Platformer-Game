using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelData : MonoBehaviour
{
    //this script is used to transfer data between a level scene and the level complete scene
    //first, we need a string to hold the name of the level
    public static string levelName;

    //we also need a float to hold the time taken to complete the level
    public static float time;
}
