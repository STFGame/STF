using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Utility
{
    [Serializable]
    public struct Timer
    {
        [SerializeField] private float timer;
        [SerializeField] private float holdLength;

        public bool DelayTimer(float length)
        {
            if (timer < length)
            {
                timer += Time.deltaTime;
                return false;
            }

            timer = 0f;
            return true;
        }

        public bool HoldTimer(bool hold)
        {
            if(!hold)
            {
                timer = 0f;
                return false;
            }

            timer += Time.deltaTime;

            return (timer < holdLength);
        }
    }
}
