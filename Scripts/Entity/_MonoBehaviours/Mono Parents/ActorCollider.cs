using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Actor.Encounters;

namespace Actor
{
    public class ActorCollider : ActorEncounter<bool>
    {
        private void OnCollisionEnter(UnityEngine.Collision collision)
        {
            encounter.OnCollisionEnter(collision);
        }

        private void OnCollisionExit(UnityEngine.Collision collision)
        {
            encounter.OnCollisionExit(collision);
        }

        public override Encounter<bool> Encounter { get { return encounter; } }
    }
}
