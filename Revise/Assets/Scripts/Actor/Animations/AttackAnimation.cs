using Actor.StateBehaviours;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Actor.Animations
{
    [Serializable]
    public class AttackAnimation
    {
        [SerializeField] private string attackIndexName = "AttackIndex";

        private Animator animator;
        private CombatBehaviour attackBehaviour;

        public void Init(ActorCombat actorCombat)
        {
            animator = actorCombat.GetComponent<Animator>();
            attackBehaviour = animator.GetBehaviour<CombatBehaviour>();

            if (attackBehaviour != null)
                attackBehaviour.actorCombat = actorCombat;
        }

        public void PlayAttackAnim(int index) { animator.SetInteger(attackIndexName, index); }
    }
}
