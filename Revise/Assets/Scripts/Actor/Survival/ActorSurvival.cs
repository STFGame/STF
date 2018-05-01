using Actor.Animations;
using Actor.Bubbles;
using Actor.Combat;
using Actor.Survivability;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utility;

namespace Actor
{
    public class ActorSurvival : MonoBehaviour
    {
        [SerializeField] private Shield shield = new Shield();
        [SerializeField] private Health health = new Health();
        [SerializeField] private Dodge dodge = new Dodge();

        private List<GameObject> hurtBubbles = new List<GameObject>();

        public bool IsBlocking { get { return shield.isBlocking; } private set { shield.isBlocking = value; } }
        public bool IsStunned { get { return shield.isStunned; } private set { shield.isStunned = value; } }

        public int HitIndex { get; private set; }

        private bool previousDisable = true;

        private SurvivalAnimation survivalAnimation = new SurvivalAnimation();

        private void Awake()
        {
            survivalAnimation.Init(this);
        }

        private void Start()
        {
            for (int i = 0; i < GetComponent<BubbleManager>().bubbles.Count; i++)
            {
                GameObject hurtBubble = GetComponent<BubbleManager>().bubbles[i];
                if (hurtBubble.GetComponent<Bubble>().bubbleType == BubbleType.HurtBubble)
                    hurtBubbles.Add(hurtBubble);
            }
        }

        public void PerformSurvival(bool block)
        {
            shield.Block(block);

            DisableAllBubbles(IsBlocking);

            if (IsStunned)
                GetComponent<Rigidbody>().velocity = (Vector3.zero);
        }

        private float previousDirection = 0f;

        public void Roll(Vector3 direction)
        {
            if (IsBlocking)
            {
                Vector3 move = new Vector3(direction.x, 0f, 0f);
                if (move.x == previousDirection)
                    return;
                if (move.x >= 1f || move.x <= -1f)
                    GetComponent<Rigidbody>().AddForce(move * 15f, ForceMode.VelocityChange);
                previousDirection = move.x;

                gameObject.layer = (int)Layer.PlayerDynamic;

                return;
            }

            gameObject.layer = (int)Layer.PlayerStatic;
        }

        public void Dodge(Vector3 direction)
        {
            if(IsBlocking)
            {
                if (direction.y <= -0.5)
                    Debug.Log("Spot Dodge");
            }
        }

        private void DisableAllBubbles(bool disable)
        {
            if (previousDisable == disable)
                return;

            for (int i = 0; i < hurtBubbles.Count; i++)
                hurtBubbles[i].SetActive(!disable);
            previousDisable = disable;
        }

        public void TakeDamage(AttackDamage damageAmount)
        {
            if (IsBlocking)
                shield.TakeDamage(damageAmount.damage);
            else
                health.TakeDamage(damageAmount.damage);
        }

        public void PlaySurvivalAnimation()
        {
            survivalAnimation.PlayBlockAnim(IsBlocking);
        }

        private void OnDrawGizmos() { }
    }
}