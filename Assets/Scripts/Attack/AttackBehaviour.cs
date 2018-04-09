using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Actor
{
    public class AttackBehaviour : StateMachineBehaviour
    {
        [HideInInspector] public ActorAttack actorAttack;

        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            Speed = stateInfo.length / stateInfo.speed;
            actorAttack.IsAttacking = true;

            Debug.Log(Speed);
            base.OnStateEnter(animator, stateInfo, layerIndex);
        }

        public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            actorAttack.IsAttacking = false;
        }

        public bool IsAttacking { get; private set; }
        public float Speed { get; private set; }
    }
}
