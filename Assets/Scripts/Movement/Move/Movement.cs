using Actor.Animation;
using UnityEngine;

namespace Actor
{
    /// <summary>
    /// An abstract class that contains common variables and functionality <para/>
    /// that is associated with movement.
    /// </summary>
    [RequireComponent(typeof(Rigidbody))]
    public abstract class Movement : MonoBehaviour
    {
        [SerializeField] protected ForceMode forceMode;                 //Sets which ForceMode to apply to the Rigidbody of the Actor
        [SerializeField] protected float acceleration;                  //The acceleration of the Actor
        [SerializeField] protected float deceleration;                  //The drag of the Rigidbody of the Actor
        [SerializeField] protected float rotationSpeed;                 //The speed that the actor rotates at

        protected new Rigidbody rigidbody;                              //Rigidbody for getting the component of the Actor
        protected new Transform transform;                              //Transform of the GameObject attached to this component
        [SerializeField] protected new MovementAnimation animation;     //The movement animations

        public Vector2 Forward { get; protected set; }                  //A Vector2 that is the forward movement of the Actor
        public float Rotation { get; protected set; }                   //The rotation of the Actor
        public bool IsRotating { get; protected set; }                  //Returns true while the Actor is rotating/turning
        public bool Halt { get; set; }                                  //Halts the Actor's movement completely
        public bool IsDashing { get; set; }                             //Property for checking if the Actor is dashing

        protected virtual void Awake()
        {
            rigidbody = GetComponent<Rigidbody>();
            transform = GetComponent<Transform>();
            rigidbody.drag = deceleration;

            animation.Init(this);

            IsRotating = false;
            Rotation = 0f;
        }

        //An abstract method for moving the Actor
        public abstract void Move(float direction);

        //A virtual method for rotating the Actor
        protected virtual void Rotate(float direction)
        {
            Rotation = (direction > 0.01f) ? 0f : (direction < -0.01f) ? -180f : Rotation;
            Quaternion endRotation = Quaternion.Euler(0f, Rotation, 0f);

            transform.localRotation = Quaternion.RotateTowards(transform.localRotation, endRotation, rotationSpeed);

            IsRotating = (transform.localRotation.eulerAngles.y != -Rotation);
        }

        protected virtual void Animation(bool value, float direction)
        {
            animation.SetDash(value);
            animation.SetSpeed(direction);
        }
    }
}
