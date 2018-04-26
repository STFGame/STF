using Actor.Bubbles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Actor.Combat
{
    [Serializable]
    public class AttackContainer
    {
        [SerializeField] private string name = "Attack";

        [SerializeField] private BodyArea bodyArea = BodyArea.None;
        [SerializeField] private float maxRange = 1f;
        [SerializeField] private float minRange = 0f;
        [SerializeField] private Damage damage = new Damage(10f, 10f);

        public bool enable = false;
        private int count = 0;

        private float enableTimer = 0;

        public BodyArea BodyArea { get { return bodyArea; } }
        public GameObject HitBubbleGameObject { get; set; }

        private HitBubble hitBubble;

        public void Initiate()
        {
            if (HitBubbleGameObject)
                hitBubble = HitBubbleGameObject.GetComponent<HitBubble>();

            HitBubbleGameObject.SetActive(enable);
        }

        public void EnableAttack(bool enable)
        {
            this.enable = enable || this.enable;

            if (this.enable)
                this.enable = enableTimer < maxRange;

            enableTimer = (this.enable) ? enableTimer + Time.deltaTime : 0f;

            if (enableTimer < minRange)
                return;

            bool active = (enableTimer >= minRange && enableTimer <= maxRange);

            EnableHitBubble(active);

            if (active)
            {
                if (hitBubble != null)
                    hitBubble.damage = damage.damage;
            }
        }

        private void EnableHitBubble(bool isEnabled)
        {
            HitBubbleGameObject.SetActive(isEnabled);
        }
    }
}
