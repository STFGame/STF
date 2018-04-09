using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using Utility.Enums;

namespace Utility.Shapes
{
    public class Box : IShape
    {
        [SerializeField] private Vector3 size;

        [SerializeField] public Vector3 Size { get { return size; } set { size = value; } }

        [SerializeField] public Shape Shape { get; private set; }

        public Box()
        {
            Shape = Shape.Box;
        }

        public void AddCollider(GameObject gameObject)
        {
            gameObject.AddComponent<BoxCollider>();
        }
    }
}
