using UnityEngine;
using UnityEngine.EventSystems;

namespace Alerts
{
    public class Alert
    {
        public static void Send<T>(GameObject target, ExecuteEvents.EventFunction<T> functor) where T : IEventSystemHandler
        {
            ExecuteEvents.Execute(target, null, functor);
        }

        public static void SendChildren<T>(GameObject target, ExecuteEvents.EventFunction<T> functor) where T :  IEventSystemHandler
        {
            ExecuteEvents.ExecuteHierarchy(target, null, functor);
        }
    }
}
