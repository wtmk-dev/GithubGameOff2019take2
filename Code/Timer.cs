using UnityEngine;

public class Timer : ScriptableObject
{
    public Event e;
    public float lengthInSeconds;

    private float lastTimeStep;   

    public void ResetTimer()
    {
        lastTimeStep = 0.0f;
    }

    public void StartTimer()
    {
        if(Time.time - lastTimeStep >= lengthInSeconds)
        {
            lastTimeStep = Time.time;

            if(e.OnRaiseEvent != null)
            {
                e.Raise();
            }
        }
    }
}