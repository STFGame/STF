using Actor.Encounters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using Utility.Identifer;

namespace Actor
{
    public class EntityBox : MonoBehaviour
    {
        public Encounter<bool> encounter;

        private void OnTriggerEnter(Collider other)
        {
            encounter.OnTriggerEnter(other);
        }

        private void OnTriggerExit(Collider other)
        {
            encounter.OnTriggerExit(other);
        }
    }
}
