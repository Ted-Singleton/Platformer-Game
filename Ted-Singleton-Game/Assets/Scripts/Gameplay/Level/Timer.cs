using UnityEngine;

public class Timer : MonoBehaviour
{
    private float startTime;
    private bool isRunning = false;
    public static float elapsedTime;

    private void Start()
    {
        StartTimer();
    }

    private void Update()
    {
        if (isRunning)
        {
            float elapsedTime = Time.time - startTime;
            Debug.Log("Elapsed Time: " + elapsedTime.ToString("F2") + " seconds");
        }
    }

    public void StartTimer()
    {
        startTime = Time.time;
        isRunning = true;
    }

    public void EndTimer()
    {
        isRunning = false;
        elapsedTime = Time.time - startTime;
        LevelData.time = elapsedTime;
        Debug.Log("Total Time: " + elapsedTime.ToString("F2") + " seconds");
    }

    private void OnDestroy()
    {
        EndTimer();
    }
}
