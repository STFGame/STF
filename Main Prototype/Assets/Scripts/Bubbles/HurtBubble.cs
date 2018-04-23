using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Actor.Bubbles
{
    public class HurtBubble : Bubble
    {
        public delegate void OnHurt(BubbleZone zone, bool isHit);
        public event OnHurt HurtEvent;

        private bool isHit = false;

        protected override void OnTriggerEnter(Collider other)
        {
            if (!isEnabled)
                return;

            Update_HurtEvent(bubbleZone, (other.CompareTag("HitBubble") && other.gameObject.layer != gameObject.layer));
        }

        protected override void OnTriggerExit(Collider other)
        {
            if (!isEnabled)
                return;

            Update_HurtEvent(BubbleZone.None, false);
        }

        private void Update_HurtEvent(BubbleZone zone, bool isHit)
        {
            if (this.isHit == isHit)
                return;

            this.isHit = isHit;

            if (HurtEvent != null)
                HurtEvent(zone, isHit);
        }

    }
}