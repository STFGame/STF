using System;
using UnityEngine;

namespace Actor.Bubbles
{
    [Serializable]
    public struct BubbleAspects
    {
        public Transform parent;
        public ColliderType colliderType;
        public BodyArea bodyArea;
        public float radius;
        public Vector3 center;
        public Vector3 size;
        public Color color;
    }
}
