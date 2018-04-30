using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Actor.Animations
{
    [Serializable]
    public class AttackAnim
    {
        [SerializeField] private string attackIndexName = "AttackIndex";

        private Animator animator;
        private StateMachineBehaviour attackBehaviour;

        public void Init(ActorCombat actorCombat)
        {
            animator = actorCombat.GetComponent<Animator>();
            attackBehaviour = animator.GetBehaviour<StateMachineBehaviour>();
        }

        public void PlayAttackAnim(int index) { animator.SetInteger(attackIndexName, index); }
    }
}
