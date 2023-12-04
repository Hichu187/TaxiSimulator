using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace PFramework.Runtime
{
    /// <summary>
    /// This is the simplest and fastest implementation for the event bus pattern.
    /// Since this is a static bus, DO NOT USE it when you are making a package,
    /// since it may conflict with user projects.
    ///
    /// It only makes sense to use it inside the project you maintain and own.
    /// </summary>
    /// <typeparam name="T">Event Type.</typeparam>
    public static class StaticBus<T> where T : IEvent
    {
        private static List<(Action<T> handler, int priority, bool allowInactive)> eventHandlers = new List<(Action<T> handler, int priority, bool allowInactive)>();

        public static void Subscribe(Action<T> listener)
        {
            Subscribe(listener, 0, false); // Default priority = 0, default allowInactive = false
        }

        public static void Subscribe(Action<T> listener, bool allowInactive)
        {
            Subscribe(listener, 0, allowInactive); // Default priority = 0
        }

        public static void Subscribe(Action<T> listener, int priority)
        {
            Subscribe(listener, priority, false); // Default allowInactive = false
        }

        public static void Subscribe(Action<T> listener, int priority, bool allowInactive)
        {
            eventHandlers.Add((listener, priority, allowInactive));
            eventHandlers = eventHandlers.OrderByDescending(h => h.priority).ToList();
        }

        public static void Unsubscribe(Action<T> listener)
        {
            eventHandlers.RemoveAll(h => h.handler == listener);
        }

        public static void Post(T @event)
        {
            foreach (var handler in eventHandlers)
            {
                if (!handler.allowInactive && handler.handler.Target is MonoBehaviour monoBehaviour)
                {
                    var gameObject = monoBehaviour.gameObject;

                    if (!IsGameObjectHierarchyActive(gameObject))
                    {
                        continue; // Skip inactive hierarchy
                    }
                }

                handler.handler.Invoke(@event);
            }
        }

        private static bool IsGameObjectHierarchyActive(GameObject gameObject)
        {
            if (gameObject == null)
            {
                return false;
            }

            if (!gameObject.activeSelf)
            {
                return false;
            }

            if (gameObject.transform.parent != null)
            {
                return IsGameObjectHierarchyActive(gameObject.transform.parent.gameObject);
            }

            return true;
        }
    }
}
