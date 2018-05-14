using Actions;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Character
{
    public enum EvasionMode { None, Dodge, RollForward, RollBackward }

    public class CharacterEvasion : MonoBehaviour
    {
        #region CharacterEvasion Variables
        [SerializeField] private float rollSpeed;

        [Tooltip("How long the roll lasts (and all effects of roll)")]
        [SerializeField] private Act dodgeAction = null;
        [SerializeField] private Act rollAction = null;

        CharacterMove characterMove = null;
        private new Rigidbody rigidbody;
        private Animator animator;

        private bool rolling = false;
        private bool dodging = false;

        public bool Rolling { get { return rolling; } }
        public bool Dodging { get { return dodging; } }
        #endregion

        #region Load
        // Use this for initialization
        private void Awake()
        {
            characterMove = GetComponent<CharacterMove>();
            rigidbody = GetComponent<Rigidbody>();
            animator = GetComponent<Animator>();
        }
        #endregion

        #region Updates
        public void Evade(EvasionMode evasionMode, float direction)
        {
            Roll(evasionMode, direction);

            Dodge(evasionMode);
        }

        public void Roll(EvasionMode evasionMode, float direction)
        {
            if (evasionMode == EvasionMode.RollForward || evasionMode == EvasionMode.RollBackward)
                rolling = true;

            if (rolling)
            {
                rollAction.Perform(ref rolling);
            }
        }

        public void Dodge(EvasionMode evasionMode)
        {
            if (evasionMode == EvasionMode.Dodge)
                dodging = true;

            if (dodging)
                dodgeAction.Perform(ref dodging);
        }
        #endregion

        #region Visual FX and Animation
        public void AnimateEvasion(int evasionIndex)
        {
            animator.SetInteger("EvasionIndex", evasionIndex);
        }
        #endregion
    }
}
