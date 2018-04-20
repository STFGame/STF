using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Actor.Animations
{
    [Serializable]
    public abstract class Animation
    {
        protected Animator animator;
        protected StateMachineBehaviour behaviour;

        public abstract void Init<T>(MonoBehaviour mono) where T : StateMachineBehaviour;
    }
}
