using System;
using UnityEngine;
using Utility.Enums;
using Utility.Identifer;

namespace Actor.Encounters
{
    [Serializable]
    public class Hitbox : Encounter<bool>
    {
        private bool isHit = false;

        public Hitbox() { }

        public Hitbox(BodyArea bodyArea)
        {
            this.bodyArea = bodyArea;
        }

        public override void OnTriggerEnter(Collider collider)
        {
            if (collider.CompareTag(Tag.Attackbox))
                UpdateState(true);
        }

        public override void OnTriggerExit(Collider collider)
        {
            UpdateState(false);
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

        #region Properties
        public override BodyArea BodyArea
        {
            get { return bodyArea; }
        }
        #endregion
    }
}
