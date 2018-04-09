using UnityEngine;
using Utility.Enums;
using Utility.Identifer;

/* HIT BUBBLE
 * Sean Ryan
 * April 5, 2018
 * 
 * HitBubble is a component that is attached to different hit areas on the Actor
 * It derives from BubbleBehaviour and is responsible for identifying if attacks connect
 */ 

namespace Actor
{
    public class HitBubble : Bubble
    {
        private void Awake()
        {
            Type = BubbleType.HitBubble;
        }

        protected override void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag(Tag.HurtBubble) && other.gameObject.layer != gameObject.layer)
                Debug.Log("Entered HitBubble");
        }

        protected override void OnTriggerExit(Collider other)
        {
            Debug.Log("Exited HitBubble");
        }
    }
}
