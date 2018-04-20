using UnityEngine;

namespace Actor
{
    /// <summary>ActorControl controls the Actor by passing in different values to the movement component.</summary>
    [RequireComponent(typeof(ActorMovement), typeof(ActorCombat), typeof(ActorSurvival))]
    public class ActorControl : MonoBehaviour
    {
        private ActorMovement movement;
        private ActorCombat combat;
        private ActorSurvival survival;
        private new ActorAnimation animation;


        Vector3 direction;
        bool click;
        bool hold1;
        bool hold2;
        bool hold3;

        private void Awake()
        {
            movement = GetComponent<ActorMovement>();
            combat = GetComponent<ActorCombat>();
            survival = GetComponent<ActorSurvival>();
            animation = GetComponent<ActorAnimation>();
        }

        private void Update()
        {
            direction = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical"));

            click = Input.GetKeyDown(KeyCode.Joystick1Button1);
            hold1 = Input.GetKey(KeyCode.Joystick1Button1);
            hold2 = Input.GetKey(KeyCode.Joystick1Button0);
            hold3 = Input.GetKeyDown(KeyCode.Joystick1Button3);


            if (combat != null)
                combat.Perform(hold2, hold3);

            if (survival != null)
                ;

            PlayAnimations();
        }

        private void FixedUpdate()
        {
            if (movement != null)
                movement.Perform(direction, click, hold1);
        }

        private void PlayAnimations()
        {
            print(Mathf.Abs(direction.x));

            animation.PlayJumpAnim(click, movement.Jumps.Apex, movement.OnGround, direction.x);
            animation.PlayMoveAnim(direction.x, direction.z);
        }

        private void OnDrawGizmos() { }
    }
}
