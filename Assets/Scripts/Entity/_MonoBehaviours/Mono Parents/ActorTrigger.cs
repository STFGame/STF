using Actor.Encounters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Actor
{
    public class ActorTrigger : ActorEncounter<bool>
    {
        private void OnTriggerEnter(UnityEngine.Collider other)
        {
            encounter.OnTriggerEnter(other);
        }

        private void OnTriggerExit(UnityEngine.Collider other)
        {
            encounter.OnTriggerExit(other);
        }

        public override Encounter<bool> Encounter { get { return encounter; } }
    }
}
