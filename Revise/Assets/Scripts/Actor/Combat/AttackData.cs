﻿using Actor.Bubbles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Actor.Combat
{
    [Serializable]
    public class AttackData
    {
        [SerializeField] private string name = "Attack";
        public BodyArea bodyArea = BodyArea.None;

        [SerializeField] private float maxRange = 1f;
        [SerializeField] private float minRange = 0f;
        [SerializeField] private AttackDamage damage = new AttackDamage(10f, 10f, 0.2f);

        [SerializeField] private bool masterEnable = false;

        private float enableTimer = 0;
        private bool enable = false;
        private bool active = false;

        private GameObject hitBubbleGB = null;
        private Bubble hitBubble;

        private Collider other = null;

        public void Initiate(GameObject hitBubbleGB)
        {
            this.hitBubbleGB = hitBubbleGB;

            if (this.hitBubbleGB)
                hitBubble = hitBubbleGB.GetComponent<Bubble>();

            this.hitBubbleGB.SetActive(enable);

            hitBubble.IntersectEvent += UpdateCollider;
        }

        public void EnableAttack(bool enable)
        {
            if (!masterEnable)
            {
                this.enable = enable || this.enable;

                if (this.enable)
                    this.enable = enableTimer < maxRange;

                enableTimer = (this.enable) ? enableTimer + Time.deltaTime : 0f;

                if (enableTimer < minRange)
                    return;
            }

            active = ((enableTimer >= minRange && enableTimer <= maxRange) || masterEnable);

            EnableHitBubble(active);

            if (active)
            {
                if (hitBubble != null)
                {
                    if (other != null)
                        other.GetComponentInParent<ActorSurvival>().TakeDamage(damage);
                }
            }
        }

        private void UpdateCollider(Collider other)
        {
            if(other != null)
                this.other = other;

            if(!active)
                this.other = null;

            if (this.other != null)
                Debug.Log(this.other.name);
        }

        private void EnableHitBubble(bool isEnabled)
        {
            if (hitBubbleGB == null)
                return;

            hitBubbleGB.SetActive(isEnabled);
        }
    }
}
