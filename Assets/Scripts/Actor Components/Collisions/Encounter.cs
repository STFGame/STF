using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using Utility.Enums;
using Utility.Events;

namespace Actor.Collisions
{
    public abstract class Encounter<T> : IEvent<T>
    {
        protected BodyArea bodyArea;
        protected int layer;

        protected Action<T> action;

        public virtual void OnCollisionEnter(Collision collision) { }
        public virtual void OnCollisionExit(Collision collision) { }
        public virtual void OnCollisionStay(Collision collision) { }

        public virtual void OnTriggerEnter(Collider collider) { }
        public virtual void OnTriggerExit(Collider collider) { }
        public virtual void OnTriggerStay(Collider collider) { }

        #region Properties
        public virtual BodyArea BodyArea
        {
            get { return bodyArea; }
            set { bodyArea = value; }
        }

        public virtual Action<T> Action
        {
            get { return action; }
            set { action = value; }
        }
        #endregion
    }
}
