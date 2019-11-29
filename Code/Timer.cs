public class Timer 
{
    private float currentTimePassed;
    private float triggerTime;
    private bool isLocked;   


    public float RecordTime(float deltaTime)
    {
        return currentTimePassed += deltaTime;
    }

    public void SetTimer(float waitTime)
    {
        triggerTime = waitTime;
        currentTimePassed = 0;
    }

    public void SetLock(bool isLocked)
    {
        this.isLocked = isLocked;
    }

    public bool IsLocked()
    {
        return isLocked;
    }

    public bool IsDone()
    {
        var hasTriggerd = false;
        if( currentTimePassed > triggerTime )
        {
            return hasTriggerd = true;
        }
        return hasTriggerd;
    }
}