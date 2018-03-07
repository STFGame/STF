using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Character.CC
{
    public class MovementBehaviour : StateMachineBehaviour
    {
        [HideInInspector]public Movement movement;

        public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            Debug.Log("Flipped");
            base.OnStateExit(animator, stateInfo, layerIndex);
        }
    }
}
