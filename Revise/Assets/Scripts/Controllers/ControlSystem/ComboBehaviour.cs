using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Controls
{
    public class ComboBehaviour : StateMachineBehaviour
    {
        [HideInInspector] public ControlManager comboManager;

        private int previousName = 0;

        public delegate void ComboDelegate(int name);
        public event ComboDelegate ComboEvent;

        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            Update_Combo(stateInfo.tagHash);

            base.OnStateEnter(animator, stateInfo, layerIndex);
        }

        private void Update_Combo(int name)
        {
            if (name == previousName)
                return;

            previousName = name;

            if (ComboEvent != null)
                ComboEvent(name);
        }
    }
}