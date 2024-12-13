
using UnityEngine;

public class TimeController : MonoBehaviour
{
    [Header("---------- Event buses")]
    [SerializeField] private TimeEventBusScrObj _timeEventBus;

    public static readonly float ONE_HUNDRED_MILLISECOND = 0.1f;
    public static readonly int ONE_SECOND = 1;
    public static readonly int ONE_MINUTE = 60;
    public static readonly int ONE_HOUR = 3600;


    private bool _initTimer = true;
    private float _ticks = 0;
    private int _milliSeconds = 0;
    private int _seconds = 0;
    

    private void Update()
    {
        if (!_initTimer)
        {
            return;
        }

        _ticks += Time.deltaTime;

        if (_ticks >= ONE_HUNDRED_MILLISECOND)
        {            
            OneHundredMillisecondsElapsed();

            if (_milliSeconds % (ONE_SECOND * 10) == 0)
            {
                OneSecondElapsed();
            }
            if (_seconds % ONE_MINUTE == 0)
            {
                OneMinuteElapsed();
            }                
            if (_seconds % ONE_HOUR == 0)
            {
                OneHourElapsed();
            }
        }        
    }

    private void OneHundredMillisecondsElapsed()
    {
        _timeEventBus.RaiseOneHundredMillisecondsEvent();
        _milliSeconds ++;
        _ticks = 0;
    }
    private void OneSecondElapsed()
    {
        _timeEventBus.RaiseOneSecondEvent();
        _milliSeconds = 0;
        _seconds++;
    }
    private void OneMinuteElapsed()
    {
        _timeEventBus.RaiseOneMinuteEvent();
    }
    private void OneHourElapsed()
    {
        _timeEventBus.RaiseOneHourEvent();
        _seconds = 0;
    }

}
