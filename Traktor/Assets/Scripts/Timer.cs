
using System;

public class Timer
{
    public float RemainingSeconds { get; private set; }
    
    public Timer(float duration)
    {
        RemainingSeconds = duration;
    }

    public event Action onTimerEnd;

    public void Tick(float deltaTime)
    {
        if (RemainingSeconds != 0)
        {
            RemainingSeconds -= deltaTime;
        }

        CheckForTimerEnd();

    }

    private void CheckForTimerEnd()
    {
        if (RemainingSeconds > 0) { return;  }

        RemainingSeconds = 0;
        
        onTimerEnd?.Invoke();
    }
}
