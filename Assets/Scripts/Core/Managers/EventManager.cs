using UnityEngine;
using System.Collections.Generic;
using System;

namespace DeadZone.Core.Managers
{
    /// <summary>
    /// Global event manager for game-wide event broadcasting.
    /// </summary>
    public class EventManager : MonoBehaviour
    {
        public static EventManager Instance { get; private set; }

        private Dictionary<string, Action> eventDictionary = new Dictionary<string, Action>();
        private Dictionary<string, Delegate> typedEventDictionary = new Dictionary<string, Delegate>();

        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }

            Instance = this;
            DontDestroyOnLoad(gameObject);
        }

        public void Subscribe(string eventName, Action listener)
        {
            if (eventDictionary.ContainsKey(eventName))
            {
                eventDictionary[eventName] += listener;
            }
            else
            {
                eventDictionary[eventName] = listener;
            }
        }

        public void Unsubscribe(string eventName, Action listener)
        {
            if (eventDictionary.ContainsKey(eventName))
            {
                eventDictionary[eventName] -= listener;
                if (eventDictionary[eventName] == null)
                {
                    eventDictionary.Remove(eventName);
                }
            }
        }

        public void Broadcast(string eventName)
        {
            if (eventDictionary.ContainsKey(eventName))
            {
                eventDictionary[eventName]?.Invoke();
            }
        }

        public void Subscribe<T>(string eventName, Action<T> listener)
        {
            string key = eventName + typeof(T).Name;
            if (typedEventDictionary.ContainsKey(key))
            {
                typedEventDictionary[key] = (Action<T>)typedEventDictionary[key] + listener;
            }
            else
            {
                typedEventDictionary[key] = listener;
            }
        }

        public void Unsubscribe<T>(string eventName, Action<T> listener)
        {
            string key = eventName + typeof(T).Name;
            if (typedEventDictionary.ContainsKey(key))
            {
                typedEventDictionary[key] = (Action<T>)typedEventDictionary[key] - listener;
                if (typedEventDictionary[key] == null)
                {
                    typedEventDictionary.Remove(key);
                }
            }
        }

        public void Broadcast<T>(string eventName, T parameter)
        {
            string key = eventName + typeof(T).Name;
            if (typedEventDictionary.ContainsKey(key))
            {
                ((Action<T>)typedEventDictionary[key])?.Invoke(parameter);
            }
        }
    }
}
