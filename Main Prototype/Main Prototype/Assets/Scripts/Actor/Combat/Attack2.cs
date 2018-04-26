using Actor.Bubbles;
using Actor.Combat;
using ComboSystem;
using Controls;
using System;
using UnityEngine;
using Utility;

namespace Actor
{
    [Serializable]
    public class Attack2
    {
        [SerializeField] private string attackName = "Base Attack";
        public AttackType attackType = AttackType.None;

        public AttackContainer[] attackContainer;
        [SerializeField] private Displacement displacement = new Displacement(0f, 0f, 10f);

        public Displacement Displace { get { return displacement; } }

        private bool isEnabled = false;

        //attackId is for playing the animation
        private int attackId;

        public void Init()
        {
            attackId = Animator.StringToHash(attackName);
        }

        public int PerformAttack(int attackId)
        {
            isEnabled = this.attackId == attackId;

            if (attackContainer != null)
                for (int i = 0; i < attackContainer.Length; i++)
                    attackContainer[i].EnableAttack(isEnabled);

            return (this.attackId == attackId) ? (int)attackType : 0;
        }

        public Vector3 AttackVelocity(Vector3 currentVelocity)
        {
            return Vector3.zero;
        }
    }
}
