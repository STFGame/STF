using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using Utility.Enums;

namespace Actor.Components
{
    [Serializable]
    public class Unit : IUnit
    {
        Component[] component = new Component[20];
        int index = 0;

        private Rigidbody rigidbody;
        private Animator animator;
        private Transform transform;
        private Collider collider;

        public Unit()
        {
        }

        public Unit(Component component)
        {
        }

        public Unit(MonoBehaviour mono)
        {
            rigidbody = mono.GetComponent<Rigidbody>();
            animator = mono.GetComponent<Animator>();
            transform = rigidbody.transform;
            collider = mono.GetComponent<Collider>();
        }

        public void Register<T>(T component) where T : Component
        {
            this.component[index] = component;
            index++;
        }

        public T GetUnit<T>() where T : Component
        {
            return (T)component[0];
        }

        #region Properties
        public Rigidbody Rigidbody
        {
            get { return rigidbody; }
            set { rigidbody = value; }
        }

        public Animator Animator
        {
            get { return animator; }
            set { animator = value; }
        }

        public Transform Transform
        {
            get { return transform; }
            set { transform = value; }
        }

        public Collider Collider
        {
            get { return collider; }
            set { collider = value; }
        }
        #endregion
    }
}
