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
    float _duration = 1;
    float _timeStamp;

    // Start is called before the first frame update
    void Start()
    {
        _timeStamp = Time.realtimeSinceStartup;
    }

    // Update is called once per frame
    void Update()
    {
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
}
