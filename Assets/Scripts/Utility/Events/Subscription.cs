using Actor;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Utility.Events
{
    public class Subscription
    {
        public static IEnumerator Subscribe<Class, DataType>(GameObject gameObject, Action<DataType> method) where Class : IEvent<DataType>
        {
            yield return new WaitForEndOfFrame();
            gameObject.GetComponent<Class>().Action += method;
        }

        public static IEnumerator Unsubscribe<T, U>(GameObject gameObject, Action<U> method) where T : IEvent<U>
        {
            gameObject.GetComponent<T>().Action += method;
            yield break;
        }

        public static IEnumerator SubscribeAll<T, U>(GameObject gameObject, Action<U> method) where T : IEvent<U>
        {
            yield return new WaitForEndOfFrame();
            foreach (T item in gameObject.GetComponentsInChildren<T>())
                item.Action += method;
        }

        public static IEnumerator UnsubscribeAll<T, U>(GameObject gameObject, Action<U> method) where T : IEvent<U>
        {
            foreach (T item in gameObject.GetComponentsInChildren<T>())
                item.Action += method;
            yield break;
        }
    }
}
