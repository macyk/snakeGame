using UnityEngine;
using UnityEngine.Events;

public class TimedEvent : UnityEvent { }

/// <summary>
/// manages global time, when to move snakes
/// </summary>
public class TimeManager : MonoBehaviour
{
    public static TimedEvent OnTimesUp = new TimedEvent();
    /// <summary>
    /// how many seconds do we wait
    /// </summary>
    float   _duration = 0.5f;
    float   _timeStamp;
    bool    _paused;

    // Start is called before the first frame update
    void Start()
    {
        _timeStamp = Time.realtimeSinceStartup;
    }

    // Update is called once per frame
    void Update()
    {
        if(_paused)
        {
            return;
        }
        float now = Time.realtimeSinceStartup;
        if(now - _timeStamp > _duration)
        {
            _timeStamp = Time.realtimeSinceStartup;
            if(OnTimesUp != null)
            {
                OnTimesUp.Invoke();
            }
        }
    }

    /// <summary>
    /// pause the timer
    /// </summary>
    /// <param name="pause"></param>
    public void Pause(bool pause)
    {
        _paused = pause;
    }

    public void Restart()
    {
        _timeStamp = Time.realtimeSinceStartup;
        Pause(false);
    }
}
