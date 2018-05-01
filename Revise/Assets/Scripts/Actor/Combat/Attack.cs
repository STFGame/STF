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
    public class Attack
    {
        [SerializeField] private string attackName = "Base Attack";
        public AttackAnimation attackAnimation = AttackAnimation.LightAttack1;

        public AttackData[] attackData;
        [SerializeField] private AttackTravel attackDisplace = new AttackTravel(0f, 0f, 0f);

        public AttackTravel Displace { get { return attackDisplace; } }

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

            if (attackData != null)
                for (int i = 0; i < attackData.Length; i++)
                    attackData[i].EnableAttack(isEnabled);

            return (this.attackId == attackId) ? (int)attackAnimation : 0;
        }

        public Vector3 AttackVelocity(Vector3 currentVelocity)
        {
            return Vector3.zero;
        }
    }
}
