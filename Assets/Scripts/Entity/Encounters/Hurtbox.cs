using System;
using UnityEngine;
using Utility.Identifer;

namespace Entity.Encounters
{
    [Serializable]
    public class Hurtbox : Encounter<bool>
    {
        private bool isHit = false;

        public override void OnTriggerEnter(Collider collider)
        {
            if (collider.CompareTag(Tag.Attackbox))
                UpdateState(true);
        }

        private void UpdateState(bool value)
        {
            if (value == isHit)
                return;

            isHit = value;

            if (action != null)
            {
                Debug.Log("Hit");
                action(isHit);
            }
        }
    }
}
