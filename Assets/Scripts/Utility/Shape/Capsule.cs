using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using Utility.Enums;

namespace Utility.Shapes
{
    public class Capsule : IShape
    {
        [SerializeField] private float radius;

        [SerializeField] public float Radius { get { return radius; } set { radius = value; } }

        [SerializeField] public Shape Shape { get; private set; }

        public Capsule()
        {
            Shape = Shape.Capsule;
        }

        public void AddCollider(GameObject gameObject)
        {
            gameObject.AddComponent<CapsuleCollider>();
        }
    }
}
