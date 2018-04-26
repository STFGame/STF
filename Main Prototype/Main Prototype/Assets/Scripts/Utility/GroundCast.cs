using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Utility
{
    [Serializable]
    public class GroundCast
    {
        [SerializeField] private Transform parent = null;
        [SerializeField] private Vector3 offset = Vector3.zero;
        [SerializeField] private float radius = 0.5f;
        [SerializeField] private LayerMask layerMask = 0;
        [SerializeField] private Color color = Color.black;

        private float previousHeight = 0f;

        public void GroundCheck(float currentHeight, ref bool onGround)
        {
            if (currentHeight == previousHeight || parent == null)
                return;

            previousHeight = currentHeight;

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
