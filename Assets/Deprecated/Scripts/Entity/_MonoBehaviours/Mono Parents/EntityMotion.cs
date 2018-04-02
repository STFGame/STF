using UnityEngine;
using Entity.Components;
using Entity.Motions;
using Controller.Mechanism;

/* MOTION
 * Sean Ryan
 * March 17, 2018
 * 
 * This class is responsible for the movement behaviour of the character.
 * Movement includes physical movement, as well as animations.
 */

namespace Entity
{
    #region Required Components
    [RequireComponent(typeof(Rigidbody))]
    [RequireComponent(typeof(Transform))]
    [RequireComponent(typeof(Animator))]
    [RequireComponent(typeof(IControl))]
    #endregion
    public class EntityMotion : Entity
    {
        [SerializeField] private ForceMode movementForce;

        [SerializeField] private Movement movement = new Movement();
        [SerializeField] private Crouch crouch = new Crouch();
        [SerializeField] private Dash dash = new Dash();

        private Vector2 move = new Vector2();
        private float rotation = 0f;

        private bool isTurning = false;

        //Awake is called when the script instance is being loaded.
        private new void Awake()
        {
            base.Awake();

            movement.Initialize(unit, control);
            crouch.Initialize(unit, control);
            dash.Initialize(unit, control);
        }

        // Update is called once per frame.
        private void Update()
        {
            movement.OnUpdate();
            crouch.OnUpdate();
            dash.OnUpdate();

            //activity.OnUpdate();
        }

        //FixedUpdate is called every fixed framerate frame.
        private void FixedUpdate()
        {
            MotionUpdate();
        }

        private void MotionUpdate()
        {
            if (dash.IsDashing)
            {
                SetMotion(dash);
                SetRotation(dash);
            }
            else if(crouch.IsCrouching)
            {
                SetMotion(crouch);
                SetRotation(crouch);
            }
            else
            {
                SetMotion(movement);
                SetRotation(movement);
            }
        }

        private void SetMotion(Movement movementType)
        {
            float speed = (onGround) ? movementType.Speed : movementType.AerialSpeed;
            move = (Vector2.right * speed * control.Lever.Horizontal) * Time.deltaTime;
            unit.Rigidbody.AddForce(move, movementForce);
        }

        private void SetRotation(Movement movementType)
        {
            if (!onGround && !isTurning)
                return;

            float rotationSpeed = movementType.RotationSpeed;
            Quaternion endRotation = Rotate(ref rotation);
            unit.Transform.localRotation = Quaternion.RotateTowards(unit.Transform.localRotation, endRotation, rotationSpeed);

            isTurning = (unit.Transform.localRotation.eulerAngles.y != rotation);
        }

        private Quaternion Rotate(ref float rotation)
        {
            rotation = (control.Lever.Horizontal > 0.1f) ? 0f : (control.Lever.Horizontal < -0.1f) ? -180f : rotation;
            Quaternion quaternionRotation = Quaternion.Euler(0f, rotation, 0f);
            return quaternionRotation;
        }
    }
}
