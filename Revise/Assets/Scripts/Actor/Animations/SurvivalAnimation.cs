using Actor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Actor.Animations
{
    [Serializable]
    public class SurvivalAnimation
    {
        [SerializeField] private string blockName = "IsBlocking";
        [SerializeField] private string stunName = "IsDisabled";
        [SerializeField] private string hitName = "HitIndex";

        public int BlockId { get; private set; }
        public int StunId { get; private set; }
        public int HitId { get; private set; }

        private Animator animator;
        private SurvivalBehaviour survivalBehaviour;

        public void Init(ActorSurvival actorSurvival)
        {
            BlockId = Animator.StringToHash(blockName);
            StunId = Animator.StringToHash(stunName);
            HitId = Animator.StringToHash(hitName);

            animator = actorSurvival.GetComponent<Animator>();
            survivalBehaviour = animator.GetBehaviour<SurvivalBehaviour>();

            if (survivalBehaviour != null)
                survivalBehaviour.actorSurvival = actorSurvival;
        }

        public void PlayBlockAnim(bool isBlocking) { animator.SetBool(BlockId, isBlocking); }

        public void PlayStunAnim(bool isStunned) { animator.SetBool(StunId, isStunned); }

        public void PlayHitAnim(int hitIndex) { animator.SetInteger(HitId, hitIndex); }
    }
}
