using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Character.Collisions
{
    [RequireComponent(typeof(CapsuleCollider))]
    class Collides : MonoBehaviour
    {
        bool isGrounded = true;
        public bool IsGrounded { get { return isGrounded; } }

        public event Action<bool> OnGrounded;

        List<Collider> colliders = new List<Collider>();

        #region Collision Checks
        private void OnCollisionEnter(Collision collision)
        {
            ContactPoint[] contactPoints = collision.contacts;

            bool surfaceIsValid = false;

            for (int i = 0; i < contactPoints.Length; i++)
            {
                if (Vector3.Dot(contactPoints[i].normal, Vector3.up) > 0.5f)
                    surfaceIsValid = true;
            }

            if (surfaceIsValid)
            {
                Grounded(true);
                if (!colliders.Contains(collision.collider))
                    colliders.Add(collision.collider);
            }
            else
            {
                if (colliders.Contains(collision.collider))
                    colliders.Remove(collision.collider);
                if (colliders.Count == 0)
                    Grounded(false);
            }
        }

        private void OnCollisionExit(Collision collision)
        {
            if (colliders.Contains(collision.collider))
                colliders.Remove(collision.collider);
            if (colliders.Count == 0)
                Grounded(false);
        }
        #endregion

        public void Grounded(bool ground)
        {
            if (ground == isGrounded)
                return;

            isGrounded = ground;

            if (OnGrounded != null)
                OnGrounded(isGrounded);
        }
    }
}
