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
        public BodyArea bodyArea = BodyArea.None;
        public AttackType attackType = AttackType.None;

        [SerializeField] private Damage damage = new Damage(10f, 10f);
        [SerializeField] private Displacement displacement = new Displacement(0f, 0f, 10f);
        [SerializeField] private Combo combo = new Combo();

        [SerializeField] private Graph enableLength = new Graph(0f, 1f);

        private GameObject hitBubble = null;
        private bool isEnabled = false;
        private float enableTimer = 0f;

        public Displacement Displace { get { return displacement; } }

        public Action<int> AttackEvent;
        private int attackNumber = 0;

        public void Init(GameObject hitBubble, Device device)
        {
            combo.Init(device);
            this.hitBubble = hitBubble;

            Enable(false);
        }

        //Perform an attack
        public bool PerformAttack()
        {
            if (hitBubble == null)
                return false;

            if (combo.CheckCombo() && !isEnabled)
            {
                isEnabled = true;
                Update_Attack((int)attackType);
            }
            AttackTimer();

            if (isEnabled && enableTimer > enableLength.minimum)
            {
                Enable(isEnabled);
                return true;
            }

            if (!isEnabled)
                Update_Attack(0);

            Enable(false);

            return false;
        }

        private void AttackTimer()
        {
            enableTimer = (isEnabled) ? enableTimer + Time.deltaTime : 0;

            if (isEnabled)
                isEnabled = (enableTimer < enableLength.maximum);
        }

        private void Update_Attack(int number)
        {
            if (number == attackNumber)
                return;

            attackNumber = number;

            if (AttackEvent != null)
                AttackEvent(attackNumber);
        }

        private void Enable(bool value)
        {
            hitBubble.SetActive(value);
        }
    }
}
