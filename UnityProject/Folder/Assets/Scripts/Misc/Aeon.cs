using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using UnityEngine;

namespace Misc
{
    public static class Aeon
    {
        public static void Invoke(this MonoBehaviour me, Action myDelegate, float time)
        {
            me.StartCoroutine(ExecuteAfterDelay(myDelegate, time));
        }

        private static IEnumerator ExecuteAfterDelay(Action myDelegate, float delay)
        {
            yield return new WaitForSeconds(delay);
            myDelegate();
        }
    }
}
