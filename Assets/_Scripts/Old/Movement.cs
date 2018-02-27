using System.Collections.Generic;
using UnityEngine;
using System.Collections;
using System;
using Player.CC;
using Old.Animation;

namespace Old.Movement
{
    #region Movement Data -- struct
    [Serializable]
    public struct MovementData
    {
        public float m_WalkMultiplier;
        public float m_RunMultiplier;
        public float m_SprintMultiplier;
        public float m_JumpMultiplier;

        public int m_JumpCount;

        public MovementData(float walk, float run, float sprint, float jumpMultiplier, int jumpCount)
        {
            m_WalkMultiplier = walk;
            m_RunMultiplier = run;
            m_SprintMultiplier = sprint;
            m_JumpMultiplier = jumpMultiplier;
            m_JumpCount = jumpCount;
        }
    }
    #endregion

    #region Movement Inherit MonoBehaviour -- class
    [RequireComponent(typeof(Animator))]
    [RequireComponent(typeof(CapsuleCollider))]
    [RequireComponent(typeof(Rigidbody))]
    public class Movement : MonoBehaviour
    {
        public MovementData m_MoveSettings = new MovementData(10f, 15f, 20f, 35f, 2);

        public Controls controls;

        #region Components
        private Rigidbody m_Rigidbody;
        private CapsuleCollider m_Collider;

        private Animator m_Animator;
        private Animate m_Animate;
        #endregion

        #region Input
        InputManager m_Input = new InputManager();
        #endregion

        #region Movement Input
        private float m_Rotation;

        //These are modifiable movement values 
        [SerializeField] private float m_DashMultiplier = 20f;
        [SerializeField] private float m_RunMultiplier = 15f;
        [SerializeField] private float m_WalkMultiplier = 10f;
        [SerializeField] private float m_JumpMulitiplier = 10f;
        [SerializeField] private int m_JumpCount = 2;

        #endregion

        #region Collision Data
        private List<Collider> m_Collisions = new List<Collider>();
        private bool m_IsGrounded;

        #endregion

        private void OnEnable()
        {
            m_Animator = this.GetComponent<Animator>();
            m_Rigidbody = this.GetComponent<Rigidbody>();
            m_Collider = this.GetComponent<CapsuleCollider>();

            m_Animate = new Animate(m_Animator);
        }

        private void Start() { }

        private void Update()
        {
            m_Animate.UpdateAnime(m_Input, m_IsGrounded);

            if (m_IsGrounded)
                m_JumpCount = 2;

            if (Input.GetKeyDown(KeyCode.Joystick1Button1))
            {
                if (m_IsGrounded || m_JumpCount > 0)
                {
                    m_Rigidbody.useGravity = false;

                    if (m_JumpCount > 1)
                        StartCoroutine(JumpRoutine());
                    else
                        m_Rigidbody.AddForce(Vector3.up * m_JumpMulitiplier, ForceMode.VelocityChange);
                    m_JumpCount--;
                }
            }

            Rotation();
        }

        private IEnumerator JumpRoutine()
        {
            yield return new WaitForSeconds(0.2f);
            m_Rigidbody.AddForce(Vector3.up * m_JumpMulitiplier, ForceMode.VelocityChange);
        }

        private void FixedUpdate()
        {
            Move();
            //if (Input.GetKey(KeyCode.Joystick1Button1))
            //{
            //if (_jumpTimeCounter > 0f)
            //{
            //m_Rigidbody.AddForce(Vector3.up * m_JumpMulitiplier, ForceMode.Impulse);
            //_jumpTimeCounter -= Time.deltaTime;
            //m_Rigidbody.useGravity = false;
            //}
            //else
            //m_Rigidbody.useGravity = true;
            //}
            //if (Input.GetKeyUp(KeyCode.Joystick1Button1) || _jumpTimer < 0f)
            //{
            //_jumpTimeCounter = 0f;
            //m_Rigidbody.useGravity = true;
            //}
            if (!m_IsGrounded)
                m_Rigidbody.AddForce(Vector3.down * 50f);
            //Move();
        }

        private void Move()
        {
            Vector3 move = new Vector3(m_Input.Joystick.x, 0f, 0f);

            if (m_IsGrounded)
            {
                if (m_Input.Dash)
                {
                    move.x = (move.x < 0f) ? Mathf.Clamp(move.x, -1f, -1f) : Mathf.Clamp(move.x, 1f, 1f);
                    m_Rigidbody.AddForce((move * m_DashMultiplier) * Time.deltaTime, ForceMode.Impulse);
                }
                else
                {
                    if (move.x < 0.5f && move.x > -0.5f)
                    {
                        m_Rigidbody.AddForce((move * m_WalkMultiplier) * Time.deltaTime, ForceMode.Impulse);
                    }
                    else if (move.x > 0.5f || move.x < -0.5f)
                    {
                        m_Rigidbody.AddForce((move * m_RunMultiplier) * Time.deltaTime, ForceMode.Impulse);
                    }
                }
            }
            else
            {
                //move.x = Mathf.Clamp(move.x, -0.8f, 0.8f);
                m_Rigidbody.AddForce((move * m_DashMultiplier) * Time.deltaTime, ForceMode.Impulse);
            }
        }

        private void Rotation()
        {
            if (m_IsGrounded)
            {
                m_Rotation = (m_Input.Joystick.x < 0) ? -1 * 90f : (m_Input.Joystick.x > 0) ? 1 * 90f : m_Rotation;
                this.transform.eulerAngles = new Vector3(0f, m_Rotation, 0f);
            }
        }

        private void Jump()
        {
            if (m_IsGrounded || m_JumpCount > 1)
            {
                if (m_Animate.CurrentState.fullPathHash == AnimaState.JumpState)
                {
                    if (!m_Animator.IsInTransition(0))
                    {
                        m_Rigidbody.AddForce(Vector3.up * m_JumpMulitiplier * Time.deltaTime, ForceMode.Impulse);
                        m_JumpCount--;
                    }
                }
            }
        }

        #region Collision Data
        private void OnCollisionEnter(Collision collision)
        {
            /********Code for checking if the player is grounded*********/
            ContactPoint[] contactPoints = collision.contacts;
            bool validSurface = false;
            for (int i = 0; i < collision.contacts.Length; i++)
            {
                if (Vector3.Dot(contactPoints[i].normal, Vector3.up) > 0.5f)
                    validSurface = true;
            }
            if (validSurface)
            {
                m_IsGrounded = true;
                if (!m_Collisions.Contains(collision.collider))
                    m_Collisions.Add(collision.collider);
            }
            else
            {
                if (m_Collisions.Contains(collision.collider))
                    m_Collisions.Remove(collision.collider);
                if (m_Collisions.Count == 0)
                    m_IsGrounded = false;
            }
            /*************************************************************/
        }

        private void OnCollisionExit(Collision collision)
        {
            if (m_Collisions.Contains(collision.collider))
                m_Collisions.Remove(collision.collider);
            if (m_Collisions.Count == 0)
                m_IsGrounded = false;
        }
        #endregion

        private void OnGUI()
        {
            GUI.Box(new Rect(Screen.width - 260f, 10f, 250f, 150f), "Player Box");
            GUI.Label(new Rect(Screen.width - 245f, 30f, 250f, 30f), "Player Horizontal: " + m_Input.Joystick.x);
            GUI.Label(new Rect(Screen.width - 245f, 50f, 250f, 30f), "Player Dash: " + m_Input.Dash);
            GUI.Label(new Rect(Screen.width - 245f, 70f, 250f, 30f), "Player Pivot: " + m_Input.Pivot);
        }
    }
    #endregion
}