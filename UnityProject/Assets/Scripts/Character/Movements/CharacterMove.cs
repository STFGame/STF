using Actions;
using Alerts;
using Movements;
using UnityEngine;

namespace Character
{
    /// <summary>
    /// Class that is responsible for the movement of the character.
    /// </summary>
    [RequireComponent(typeof(Rigidbody), typeof(Animator), typeof(Collider))]
    public class CharacterMove : Movement, IGroundable
    {
        #region Movement Variables
        [Header("Movement Values")]
        [SerializeField] private ForceMode m_ForceMode = ForceMode.VelocityChange;
        [SerializeField] private MoveVelocity[] m_MoveSpeeds = null;

        private Rigidbody m_Rigidbody;
        private Animator m_Animator;

        //Used to help rotate the character in the correct direction
        private Vector3 m_StartDirection = Vector3.right;

        //Values that are updated based on which movement mode the character is in;
        private float m_Rotation = 0f;

        private AlertValue m_Alert = AlertValue.None;

        public bool Grounded { get; set; }

        public bool Crouching { get; private set; }
        public bool Normal { get; private set; }
        public bool Dashing { get; private set; }

        public sealed override Vector2 Velocity { get; protected set; }
        #endregion

        #region Load
        // Use this for initialization
        private void Awake()
        {
            m_Rigidbody = GetComponent<Rigidbody>();
            m_Animator = GetComponent<Animator>();

            Grounded = true;

            m_StartDirection *= transform.forward.x;

            SortMoves();
        }

        private void SortMoves()
        {
            for (int i = 0; i < m_MoveSpeeds.Length - 1; i++)
            {
                for (int j = i + 1; j < m_MoveSpeeds.Length; j++)
                {
                    if (m_MoveSpeeds[i].MovePriority < m_MoveSpeeds[j].MovePriority)
                    {
                        MoveVelocity moveVelocity = m_MoveSpeeds[i];
                        m_MoveSpeeds[i] = m_MoveSpeeds[j];
                        m_MoveSpeeds[j] = moveVelocity;
                    }
                }
            }
        }
        #endregion

        #region Updates
        //Moves the character based on the direction, and the speed based on the MoveMode.
        public override void Move(Vector2 direction)
        {
            if (Prevent())
                return;

            int moveIndex = 0;
            for (int i = 0; i < m_MoveSpeeds.Length; i++)
            {
                MoveVelocity moveSpeed = m_MoveSpeeds[i];
                if (moveSpeed.Active(direction))
                {
                    moveIndex = i;
                    break;
                }
            }

            if (Dashing)
            {
                if (direction.x > 0f)
                    direction.x = 1f;
                else if (direction.x < 0f)
                    direction.x = -1f;
            }

            float rotationSpeed = 0f;
            Vector3 velocity = m_MoveSpeeds[moveIndex].GetVelocity(direction, m_Rigidbody.velocity, ref m_Acceleration);
            Quaternion rotation = m_MoveSpeeds[moveIndex].GetRotation(m_StartDirection, direction.x, out rotationSpeed);

            Normal = (m_MoveSpeeds[moveIndex].MoveMode == Movements.MoveMode.Run && direction.x != 0f);
            Crouching = (m_MoveSpeeds[moveIndex].MoveMode == Movements.MoveMode.Crouch && direction.x != 0f);
            Dashing = (m_MoveSpeeds[moveIndex].MoveMode == Movements.MoveMode.Dash && direction.x != 0f);

            Velocity = velocity;
            Rotate(direction.x, m_MoveSpeeds[moveIndex].RotationSpeed);

            AnimateMove(direction);

            m_Rigidbody.AddForce(Velocity, m_ForceMode);
        }

        //Sets the rotation of the character based on the direction entered
        public void Rotate(float direction, float rotationSpeed)
        {
            Quaternion startRotation = transform.localRotation;
            Quaternion endRotation = Quaternion.Euler(0f, m_Rotation, 0f);

            if (!Grounded || Crouching)
            {
                transform.localRotation = Quaternion.RotateTowards(startRotation, endRotation, rotationSpeed);

                return;
            }

            if (direction != 0f)
            {
                Vector3 angle = Vector3.right * direction;

                m_Rotation = Vector3.Angle(m_StartDirection, angle) * -m_StartDirection.x;
            }

            transform.localRotation = Quaternion.RotateTowards(startRotation, endRotation, rotationSpeed);
        }

        private bool Prevent()
        {
            return (m_Alert == AlertValue.Dead || m_Alert == AlertValue.Shielding ||
                    m_Alert == AlertValue.Stunned || m_Alert == AlertValue.Attacking);
        }
        #endregion

        #region Visual FX and Animation
        //Function that animates the all of the characters movements.
        public void AnimateMove(Vector2 direction)
        {
            if (!m_Animator)
                return;

            float speed = Mathf.Abs(direction.x);
            float crouchSpeed = direction.x * transform.forward.x;
            float crouchFactor = direction.y;

            m_Animator.SetFloat("Speed", speed, 0.02f, Time.deltaTime);
            m_Animator.SetFloat("Crouch Speed", crouchSpeed, 0.02f, Time.deltaTime);
            m_Animator.SetFloat("Crouch Factor", direction.y, 0.02f, Time.deltaTime);
            m_Animator.SetBool("Crouching", Crouching);
            m_Animator.SetBool("Dashing", Dashing);
        }

        #endregion

        #region Alerts
        public override void Inform(AlertValue alert)
        {
            Debug.Log(alert);
            m_Alert = alert;
        }
        #endregion
    }
}