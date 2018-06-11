using UnityEngine;
using UnityEngine.EventSystems;

namespace Broadcasts
{
    public class Broadcast
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
