using Actor.Bubbles;
using System;
using System.Text;
using UnityEngine;
using Utility.Enums;

/* BUBBLE BEHAVIOUR
 * Sean Ryan
 * April 5, 2018
 * 
 * BubbleBehaviour is an abstract class that contains common elements that are shared among its 
 * derived classes.
 */

namespace Actor
{
    [Serializable]
    public abstract class Bubble : MonoBehaviour, IBubble
    {
        [SerializeField] protected StringBuilder bubbleName;
        [SerializeField] protected BodyArea bodyArea;
        [SerializeField] protected Vector3 offset;
        [SerializeField] protected Transform link;
        [SerializeField] protected Shape shape;
        [SerializeField] protected BubbleType type;
        [SerializeField] protected float radius;
        [SerializeField] protected Vector3 size;

        [SerializeField] public StringBuilder Name { get; set; }
        [SerializeField] public BodyArea BodyArea { get { return bodyArea; } set { bodyArea = value; } }
        [SerializeField] public Vector3 Offset { get { return offset; } set { offset = value; } }
        [SerializeField] public Transform Link { get { return link; } set { link = value; } }
        [SerializeField] public Shape Shape { get { return shape; } set { shape = value; } }
        [SerializeField] public BubbleType Type { get { return type; } set { type = value; } }
        [SerializeField] public float Radius { get { return radius; } set { radius = value; } }
        [SerializeField] public Vector3 Size { get { return size; } set { size = value; } }

        protected abstract void OnTriggerEnter(Collider other);
        protected abstract void OnTriggerExit(Collider other);
    }
}
