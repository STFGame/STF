using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Actor.Encounters
{
    [Serializable]
    public class Groundbox : Encounter<bool>
    {
        [SerializeField] private float normalPoint = 0.5f;
        [SerializeField] private bool onGround;

        private List<Collider> groundList;

        public Groundbox()
        {
            groundList = new List<Collider>();
        }

        public override void OnCollisionEnter(Collision collision)
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
                Update_OnGround(true);
            }
            else
            {
                if (groundList.Contains(collision.collider))
                    groundList.Remove(collision.collider);
                if (groundList.Count == 0)
                    Update_OnGround(false);
            }
        }

        public override void OnCollisionExit(Collision collision)
        {
            if (groundList.Contains(collision.collider))
                groundList.Remove(collision.collider);
            if (groundList.Count == 0)
                Update_OnGround(false);
        }

        private void Update_OnGround(bool onGround)
        {
            if (this.onGround == onGround)
                return;

            this.onGround = onGround;

            if (action != null)
                action(this.onGround);
        }

        #region Properties
        public bool OnGround
        {
            get { return onGround; }
        }
        #endregion
    }
}
