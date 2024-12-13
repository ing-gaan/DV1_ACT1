using UnityEngine;
using System;

[CreateAssetMenu(menuName = "ScriptableObjects/Event Buses/Time/Time events", fileName = "New timeEventBusScrObj")]
public class TimeEventBusScrObj : ScriptableObject
{
    public event Action OneHundredMillisecondsEvent;
    public event Action OneSecondEvent;
    public event Action OneMinuteEvent;
    public event Action OneHourEvent;

    public void RaiseOneHundredMillisecondsEvent()
    {
        OneHundredMillisecondsEvent?.Invoke();
    }
    public void RaiseOneSecondEvent()
    {
        OneSecondEvent?.Invoke();      
    }

    public void RaiseOneMinuteEvent()
    {
        OneMinuteEvent?.Invoke();
    }

    public void RaiseOneHourEvent()
    {
        OneHourEvent?.Invoke();
    }

}