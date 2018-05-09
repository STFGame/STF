using Character;
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
        private int hitID = 0;

        private CharacterHealth characterHealth = null;

        private void Awake()
        {
            characterHealth = parent.GetComponentInParent<CharacterHealth>();
        }

        private void OnTriggerEnter(Collider other)
        {
            characterHealth.hit = true;
            characterHealth.area = hitID;

            HurtUpdate((int)boxArea, true);
        }

        private void OnTriggerExit(Collider other)
        {
            characterHealth.hit = false;
            characterHealth.area = 0;

            HurtUpdate(0, false);
        }

        public void ResetHurtbox()
        {
            hurt = false;
            hitID = 0;
        }

        private void HurtUpdate(int hitId, bool hurt)
        {
            if (this.hurt == hurt || this.hitID == hitId)
                return;

            this.hurt = hurt;
            this.hitID = hitId;

            if (HurtEvent != null)
                HurtEvent(hitId);
        }
    }
}