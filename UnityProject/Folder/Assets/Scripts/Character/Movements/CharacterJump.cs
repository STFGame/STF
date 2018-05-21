using Actions;
using Movements;
using System.Collections;
using UnityEngine;

namespace Character
{
    public enum JumpMode { None, Jump, Descend }

    /// <summary>
    /// Handles the jumping and falling of the character.
    /// </summary>
    [RequireComponent(typeof(Rigidbody), typeof(Animator), typeof(Collider))]
    public class CharacterJump : Jump, IGroundable
    {
        #region Jump Variables
        [Header("Jump Values")]
        [SerializeField] private ActionRange m_FallAction = new ActionRange();
        [SerializeField] private ActionHold m_JumpAction = new ActionHold();

        private ParticleSystem landParticle = null;

        //Components to be cached
        private Rigidbody m_Rigidbody;
        private Transform m_Transform;
        private Animator m_JumpAnimator;
        private Gravity m_Gravity;

        public bool Grounded { get; set; }
        public bool Falling { get; private set; }
        #endregion

        #region Load
        private void Awake()
        {
            m_Rigidbody = GetComponent<Rigidbody>();
            m_Transform = GetComponent<Transform>();
            m_JumpAnimator = GetComponent<Animator>();
            m_Gravity = GetComponent<Gravity>();

            m_PreviousHeight = m_Rigidbody.position.y;
            m_JumpCount = m_MaxJumps;
        }

        #endregion

        #region Updates
        //Makes the character jump for as long as the jumpHold is held
        public override void Spring(Vector2 direction, bool jump, bool jumpHold)
        {
            jumpHold = m_JumpAction.ActionSuccess(jumpHold);

            if (Grounded)
            {
                m_PreviousHeight = m_Transform.position.y;
                m_JumpCount = m_MaxJumps;
            }

            if (m_FallAction.ActionSuccess(direction))
                m_Rigidbody.AddForce(Vector3.down * m_DescendSpeed);

            if (jump && m_JumpCount > 0)
            {
                float velocity = Mathf.Sqrt(2f * m_Gravity.Gravitation * m_JumpHeight);
                StartCoroutine(JumpDelay(velocity));
            }

            if (m_Gravity)
                m_Gravity.DecreaseGravity(jumpHold);

            AnimateJump(jump);
        }

        //Coroutine for the delay of the jump
        private IEnumerator JumpDelay(float velocity)
        {
            if (!Grounded)
            {
                m_Rigidbody.velocity = new Vector3(m_Rigidbody.velocity.x, velocity, 0f);
                m_JumpCount--;
                yield break;
            }

            yield return new WaitForSeconds(m_JumpDelay);
            m_Rigidbody.velocity = new Vector3(m_Rigidbody.velocity.x, velocity, 0f);

            yield return new WaitUntil(() => Grounded == false);
            m_JumpCount--;
        }

        //Checks to see whether the character is falling based on the comparision of their current height and their last height
        public override void Fall(float currentHeight)
        {
            if (currentHeight >= m_PreviousHeight)
            {
                m_PreviousHeight = currentHeight;
                Falling = false;
                return;
            }

            m_PreviousHeight = currentHeight + 0.5f;
            Falling = true;
        }
        #endregion

        #region Visual FX and Animations
        public void LandParticles(GameObject particleEffect)
        {
            if (landParticle == null)
            {
                Instantiate(particleEffect, m_Transform.position, m_Transform.rotation, m_Transform);
                landParticle = particleEffect.GetComponent<ParticleSystem>();
            }

            landParticle.transform.position = Vector3.zero;
            landParticle.gameObject.SetActive(false);
        }

        public void AnimateJump(bool jump)
        {
            m_JumpAnimator.SetBool("Jump", jump);
            m_JumpAnimator.SetBool("Falling", Falling);
            m_JumpAnimator.SetBool("Grounded", Grounded);
            m_JumpAnimator.SetInteger("Jump Count", m_JumpCount + 1);
        }
    }
    #endregion
}