using Movements;
using UnityEngine;

namespace Characters
{
    /// <summary>
    /// Class that is responsible for the movement of the character.
    /// </summary>
    [RequireComponent(typeof(Rigidbody), typeof(Animator), typeof(Collider))]
    public class Movement : MonoBehaviour, IGroundable
    {
        private float m_speed;
        private float m_acceleration;

        [Header("Movement Values")]
        [SerializeField] private ForceMode m_forceMode = ForceMode.VelocityChange;
        [SerializeField] private MoveVelocity[] m_moveSpeed = null;

        private Rigidbody m_rigidbody;

        //Used to help rotate the character in the correct direction
        private Vector3 m_startDirection = Vector3.right;

        //Values that are updated based on which movement mode the character is in;
        private float m_characterRotation = 0f;

        public bool IsGrounded { get; set; }

        public bool IsCrouching { get; private set; }
        public bool IsRegular { get; private set; }
        public bool IsDashing { get; private set; }

        public Vector2 Velocity { get; private set; }

        // Use this for initialization
        private void Awake()
        {
            m_rigidbody = GetComponent<Rigidbody>();

            IsGrounded = true;

            m_startDirection *= transform.forward.x;

            SortMoves();

            IsDashing = false;
            IsCrouching = false;
            IsRegular = true;
        }

        private void SortMoves()
        {
            for (int i = 0; i < m_moveSpeed.Length - 1; i++)
            {
                for (int j = i + 1; j < m_moveSpeed.Length; j++)
                {
                    if (m_moveSpeed[i].MovePriority < m_moveSpeed[j].MovePriority)
                    {
                        MoveVelocity moveVelocity = m_moveSpeed[i];
                        m_moveSpeed[i] = m_moveSpeed[j];
                        m_moveSpeed[j] = moveVelocity;
                    }
                }
            }
        }

        //Moves the character based on the direction, and the speed based on the MoveMode.
        public void Move(Vector2 direction)
        {
            int moveIndex = 0;
            for (int i = 0; i < m_moveSpeed.Length; i++)
            {
                MoveVelocity moveSpeed = m_moveSpeed[i];
                if (moveSpeed.Active(direction, IsGrounded))
                {
                    moveIndex = i;
                    break;
                }
            }

            if (IsDashing)
            {
                if (direction.x > 0f)
                    direction.x = 1f;
                else if (direction.x < 0f)
                    direction.x = -1f;
            }

            Vector3 velocity = m_moveSpeed[moveIndex].GetVelocity(direction, m_rigidbody.velocity, ref m_acceleration);

            IsRegular = (m_moveSpeed[moveIndex].MoveMode == MoveMode.Run && direction.x != 0f);
            IsCrouching = (m_moveSpeed[moveIndex].MoveMode == MoveMode.Crouch);
            IsDashing = (m_moveSpeed[moveIndex].MoveMode == MoveMode.Dash && direction.x != 0f);

            Velocity = velocity;
            Rotate(direction.x, m_moveSpeed[moveIndex].RotationSpeed);

            m_rigidbody.AddForce(velocity, m_forceMode);
        }

        //Sets the rotation of the character based on the direction entered
        private void Rotate(float direction, float rotationSpeed)
        {
            Quaternion startRotation = transform.localRotation;
            Quaternion endRotation = Quaternion.Euler(0f, m_characterRotation, 0f);

            if (!IsGrounded || IsCrouching)
            {
                transform.localRotation = Quaternion.RotateTowards(startRotation, endRotation, rotationSpeed);

                return;
            }

            if (direction != 0f)
            {
                Vector3 angle = Vector3.right * direction;

                m_characterRotation = Vector3.Angle(m_startDirection, angle) * -m_startDirection.x;
            }

            transform.localRotation = Quaternion.RotateTowards(startRotation, endRotation, rotationSpeed);
        }

        public void ResetMove()
        {
            IsDashing = false;
            IsCrouching = false;
            IsGrounded = true;
            Velocity = Vector3.zero;
        }
    }
}