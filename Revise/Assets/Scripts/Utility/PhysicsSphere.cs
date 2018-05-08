using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Utility
{
    [Serializable]
    public class PhysicsSphere
    {
        public enum Direction { Up, Down, Right, Left }

        [SerializeField] private Transform target = null;
        [SerializeField] private Direction direction = Direction.Up;
        [SerializeField] private float radius = 0.5f;
        [SerializeField] private float maxDistance = 10f;
        [SerializeField] private LayerMask layerMask = 0;
        [SerializeField] private Color color = Color.black;

        private Vector3 rayDirection = Vector3.down;

        public PhysicsSphere()
        {
            if (direction == Direction.Up)
                rayDirection = Vector3.up;
            else if (direction == Direction.Down)
                rayDirection = Vector3.down;
            else if (direction == Direction.Right)
                rayDirection = Vector3.right;
            else
                rayDirection = Vector3.left;
        }

        public bool SphereRay()
        {
            return Physics.CheckSphere(target.position + Vector3.down, radius, layerMask);
        }

        public void DrawGizmo()
        {
            if (target == null)
                return;

            Gizmos.color = color;
            Gizmos.DrawWireSphere(target.position - Vector3.down, radius);
            Gizmos.DrawLine(target.transform.position, Vector3.down);
        }
    }
}
