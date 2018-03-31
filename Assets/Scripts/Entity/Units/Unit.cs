using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using Utility.Enums;

namespace Entity.Components
{
    [Serializable]
    public class Unit : IUnit
    {
        private Dictionary<Type, Component> _unitDictionary;
        private Rigidbody rigidbody;
        private Animator animator;
        private Transform transform;
        private Collider collider;

        public Unit(MonoBehaviour mono)
        {
            rigidbody = mono.GetComponent<Rigidbody>();
            animator = mono.GetComponent<Animator>();
            transform = rigidbody.transform;
            collider = mono.GetComponent<Collider>();
        }

        public void Register<T>(T component) where T : Component
        {
            if(!_unitDictionary.ContainsKey(typeof(T)))
                _unitDictionary.Add(typeof(T), component);
        }

        public Component GetUnit<T>() where T : Component
        {
            return _unitDictionary[typeof(T)];
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
