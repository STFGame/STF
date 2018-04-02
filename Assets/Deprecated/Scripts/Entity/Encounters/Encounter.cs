using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Entity.Encounters
{
    public abstract class Encounter<T>
    {
        public Action<T> action;

        public virtual void OnCollisionEnter(Collision collision) { }
        public virtual void OnCollisionExit(Collision collision) { }
        public virtual void OnCollisionStay(Collision collision) { }

        public virtual void OnTriggerEnter(Collider collider) { }
        public virtual void OnTriggerExit(Collider collider) { }
        public virtual void OnTriggerStay(Collider collider) { }
    }
}
