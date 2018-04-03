using Actor.Collisions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Actor
{
    public class ActorTrigger<T> : ActorEncounter<T>
    {
        private void OnTriggerEnter(UnityEngine.Collider other)
        {
            encounter.OnTriggerEnter(other);
        }

        private void OnTriggerExit(UnityEngine.Collider other)
        {
            encounter.OnTriggerExit(other);
        }

        public override Encounter<T> Encounter
        {
            get { return encounter; }
            set { encounter = value; }
        }
    }
}
