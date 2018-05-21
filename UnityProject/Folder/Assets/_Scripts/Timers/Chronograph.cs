using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace STF.Timers
{
    public class Chronograph
    {
        //Basic delay
        public static void Delay(MonoBehaviour me, float delayLength, Action myDelegate)
        {
            me.StartCoroutine(EnumeratorDelay(delayLength, myDelegate));
        }

        //Delay type of T
        public static void Delay<T>(MonoBehaviour me, float delayLength, Action<T> myDelegate, T variable)
        {
            me.StartCoroutine(EnumeratorDelay(delayLength, myDelegate, variable));
        }

        //Wait until
        public static void WaitUntil(MonoBehaviour me, bool predicate, Action myDelegate)
        {
            me.StartCoroutine(EnumeratorWait(predicate, myDelegate));
        }

        //IEnumerator Wait
        private static IEnumerator EnumeratorWait(bool predicate, Action myDelegate)
        {
            yield return new WaitUntil(() => predicate);
            myDelegate();
        }

        //IEnumerator delay of type T
        private static IEnumerator EnumeratorDelay<T>(float delayLength, Action<T> myDelegate, T variable)
        {
            yield return new WaitForSeconds(delayLength);
            myDelegate(variable);
        }

        //Basic delay
        private static IEnumerator EnumeratorDelay(float delayLength, Action myDelegate)
        {
            yield return new WaitForSeconds(delayLength);
            myDelegate();
        }
    }
}
