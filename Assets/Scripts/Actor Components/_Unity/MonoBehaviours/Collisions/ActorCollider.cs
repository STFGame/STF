using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Actor.Collisions;

namespace Actor
{
    public class ActorCollider<T> : ActorEncounter<T>
    {
        private void OnCollisionEnter(UnityEngine.Collision collision)
        {
            encounter.OnCollisionEnter(collision);
        }

        private void OnCollisionExit(UnityEngine.Collision collision)
        {
            encounter.OnCollisionExit(collision);
        }

        public override Encounter<T> Encounter
        {
            get { return encounter; }
            set { encounter = value; }
        }
    }
}
