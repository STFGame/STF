using System.Collections.Generic;
using UnityEngine;
using Utility;

namespace Actor.Bubbles
{
    public enum BubbleType { None, HurtBubble, HitBubble }
    public enum ColliderType { None, SphereCollider, BoxCollider }

    public class Bubble : MonoBehaviour
    {
        public Transform parent = null;
        public Color color = Color.green;
        public ColliderType colliderType = ColliderType.None;
        public BodyArea bodyArea = BodyArea.None;
        public BubbleType bubbleType = BubbleType.None;

        public bool isEnabled = true;
        private bool isHit = false;

        public delegate void IntersectChange(Collider other);
        public event IntersectChange IntersectEvent;

        private void OnTriggerEnter(Collider other)
        {
            if (!isEnabled)
            {
                UpdateIntersect(false, null);
                return;
            }

            UpdateIntersect(true, other);
        }

        private void OnTriggerExit(Collider other)
        {
            if (!isEnabled)
                return;

            UpdateIntersect(false, null);
        }

        private void UpdateIntersect(bool isHit, Collider other)
        {
            if (this.isHit == isHit)
                return;

            this.isHit = isHit;

            if (IntersectEvent != null)
                IntersectEvent(other);
        }

        private void OnDrawGizmos()
        {
            if (parent == null || !isEnabled)
                return;

            Matrix4x4 transformMatrix = Matrix4x4.TRS(transform.position, transform.rotation, transform.lossyScale);
            Gizmos.matrix = transformMatrix;

            Gizmos.color = color;
            if (colliderType == ColliderType.BoxCollider)
            {
                Vector3 size = GetComponent<BoxCollider>().size;
                Vector3 offset = GetComponent<BoxCollider>().center;
                Gizmos.DrawWireCube(offset, size * -1);
            }
            else if (colliderType == ColliderType.SphereCollider)
            {
                float radius = GetComponent<SphereCollider>().radius;
                Vector3 offset = GetComponent<SphereCollider>().center;
                Gizmos.DrawWireSphere(offset, radius);
            }
        }
    }
}