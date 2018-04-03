using System;
using UnityEngine;
using Utility.Enums;
using Utility.Identifer;

namespace Actor.Collisions
{
    public class Hitbox : Encounter<object>
    {
        private bool isHit = false;

        public Hitbox() { }

        public Hitbox(int layer, BodyArea bodyArea)
        {
            this.bodyArea = bodyArea;
            this.layer = layer;
        }

        public override void OnTriggerEnter(Collider collider)
        {
            if (collider.CompareTag(UnityTag.Attackbox.ToString()) && collider.gameObject.layer != layer)
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
                action(this);
        }

        #region Properties
        public override BodyArea BodyArea
        {
            get { return bodyArea; }
        }
        #endregion
    }
}
