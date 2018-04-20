using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Actor.Bubbles
{
    [Serializable]
    public sealed class GroundBubble
    {
        [SerializeField] private Color color;
        [SerializeField] private Transform parent;
        [SerializeField] private Vector3 offset;
        [SerializeField] private float radius;
        [SerializeField] private LayerMask layerMask;

        private float updatedHeight;

        public GroundBubble()
        {
            color = Color.yellow;
        }

        public void Intersection(float currentHeight, ref bool onGround)
        {
            if (parent == null)
                return;

            if (updatedHeight == currentHeight)
                return;

            updatedHeight = currentHeight;
            onGround = Physics.CheckSphere(parent.position + offset, radius, layerMask);
        }

        public void DrawGizmo()
        {
            if (parent == null)
                return;

            Gizmos.color = color;
            Gizmos.DrawWireSphere(parent.position + offset, radius);
        }
    }
}
