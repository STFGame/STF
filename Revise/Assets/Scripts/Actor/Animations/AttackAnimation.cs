using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Actor.Animations
{
    [Serializable]
    public class AttackAnimation : Animation
    {
        [SerializeField]private string attackName = "AttackIndex";

        private int attack = 0;

        public override void Init<T>(MonoBehaviour mono)
        {
            animator = mono.GetComponent<Animator>();
        }

        public void PlayAttack(int attack)
        {
            this.attack = attack;

            animator.SetInteger(attackName, attack);
        }
    }
}
