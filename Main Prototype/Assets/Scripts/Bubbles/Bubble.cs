using UnityEngine;

namespace Actor.Bubbles
{
    public abstract class Bubble : MonoBehaviour
    {
        public BubbleAspects aspects = new BubbleAspects();

        protected new Collider collider;
        protected BubbleZone bubbleZone;

        public bool isEnabled = true;

        protected void Awake()
        {
            collider = GetComponent<Collider>();

            SetBubbleZone(aspects.bodyArea);
        }

        protected abstract void OnTriggerEnter(Collider other);

        protected abstract void OnTriggerExit(Collider other);

        protected void OnDrawGizmos()
        {
            DrawCollider();
        }

        protected void SetBubbleZone(BodyArea bodyArea)
        {
            if (bodyArea == BodyArea.Head || bodyArea == BodyArea.ShoulderL ||
                bodyArea == BodyArea.ShoulderR)
            {
                bubbleZone = BubbleZone.Head;
            }
            else if (bodyArea == BodyArea.Torso || bodyArea == BodyArea.BicepL ||
                    bodyArea == BodyArea.BicepR || bodyArea == BodyArea.ElbowL ||
                    bodyArea == BodyArea.ElbowR || bodyArea == BodyArea.ForearmL ||
                    bodyArea == BodyArea.ForearmR || bodyArea == BodyArea.HandL ||
                    bodyArea == BodyArea.HandR)
            {
                bubbleZone = BubbleZone.Torso;
            }
            else if (bodyArea == BodyArea.Hips || bodyArea == BodyArea.ThighL ||
                    bodyArea == BodyArea.ThighR || bodyArea == BodyArea.KneeL ||
                    bodyArea == BodyArea.KneeR || bodyArea == BodyArea.CalfL ||
                    bodyArea == BodyArea.CalfR || bodyArea == BodyArea.FootL ||
                    bodyArea == BodyArea.FootR)
            {
                bubbleZone = BubbleZone.Legs;
            }
            else
                bubbleZone = BubbleZone.None;
        }

        private void DrawCollider()
        {
            if (GetComponent<Collider>() == null || !isEnabled)
                return;

            Matrix4x4 rotationMatrix = Matrix4x4.TRS(transform.position, transform.rotation, transform.lossyScale);
            Gizmos.matrix = rotationMatrix;

            Gizmos.color = aspects.color;
            if (GetComponent<Collider>().GetType() == typeof(BoxCollider))
                Gizmos.DrawWireCube(GetComponent<BoxCollider>().center, GetComponent<BoxCollider>().size * -1);
            else if (GetComponent<Collider>().GetType() == typeof(SphereCollider))
                Gizmos.DrawWireSphere(GetComponent<SphereCollider>().center, GetComponent<SphereCollider>().radius);
        }
    }
}