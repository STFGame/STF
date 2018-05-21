using Actions;
using Boxes;
using Managers;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
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

        private new Rigidbody rigidbody;
        private Animator animator;

        private bool rolling = false;
        private bool dodging = false;

        private List<Hurtbox> hurtboxes = new List<Hurtbox>();

        public bool Rolling { get { return rolling; } }
        public bool Dodging { get { return dodging; } }
        #endregion

        #region Load
        // Use this for initialization
        private void Awake()
        {
            rigidbody = GetComponent<Rigidbody>();
            animator = GetComponent<Animator>();
        }

        private void Start()
        {
            BoxArea[] boxArea = Enum.GetValues(typeof(BoxArea)).Cast<BoxArea>().ToArray();
            for (int i = 0; i < boxArea.Length; i++)
            {
                Hurtbox hurtbox = (Hurtbox)GetComponent<BoxManager>().GetBox(BoxType.Hurtbox, boxArea[i]);
                if (hurtbox)
                    hurtboxes.Add(hurtbox);
            }
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
            {
                dodgeAction.Perform(ref dodging);

                for (int i = 0; i < hurtboxes.Count; i++)
                    hurtboxes[i].Enabled(!dodging);
            }
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
