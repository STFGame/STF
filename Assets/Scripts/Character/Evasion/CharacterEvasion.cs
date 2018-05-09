using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Character
{
    public class CharacterEvasion : MonoBehaviour
    {
        #region CharacterEvasion Variables
        [SerializeField] private float rollSpeed;

        [Tooltip("How long the roll lasts (and all effects of roll)")]
        [SerializeField] [Range(0f, 5f)] private float rollLength = 2f;
        [SerializeField] [Range(0f, 5f)] private float dodgeLength = 2f;

        private new Rigidbody rigidbody;
        private Animator animator;

        public bool Rolling { get; private set; }
        public bool Dodging { get; private set; }
        #endregion

        #region Initialization
        // Use this for initialization
        private void Awake()
        {
            rigidbody = GetComponent<Rigidbody>();
            animator = GetComponent<Animator>();

            Rolling = false;
            Dodging = false;
        }
        #endregion

        #region Roll
        public void Roll(bool roll, float direction)
        {
            if (roll)
                StartCoroutine(RollAction(direction));
        }

        private IEnumerator RollAction(float direction)
        {
            while (true)
            {
                Rolling = true;
                gameObject.layer = (int)Layer.PlayerDynamic;

                yield return new WaitForSeconds(rollLength);

                gameObject.layer = (int)Layer.PlayerStatic;
                Rolling = false;

                yield break;
            }
        }
        #endregion

        #region Dodge
        public void Dodge(bool dodge)
        {
            if (dodge)
                StartCoroutine(DodgeAction());
        }

        private IEnumerator DodgeAction()
        {
            while (true)
            {
                Dodging = true;
                gameObject.layer = (int)Layer.PlayerDynamic;

                yield return new WaitForSeconds(dodgeLength);

                gameObject.layer = (int)Layer.PlayerStatic;
                Dodging = false;

                yield break;
            }
        }
        #endregion

        public void AnimateEvasion()
        {
            animator.SetInteger("Roll", 1);
        }
    }
}
