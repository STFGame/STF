using UnityEngine;
using Player.CC;
using System.Collections.Generic;

/****This class is responsible for the movement of the player****/
namespace Player.Movement
{
    #region Component Requirements
    [RequireComponent(typeof(Rigidbody))]
    [RequireComponent(typeof(CapsuleCollider))]
    #endregion
    public class Movement : MonoBehaviour
    {
        #region Movement Settings
        [Header("Movement Settings")]
        [SerializeField] float walkSpeed;
        [SerializeField] float runSpeed;
        [SerializeField] float sprintSpeed;

        [SerializeField] float jumpHeight;
        [SerializeField] int jumpCount;

        float rotation = 0f;
        bool isGrounded = true;
        int maxJumps;
        #endregion

        #region Controls
        [Header("Controls")]
        public Controls Controls;

        Vector2 joystick;
        bool moving;
        bool jump;
        bool rapid;
        bool attackDown;
        #endregion

        #region Components of GameObject
        private Rigidbody rigidBody;
        private CapsuleCollider capsuleCollider;
        #endregion

        List<Collider> collisionList = new List<Collider>();

        #region MonoBehaviours
        void Start()
        {
            rigidBody = this.GetComponent<Rigidbody>();
            capsuleCollider = this.GetComponent<CapsuleCollider>();

            maxJumps = jumpCount;
        }

        // Update is called once per frame, so it is used to capture input
        void Update()
        {
            joystick = Controls.Joystick.JStick();
            moving = (joystick.x > 0f || joystick.x < 0f) ? true : false;
            rapid = Controls.Joystick.Rapid(0.02f);
            jump = Input.GetKeyDown(KeyCode.Joystick1Button1);

            if (!isGrounded)
            {
                if (Input.GetKeyDown(KeyCode.Joystick1Button0) && joystick.y < 0f)
                    attackDown = true;
                else
                    attackDown = false;
            }
        }

        //This is for anything that requires an update in the physics
        private void FixedUpdate()
        {
            if (jump)
                Jump();

            if (moving)
                Move(rapid);

            if (attackDown && !isGrounded)
                rigidBody.AddForce(Vector3.down * 100f, ForceMode.Impulse);
        }

        #region Collisions
        private void OnCollisionEnter(Collision collision)
        {
            #region Check Grounded Collisions
            ContactPoint[] contactPoints = collision.contacts;
            bool isValid = false;
            for (int i = 0; i < collision.contacts.Length; i++)
            {
                if (Vector3.Dot(contactPoints[i].normal, Vector2.up) > 0.5f)
                    isValid = true;
            }
            if (isValid)
            {
                isGrounded = true;
                if (!collisionList.Contains(collision.collider))
                    collisionList.Add(collision.collider);
            }
            else
            {
                if (collisionList.Contains(collision.collider))
                    collisionList.Remove(collision.collider);
                if (collisionList.Count == 0)
                    isGrounded = false;
            }
            #endregion

            if (collision.collider.tag == "Player")
                rigidBody.velocity = new Vector2(-10f, 40f);
        }

        private void OnCollisionExit(Collision collision)
        {
            if (collisionList.Contains(collision.collider))
                collisionList.Remove(collision.collider);
            if (collisionList.Count == 0)
                isGrounded = false;
        }
        #endregion

        private void OnGUI()
        {
            GUI.Box(new Rect(Screen.width - 260f, 10f, 250f, 150f), "Movement Box");
            GUI.Label(new Rect(Screen.width - 245f, 30f, 250f, 30f), "Joystick X-Axis: " + joystick.x);
            GUI.Label(new Rect(Screen.width - 245f, 50f, 250f, 30f), "Joystick Y-Axis: " + joystick.y);
        }

        #endregion

        #region Movement Methods
        public void Move(bool rapid)
        {
            //This sets the rotation of the rigidbody based on the direction the last x axis movement
            rotation = ((joystick.x > 0.0f) ? 1.0f * 90f : (joystick.x < 0.0f) ? -1.0f * 90f : rotation);
            Quaternion yRot = Quaternion.Euler(0f, rotation, 0f);
            rigidBody.MoveRotation(yRot);

            //Add force to the rigidbody and adds speed depending whether the player moved the stick fast or not
            float speed = (rapid) ? sprintSpeed : walkSpeed;
            Vector3 move = new Vector2(joystick.x, 0.0f) * speed;
            rigidBody.AddForce(move * Time.deltaTime, ForceMode.Impulse);
        }

        public void Jump()
        {
            //If the player is grounded, resets the jumpCount back to the max value
            if (isGrounded)
                jumpCount = maxJumps;

            //Determines whether the rigidbody can jump based on if is grounded or has any jumps left. Also decrements the jumpCount
            if (isGrounded || jumpCount > 0)
            {
                rigidBody.velocity = Vector2.up * jumpHeight;
                jumpCount--;
            }
        }
        #endregion
    }
}