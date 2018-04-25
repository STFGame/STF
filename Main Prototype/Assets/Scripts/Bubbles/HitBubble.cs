using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Actor.Bubbles
{
    public sealed class HitBubble : Bubble
    {
        [HideInInspector] public float damage;

        protected override void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("HurtBubble"))
                Debug.Log("HIT!");
        }

        protected override void OnTriggerExit(Collider other)
        {
        }
    }
}