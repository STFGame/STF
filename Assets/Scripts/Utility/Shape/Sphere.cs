using UnityEngine;
using Utility.Enums;

namespace Utility.Shapes
{
    public class Sphere : IShape
    {
        [SerializeField] private float radius;

        [SerializeField] public float Radius { get { return radius; } set { radius = value; } }

        [SerializeField] public Shape Shape { get; private set; }

        public Sphere()
        {
            Shape = Shape.Sphere;
        }

        public void AddCollider(GameObject gameObject)
        {
            gameObject.AddComponent<SphereCollider>();
        }
    }
}
