using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Actor
{
    public class SurvivalBehaviour : StateMachineBehaviour
    {
        public ActorSurvival actorSurvival;

        public readonly int hitHeadHash = Animator.StringToHash("Base Layer.Hits.Ground Hits.HitHead");
        public readonly int hitStomachHash = Animator.StringToHash("Base Layer.Hits.Ground Hits.HitStomach");
        public readonly int hitLegHash = Animator.StringToHash("Base Layer.Hits.Ground Hits.HitLeg");

        private float animationSpeed;

        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            animationSpeed = stateInfo.speed;

            base.OnStateEnter(animator, stateInfo, layerIndex);
        }
    }
}
