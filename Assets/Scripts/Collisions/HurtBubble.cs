using UnityEngine;
using Utility.Enums;
using Utility.Identifer;

/* HURT BUBBLE
 * Sean Ryan
 * April 5, 2018
 * 
 * HurtBubble is component that is attached to the different areas that can be hurt on the Actor
 * It derives from BubbleBehaviour and is responsible for identifying if the player gets hurt
 */ 

namespace Actor
{
    public class HurtBubble : Bubble
    {
        private void Awake()
        {
            Type = BubbleType.HurtBubble;
        }

        protected override void OnTriggerEnter(Collider other)
        {
            if(other.CompareTag(Tag.HitBubble) && other.gameObject.layer != gameObject.layer)
                Debug.Log("Entered HurtBubble");
        }

        protected override void OnTriggerExit(Collider other)
        {
            Debug.Log("Exited HurtBubble");
        }
    }
}
