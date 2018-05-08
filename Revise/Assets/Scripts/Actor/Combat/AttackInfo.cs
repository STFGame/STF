using Actor.Bubbles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Actor.Combat
{
    [Serializable]
    public class AttackInfo
    {
        [SerializeField] private string name = "Attack";
        public BodyArea bodyArea = BodyArea.None;
        public BubbleType bubbleType = BubbleType.None;

        [SerializeField] [Range(0f, 10f)] private float maxRange = 1f;
        [SerializeField] [Range(0f, 10f)] private float minRange = 0f;
        public Damage damage = new Damage(10f, 10f, 0.2f);

        [SerializeField] private bool masterEnable = false;

        private bool isEnabled = false;
        private float enableTimer = 0;

        private GameObject hitBubbleGB = null;
        private Bubble hitBubble = null;

        private Collider other = null;
        private int count = 0;

        public bool Active { get; private set; }
        public string Tag { get; private set; }

        public void Initiate(GameObject hitBubbleGB)
        {
            this.hitBubbleGB = hitBubbleGB;

            if (this.hitBubbleGB)
                hitBubble = hitBubbleGB.GetComponent<Bubble>();

            this.hitBubbleGB.SetActive(isEnabled);

            Tag = hitBubbleGB.tag;

            hitBubble.IntersectEvent += UpdateCollider;
        }

        public void EnableAttack(bool isEnabled)
        {
            if (!masterEnable)
            {
                this.isEnabled = isEnabled || this.isEnabled;

                if (this.isEnabled)
                    this.isEnabled = enableTimer < maxRange;

                enableTimer = (this.isEnabled) ? enableTimer + Time.deltaTime : 0f;

                if (enableTimer < minRange)
                    return;
            }

            Active = ((enableTimer >= minRange && enableTimer <= maxRange) || masterEnable);

            EnableHitBubble(Active);

            count = (Active) ? count : 0;

            if (Active)
            {
                if (hitBubble == null)
                    return;

                if (other == null || count != 0)
                    return;

                other.GetComponentInParent<ActorSurvival>().TakeDamage(this);
                count++;
            }
        }

        private void UpdateCollider(Collider other)
        {
            if (other != null)
                this.other = other;

            if (!Active)
                this.other = null;
        }

        private void EnableHitBubble(bool isEnabled)
        {
            if (hitBubbleGB == null)
                return;

            hitBubbleGB.SetActive(isEnabled);
        }
    }
}
