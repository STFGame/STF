using Controls;
using UnityEngine;

namespace Actor
{
    /// <summary>ActorControl controls the Actor by passing in different values to the movement component.</summary>
    [RequireComponent(typeof(ActorMovement), typeof(ActorCombat), typeof(ActorSurvival))]
    [RequireComponent(typeof(ActorAnimation))]
    public class ActorControl : MonoBehaviour
    {
        public PlayerNumber playerNumber;

        private ActorMovement movement;
        private ActorCombat combat;
        private ActorSurvival survival;
        private new ActorAnimation animation;

        [HideInInspector] public Device device;

        private void Awake()
        {
            movement = GetComponent<ActorMovement>();
            combat = GetComponent<ActorCombat>();
            survival = GetComponent<ActorSurvival>();
            animation = GetComponent<ActorAnimation>();

            string name = Input.GetJoystickNames()[(int)playerNumber - 1];

            device = new Device(name, (int)playerNumber);
        }

        private void Update()
        {
            device.UpdateDevice();

            combat.Perform();
        }

        private void FixedUpdate()
        {
            Vector3 move = new Vector3(device.LeftStick.Horizontal, device.LeftStick.Vertical, 0f);

            movement.Perform(move, device.RightBumper.Trigger, device.RightBumper.Hold);
        }

        private void OnDrawGizmos() { }
    }
}
