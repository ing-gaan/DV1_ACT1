using System;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/Event Buses/Game/Game events", fileName = "New gameEventBusScrObj")]
public class GameEventBusScrObj : ScriptableObject
{
    public event Action EnemyKillEvent;
    public event Action OneLiveLostEvent;


    public void RaiseEnemyKillEvent()
    {
        EnemyKillEvent?.Invoke();
    }
    public void RaiseOneLiveLostEvent()
    {
        OneLiveLostEvent?.Invoke();
    }

}
