using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utility;

namespace Actor.Bubbles
{
    public enum BubbleType { None, HurtBubble, HitBubble, GroundBubble, ThrowBubble }
    public enum ColliderType { None, SphereCollider, BoxCollider }

    public class Bubble : MonoBehaviour
    {
        public Transform parent = null;
        public Color color = Color.green;
        public ColliderType colliderType = ColliderType.None;
        public BodyArea bodyArea = BodyArea.None;
        public BubbleType bubbleType = BubbleType.None;

        public bool isEnabled = true;

        private bool isTriggered = false;

        #region HitBubble Variables

        public delegate void HitChange(Collider other);
        public event HitChange IntersectEvent;

        #endregion

        #region HurtBubble Variables
        public new string tag = "";
        private int hitIndex = 0;

        public delegate void HurtChange(int bodyArea);
        public event HurtChange HurtEvent;
        #endregion

        #region GroundBubble Variables
        public delegate void GroundChange(bool onGround);
        public event GroundChange GroundEvent;
        #endregion

        #region Unity Triggers
        private void OnTriggerEnter(Collider other)
        {
            switch (bubbleType)
            {
                case BubbleType.HitBubble:
                    HitBubbleEnter(other);
                    break;
                case BubbleType.HurtBubble:
                    HurtBubbleEnter(other);
                    break;
                case BubbleType.GroundBubble:
                    GroundBubbleEnter(other);
                    break;
                case BubbleType.ThrowBubble:
                    ThrowBubbleEnter(other);
                    break;
                default:
                    break;
            }
        }

        private void OnTriggerExit(Collider other)
        {
            switch (bubbleType)
            {
                case BubbleType.HitBubble:
                    HitBubbleExit(other);
                    break;
                case BubbleType.HurtBubble:
                    HurtBubbleExit(other);
                    break;
                case BubbleType.GroundBubble:
                    GroundBubbleExit(other);
                    break;
                case BubbleType.ThrowBubble:
                    ThrowBubbleExit(other);
                    break;
                default:
                    break;
            }
        }
        #endregion

        #region HitBubble Methods
        private void HitBubbleEnter(Collider other)
        {
            if (!isEnabled)
            {
                UpdateIntersect(false, null);
                return;
            }

            UpdateIntersect(true, other);
        }

        private void HitBubbleExit(Collider other)
        {
            if (!isEnabled)
                return;

            UpdateIntersect(false, null);
        }

        private void UpdateIntersect(bool isHit, Collider other)
        {
            if (this.isTriggered == isHit)
                return;

            this.isTriggered = isHit;

            if (IntersectEvent != null)
                IntersectEvent(other);
        }
        #endregion

        #region HurtBubble Methods
        public void InitHurtBubble(HitZone hitZone)
        {
            if (hitZone == HitZone.Complex)
            {
                hitIndex = (int)bodyArea;
                return;
            }

            if (bodyArea == BodyArea.Head || bodyArea == BodyArea.RightShoulder || bodyArea == BodyArea.RightShoulder ||
                bodyArea == BodyArea.UpperTorso || bodyArea == BodyArea.LeftBicep || bodyArea == BodyArea.RightBicep)
                hitIndex = (int)SimpleBodyArea.Head;
            else if (bodyArea == BodyArea.Hips || bodyArea == BodyArea.MidTorso || bodyArea == BodyArea.LowTorso ||
                     bodyArea == BodyArea.LeftElbow || bodyArea == BodyArea.RightElbow || bodyArea == BodyArea.LeftForearm ||
                     bodyArea == BodyArea.RightForearm || bodyArea == BodyArea.RightWrist || bodyArea == BodyArea.LeftWrist ||
                     bodyArea == BodyArea.RightHand || bodyArea == BodyArea.LeftHand)
                hitIndex = (int)SimpleBodyArea.Torso;
            else
                hitIndex = (int)SimpleBodyArea.Legs;
        }

        private void HurtBubbleEnter(Collider other)
        {

            if (!isEnabled || tag == other.tag)
            {
                Debug.Log("Here");
                Debug.Log(tag);

                UpdateHurtBubble(false, 0);
                return;
            }

            Debug.Log(other.tag);

            UpdateHurtBubble(true, hitIndex);
        }

        private void HurtBubbleExit(Collider other)
        {
            UpdateHurtBubble(false, 0);
        }

        private void UpdateHurtBubble(bool isHurt, int hitIndex)
        {
            if (isTriggered == isHurt)
                return;

            isTriggered = isHurt;

            if (HurtEvent != null)
                HurtEvent(hitIndex);
        }
        #endregion

        #region GroundBubble Methods
        private void GroundBubbleEnter(Collider other)
        {
            if (!isEnabled)
                return;

            UpdateOnGround(true);
        }

        private void GroundBubbleExit(Collider other)
        {
            if (!isEnabled)
                return;

            UpdateOnGround(false);
        }

        private void UpdateOnGround(bool onGround)
        {
            if (onGround == isTriggered)
                return;

            isTriggered = onGround;

            if (GroundEvent != null)
                GroundEvent(isTriggered);
        }
        #endregion

        #region ThrowBubble Methods
        private void ThrowBubbleEnter(Collider other)
        {
            other.transform.position = parent.position;
        }

        private void ThrowBubbleExit(Collider other)
        {

        }
        #endregion

        #region Draw
        private void OnDrawGizmos()
        {
            if (parent == null || !isEnabled)
                return;

            Matrix4x4 transformMatrix = Matrix4x4.TRS(transform.position, transform.rotation, transform.lossyScale);
            Gizmos.matrix = transformMatrix;

            Gizmos.color = color;
            if (colliderType == ColliderType.BoxCollider)
            {
                Vector3 size = GetComponent<BoxCollider>().size;
                Vector3 offset = GetComponent<BoxCollider>().center;
                Gizmos.DrawWireCube(offset, size * -1);
            }
            else if (colliderType == ColliderType.SphereCollider)
            {
                float radius = GetComponent<SphereCollider>().radius;
                Vector3 offset = GetComponent<SphereCollider>().center;
                Gizmos.DrawWireSphere(offset, radius);
            }
        }
        #endregion
    }
}