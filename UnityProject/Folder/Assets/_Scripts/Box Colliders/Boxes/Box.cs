using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Boxes
{
    /// <summary>
    /// Abstract class that contains general information related to boxes.
    /// </summary>
    public abstract class Box : MonoBehaviour
    {
        #region Box Variables
        //The parent of the box
        [HideInInspector] public Transform parent;

        //The area of the character that the box wants to be identified as
        [HideInInspector] public BoxArea boxArea;

        //The type of box that it is
        [HideInInspector] public BoxType boxType;

        //The collider shape
        [HideInInspector] public ColliderType colliderType;

        //The color of the debug drawing
        [HideInInspector] public Color color;

        public bool active = true;
        #endregion

        #region Debug Drawing
        private void OnDrawGizmos()
        {
            if (parent == null || !active)
                return;

            Matrix4x4 transformMatrix = Matrix4x4.TRS(transform.position, transform.rotation, transform.lossyScale);
            Gizmos.matrix = transformMatrix;

            Gizmos.color = color;
            if (colliderType == ColliderType.Sphere)
            {
                SphereCollider sphereCollider = GetComponent<SphereCollider>();
                Gizmos.DrawWireSphere(sphereCollider.center, sphereCollider.radius);
            }
            else if (colliderType == ColliderType.Box)
            {
                BoxCollider boxCollider = GetComponent<BoxCollider>();
                Gizmos.DrawWireCube(boxCollider.center, boxCollider.size);
            }
        }
        #endregion
    }
}