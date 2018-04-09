using UnityEngine;

/* ACTOR MOVEMENT
 * Sean Ryan
 * April 5, 2018
 * 
 * Derives from the Movement class and allows the controller to control the Actor
 */

namespace Actor
{
    public class ActorMovement : Movement
    {
        //Overrides the Move method of the base class and moves the rigid body
        public override void Move(float direction)
        {
            if (Halt)
                return;

            if (!IsRotating)
            {
                Forward = new Vector2(direction, 0f) ;

                Vector2 forward = Forward * Time.deltaTime;
                rigidbody.AddForce((forward * acceleration), forceMode);
            }

            Rotate(direction);

            float animSpeed = Mathf.Abs(direction);
            Animation(IsDashing, animSpeed);
        }
    }
}
