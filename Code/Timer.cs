public class Timer 
{
    private float currentTimePassed;
    private float triggerTime;   


    public float RecordTime(float deltaTime)
    {
        return currentTimePassed += deltaTime;
    }

    public void SetTimer(float waitTime)
    {
        this.triggerTime = waitTime;
        currentTimePassed = 0;
    }

    public bool IsDone()
    {
        var hasTriggerd = false;
        if( currentTimePassed > triggerTime )
        {
            currentTimePassed = 0;
            return hasTriggerd = true;
        }
        return hasTriggerd;
    }
}