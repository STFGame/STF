using Actor.Bubbles;
using ComboSystem;
using Controls;
using System;
using UnityEngine;
using Utility;

namespace Actor
{
    public enum AttackType
    {
        None,
        LightAttackOne, LightAttackTwo, LightAttackThree,
        HeavyAttackOne, HeavyAttackTwo, HeavyAttackThree
    }

    [Serializable]
    internal struct Damage
    {
        public float damage;
        public float force;

        public Damage(float damage, float force)
        {
            this.damage = damage;
            this.force = force;
        }
    }

    [Serializable]
    public struct Displacement
    {
        [SerializeField] [Range(-1f, 1f)] private float directionX;
        [SerializeField] [Range(-1f, 1f)] private float directionY;
        public float speed;

        public Vector2 Direction { get { return new Vector2(directionX, directionY); } }

        public Displacement(float directionX, float directionY, float speed)
        {
            this.directionX = directionX;
            this.directionY = directionY;
            this.speed = speed;
        }
    }

    [Serializable]
    public class Attack
    {
        [SerializeField] private string attackName = "Base Attack";
        public BodyArea[] bodyAreas;
        public AttackType attackType = AttackType.None;

        [SerializeField] private Damage damage = new Damage(10f, 10f);
        [SerializeField] private Displacement displacement = new Displacement(0f, 0f, 10f);

        [SerializeField] private STFRange enableRange = new STFRange(0f, 1f);

        private GameObject[] hitBubble = null;
        private float enableTimer = 0f;

        private bool isEnabled = false;
        private bool startTimer = false;

        public Displacement Displace { get { return displacement; } }

        private int attackId;

        public void Init(GameObject[] hitBubble)
        {
            attackId = Animator.StringToHash(attackName);
            this.hitBubble = hitBubble;

            Enable(false);
        }

        public int PerformAttack(int attackId)
        {
            if (hitBubble == null)
                return 0;

            if (this.attackId == attackId)
                startTimer = true;

            Enable(isEnabled || startTimer);

            AttackTimer();

            return (this.attackId == attackId) ? (int)attackType : 0;
        }

        private void AttackTimer()
        {
            enableTimer = (startTimer) ? enableTimer + Time.deltaTime : 0;

            if (enableTimer > enableRange.maximum)
                startTimer = false;

            isEnabled = (enableTimer > enableRange.minimum && enableTimer < enableRange.maximum);
        }

        private void Enable(bool value)
        {
            for (int i = 0; i < hitBubble.Length; i++)
                hitBubble[i].SetActive(value);
        }
    }
}
