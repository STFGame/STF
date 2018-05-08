using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Boxes
{
    public abstract class Box : MonoBehaviour
    {
        [HideInInspector] public Transform parent;
        [HideInInspector] public BoxArea boxArea;
        [HideInInspector] public BoxType boxType;
        [HideInInspector] public ColliderType colliderType;
        [HideInInspector] public Color color;

        private void OnDrawGizmos()
        {
            if (parent == null || gameObject.layer == (int)Layer.PlayerDynamic)
                return;

            Gizmos.color = color;
            if (colliderType == ColliderType.Sphere)
            {
                SphereCollider sphereCollider = GetComponent<SphereCollider>();
                Gizmos.DrawWireSphere(parent.position + sphereCollider.center, sphereCollider.radius);
            }
            else if (colliderType == ColliderType.Box)
            {
                BoxCollider boxCollider = GetComponent<BoxCollider>();
                Gizmos.DrawWireCube(parent.position + boxCollider.center, boxCollider.size);
            }
        }
    }
}