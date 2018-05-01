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

        private int blockId = 0;

        private Animator animator;
        private SurvivalBehaviour survivalBehaviour;

        public void Init(ActorSurvival actorSurvival)
        {
            blockId = Animator.StringToHash(blockName);

            animator = actorSurvival.GetComponent<Animator>();
            survivalBehaviour = animator.GetBehaviour<SurvivalBehaviour>();

            if (survivalBehaviour != null)
                survivalBehaviour.actorSurvival = actorSurvival ;
        }

        public void PlayBlockAnim(bool isBlocking) { animator.SetBool(blockId, isBlocking); }
    }
}
