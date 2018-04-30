using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Actor.Animations
{
    [Serializable]
    public class BlockAnimation : Animation
    {
        [SerializeField] private string blockName;
        [SerializeField] private string stunName;

        public override void Init<T>(MonoBehaviour mono)
        {
            ActorSurvival actorSurvival = (ActorSurvival)mono;
            animator = actorSurvival.GetComponent<Animator>();
            //behaviour = animator.GetBehaviour<MovementBehaviour>();
        }

        public void Block(bool block) { animator.SetBool(blockName, block); }

        public void Stun(bool stun) { animator.SetBool(stunName, stun); }
    }
}
