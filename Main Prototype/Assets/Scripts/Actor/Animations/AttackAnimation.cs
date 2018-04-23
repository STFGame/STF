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
            if (this.attack > 0)
            {
                timer += Time.deltaTime;

                if(timer >= saveTimer)
                {
                    timer = 0f;
                    this.attack = 0;
                }
            }

            this.attack = attack;

            animator.SetInteger(attackName, attack);
        }
    }
}
