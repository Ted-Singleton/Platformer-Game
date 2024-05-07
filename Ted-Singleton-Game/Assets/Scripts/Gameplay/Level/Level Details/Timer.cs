using UnityEngine;

public class Timer : MonoBehaviour
{
    private float startTime;
    private bool isRunning = false;
    public static float elapsedTime;

    private void Start()
    {
        //when the script loads, we begin the timer
        StartTimer();
    }

    private void Update()
    {
        //if the timer is running, update its elapsed time
        if (isRunning)
        {
            elapsedTime = Time.time - startTime;
        }
    }

    public void StartTimer()
    {
        //we set the start time to the current time, using Time.time
        startTime = Time.time;
        //and set the isRunning flag to true
        isRunning = true;
    }

    public void EndTimer()
    {
        //when the timer ends, we set the isRunning flag to false
        isRunning = false;
        //and perfom one last update to get the final elapsed time
        elapsedTime = Time.time - startTime;
        //which we pass to LevelData, so it can be used in the level complete scene
        LevelData.time = elapsedTime;
    }

    private void OnDestroy()
    {
        //when the object is destroyed, we end the timer
        //this will be called when the level is completed, and we move onto a new scene
        EndTimer();
    }

    //this method will be used by the HUD to get the current elapsed time
    public float GetElapsedTime()
    {
        return elapsedTime;
    }
}
