using Actor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Actor.Animations
{
    [Serializable]
    public class SurvivalAnim
    {
        [SerializeField] private string blockName = "IsBlocking";

        private int blockId = 0;

        private Animator animator;
        private StateMachineBehaviour survivalBehaviour;

        public void Init(ActorSurvival actorSurvival)
        {
            blockId = Animator.StringToHash(blockName);

            animator = actorSurvival.GetComponent<Animator>();
            survivalBehaviour = animator.GetBehaviour<StateMachineBehaviour>();
        }

        public void PlayBlockAnim(bool isBlocking) { animator.SetBool(blockId, isBlocking); }
    }
}
