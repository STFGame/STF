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

        private void OnTriggerEnter(Collider other)
        {
            if (!isEnabled)
            {
                isHit = false;
                return;
            }

            Debug.Log("Hit " + gameObject.name + " " + other.gameObject.layer);
            isHit = true;
        }

        private void OnTriggerExit(Collider other)
        {
            if (!isEnabled)
                return;

            Debug.Log("Exit " + gameObject.name);
            isHit = false;
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