using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Actor.Collisions
{
    [Serializable]
    public class Groundbox
    {
        [SerializeField] private float normalPoint = 0.5f;

        private List<Collider> groundList = new List<Collider>();

        public bool OnGround { get; private set; }

        public void OnCollisionEnter(Collision collision)
        {
            ContactPoint[] contactPoints = collision.contacts;

            bool isSurfaceValid = false;

            for (int i = 0; i < contactPoints.Length; i++)
            {
                if (Vector2.Dot(contactPoints[i].normal, Vector2.up) > normalPoint)
                    isSurfaceValid = true;
            }

            if (isSurfaceValid)
            {
                if (!groundList.Contains(collision.collider))
                    groundList.Add(collision.collider);
                OnGround = true;
            }
            else
            {
                if (groundList.Contains(collision.collider))
                    groundList.Remove(collision.collider);
                if (groundList.Count == 0)
                    OnGround = false;
            }
        }

        public void OnCollisionExit(Collision collision)
        {
            if (groundList.Contains(collision.collider))
                groundList.Remove(collision.collider);
            if (groundList.Count == 0)
                OnGround = false;
        }
    }
}
