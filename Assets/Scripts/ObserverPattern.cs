using System.Collections.Generic;
using UnityEngine;

public abstract class Observer : MonoBehaviour
{
    public abstract void OnNotify(object value, int collValue, NotificationType notification);
}

public abstract class Subject : MonoBehaviour
{
    private List<Observer> _observers = new List<Observer>();

    public void RegisterObserver(Observer observer)
    {
        _observers.Add(observer);
    }

    public void UnregisterObserver(Observer observer)
    {
        _observers.Remove(observer);
    }

    /// <summary>
    /// Loops through each observer in the list and notifies them of a new notification
    /// <summary>
    public void Notify(object value, int collValue, NotificationType notificationType)
    {
        foreach(var observer in _observers)
        {
            observer.OnNotify(value, collValue, notificationType);
        }
    }
}

public enum NotificationType
{
    Hit, Move, Stop, Die
}
