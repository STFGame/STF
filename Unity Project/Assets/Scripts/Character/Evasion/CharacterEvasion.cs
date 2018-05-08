using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Character
{
    public class CharacterEvasion : MonoBehaviour
    {
        [SerializeField] private float rollSpeed;

        [Tooltip("How long the roll lasts (and all effects of roll)")]
        [SerializeField] private float rollLength = 2f;
        [SerializeField] private float dodgeLength = 2f;

        private new Rigidbody rigidbody;
        private CharacterMove characterMove;
        private Animator animator;

        public bool Rolling { get; private set; }
        public bool Dodging { get; private set; }

        // Use this for initialization
        private void Awake()
        {
            rigidbody = GetComponent<Rigidbody>();
            characterMove = GetComponent<CharacterMove>();
            animator = GetComponent<Animator>();

            Rolling = false;
            Dodging = false;
        }

        public void Roll(bool roll, float direction)
        {
            if (roll)
                StartCoroutine(RollAction(direction));
        }

        private IEnumerator RollAction(float direction)
        {
            while(true)
            {
                Rolling = true;
                Debug.Log("Entered roll");
                yield return new WaitForSeconds(rollLength);
                Debug.Log("Exited roll");
                Rolling = false;
                yield break;
            }
        }

        public void Dodge(bool dodge)
        {
            if (dodge)
                StartCoroutine(DodgeAction());
        }

        private IEnumerator DodgeAction()
        {
            while(true)
            {
                Dodging = true;
                yield return new WaitForSeconds(dodgeLength);
                Dodging = false;
                yield break;
            }
        }

        public void AnimateEvasion()
        {
            animator.SetInteger("Roll", 1);
        }
    }
}
