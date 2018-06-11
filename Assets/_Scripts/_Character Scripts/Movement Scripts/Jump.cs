using Actions;
using System.Collections;
using UnityEngine;

namespace Characters
{
    /// <summary>
    /// Handles the jumping and falling of the character.
    /// </summary>
    [RequireComponent(typeof(Rigidbody), typeof(Animator), typeof(Collider))]
    public class Jump : MonoBehaviour, IGroundable
    {
        [Header("Jump Variables")]
        [SerializeField] [Range(0f, 200f)] private float m_jumpHeight;
        [SerializeField] [Range(0f, 200f)] private float m_descentSpeed;
        [SerializeField] [Range(0f, 5f)] private float m_jumpDelay;
        [SerializeField] private int m_maxJumps = 2;

        private float m_currentCrestPeak = 0f;
        private int m_jumpsRemaining;

        [Header("Jump Controls")]
        [SerializeField] private ActionRange m_fallAction = new ActionRange();
        [SerializeField] private ActionHold m_jumpAction = new ActionHold();

        //Components to be cached
        private Rigidbody m_rigidbody;
        private Transform m_transform;
        private Animator m_jumpAnimator;
        private Gravity m_gravity;

        public int JumpsRemaining { get { return m_jumpsRemaining; } }
        public bool IsGrounded { get; set; }
        public bool IsFalling { get; private set; }

        private void Awake()
        {
            m_rigidbody = GetComponent<Rigidbody>();
            m_transform = GetComponent<Transform>();
            m_jumpAnimator = GetComponent<Animator>();
            m_gravity = GetComponent<Gravity>();

            m_currentCrestPeak = m_rigidbody.position.y;
            m_jumpsRemaining = m_maxJumps;
        }

        //Makes the character jump for as long as the jumpHold is held
        public void Execute(Vector2 direction, bool jump, bool jumpHold)
        {
            jumpHold = m_jumpAction.ActionSuccess(jumpHold);

            if (IsGrounded)
            {
                m_currentCrestPeak = m_transform.position.y;
                m_jumpsRemaining = m_maxJumps;
            }

            if (m_fallAction.ActionSuccess(direction))
                m_rigidbody.AddForce(Vector3.down * m_descentSpeed);

            if (jump && m_jumpsRemaining > 0)
            {
                float velocity = Mathf.Sqrt(2f * m_gravity.Gravitation * m_jumpHeight);
                StartCoroutine(JumpDelay(velocity));
            }

            if (m_gravity)
                m_gravity.DecreaseGravity(jumpHold);
        }

        //Coroutine for the delay of the jump
        private IEnumerator JumpDelay(float velocity)
        {
            if (!IsGrounded)
            {
                m_rigidbody.velocity = new Vector3(m_rigidbody.velocity.x, velocity, 0f);
                m_jumpsRemaining--;
                yield break;
            }

            yield return new WaitForSeconds(m_jumpDelay);
            m_rigidbody.velocity = new Vector3(m_rigidbody.velocity.x, velocity, 0f);

            yield return new WaitUntil(() => IsGrounded == false);
            m_jumpsRemaining--;
        }

        public void ResetJump()
        {
            IsFalling = false;
            IsGrounded = true;
        }

        //Checks to see whether the character is falling based on the comparision of their current height and their last height
        public void Fall(float currentHeight)
        {
            if (currentHeight >= m_currentCrestPeak)
            {
                m_currentCrestPeak = currentHeight;
                IsFalling = false;
                return;
            }

            m_currentCrestPeak = currentHeight + 0.5f;
            IsFalling = true;
        }
    }
}