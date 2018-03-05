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
            movement.rigidBody.transform.localRotation = Quaternion.Euler(0f, movement.rotation, 0f);
            base.OnStateExit(animator, stateInfo, layerIndex);
        }
    }
}
