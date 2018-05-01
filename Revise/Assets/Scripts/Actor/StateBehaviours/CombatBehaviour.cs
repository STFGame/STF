using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.Animations;

namespace Actor.StateBehaviours
{
    public class CombatBehaviour : StateMachineBehaviour
    {
        public ActorCombat actorCombat;

        public readonly int lightAttackOneHash = Animator.StringToHash("Base Layer.Combat.Ground Attacks.Light Attacks.LightAttackOne");
        public readonly int lightAttackTwoHash = Animator.StringToHash("Base Layer.Combat.Ground Attacks.Light Attacks.LightAttackTwo");
        public readonly int lightAttacKThreeHash = Animator.StringToHash("Base Layer.Combat.Ground Attacks.Light Attacks.LightAttackThree");

        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex, AnimatorControllerPlayable controller)
        {
            base.OnStateEnter(animator, stateInfo, layerIndex, controller);
        }
    }
}
