using System.Collections.Generic;

public class EventsManager
{
    public delegate void EventReceiver(params object[] parameterContainer);
    private static Dictionary<string, EventReceiver> _events;

    /// <summary>
    /// Método para suscribirnos a eventos
    /// </summary>
    /// <param name="eventName"></param>
    /// <param name="listener"></param>
    public static void SubscribeToEvent(string eventName, EventReceiver listener)
    {
        if (_events == null)
            _events = new Dictionary<string, EventReceiver>();

        if (!_events.ContainsKey(eventName))
            _events.Add(eventName, null);

        _events[eventName] += listener;
    }

    /// <summary>
    /// Método para desuscribirnos de eventos
    /// </summary>
    /// <param name="eventName"></param>
    /// <param name="listener"></param>
    public static void UnsubscribeToEvent(string eventName, EventReceiver listener)
    {
        if (_events != null)
        {
            if (_events.ContainsKey(eventName))
                _events[eventName] -= listener;
        }
    }

    /// <summary>
    /// Método para desuscribir todas las clases
    /// </summary>
    public static void UnsubscribeAllEvents()
    {
        if(_events != null)
        {
            _events.Clear();
        }
    }

    /// <summary>
    /// Disparar un evento sin parámetros
    /// </summary>
    /// <param name="eventName"></param>
    public static void TriggerEvent(string eventName)
    {
        TriggerEvent(eventName, null);
    }

    /// <summary>
    /// Dispara un evento con parámetros
    /// </summary>
    /// <param name="eventName"></param>
    /// <param name="parametersWrapper"></param>
    public static void TriggerEvent(string eventName, params object[] parametersWrapper)
    {
        if (_events == null)
        {
            UnityEngine.Debug.LogWarning("No events subscribed");
            return;
        }

        if (_events.ContainsKey(eventName))
        {
            if (_events[eventName] != null)
                _events[eventName](parametersWrapper);
        }
    }
}