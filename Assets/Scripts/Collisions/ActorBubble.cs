using Actor.Collisions;
using System.Collections.Generic;
using UnityEngine;
using Utility.Enums;

namespace Actor
{
    /// <summary>
    /// ActorBubble contains information that is related to the different collisions<para/>
    /// an Actor can experience.
    /// </summary>
    public class ActorBubble : MonoBehaviour
    {
        public List<GameObject> bubbles = new List<GameObject>();                    //List of GameObjects that add different collision bubbles.
        public List<BubbleType> bubbleType = new List<BubbleType>();
        Groundbox groundBox = new Groundbox();

        public delegate void GroundEvent(bool value);
        public event GroundEvent OnGround;

        [SerializeField] private bool onGround = true;

        private void OnCollisionEnter(Collision collision)
        {
            groundBox.OnCollisionEnter(collision);
            Update_GroundEvent(groundBox.OnGround);
        }

        private void OnCollisionExit(Collision collision)
        {
            groundBox.OnCollisionExit(collision);
            Update_GroundEvent(groundBox.OnGround);
        }

        private void Update_GroundEvent(bool value)
        {
            if (value == onGround)
                return;

            onGround = value;

            if (OnGround != null)
                OnGround(value);
        }
    }
}
