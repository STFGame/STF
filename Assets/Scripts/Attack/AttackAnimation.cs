using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Actor.Animation
{
    /// <summary>
    /// Class that contains animations that are associated with attacks
    /// </summary>
    [Serializable]
    public class AttackAnimation
    {
        [SerializeField] private string attackName = "Attack";

        private Animator animator;
        private AttackBehaviour behaviour;

        public void Init(ActorAttack attack)
        {
            animator = attack.GetComponent<Animator>();
            behaviour = animator.GetBehaviour<AttackBehaviour>();

            if (behaviour != null)
                behaviour.actorAttack = attack;
        }

        public void SetAttack(int value) { animator.SetInteger(attackName, value); }
    }
}
