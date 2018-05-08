using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Boxes
{
    public class Hurtbox : Box
    {
        public delegate void Hurt(int hitId);
        public event Hurt HurtEvent;

        private bool hurt = false;
        private int hitId = 0;

        private void OnTriggerEnter(Collider other)
        {
            HurtUpdate((int)boxArea, true);
        }

        private void OnTriggerExit(Collider other)
        {
            HurtUpdate(0, false);
        }

        private void HurtUpdate(int hitId, bool hurt)
        {
            if (this.hurt == hurt || this.hitId == hitId)
                return;

            this.hurt = hurt;
            this.hitId = hitId;

            if (HurtEvent != null)
                HurtEvent(hitId);
        }
    }
}