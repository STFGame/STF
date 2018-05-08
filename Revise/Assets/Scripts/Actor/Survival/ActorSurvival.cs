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
    public enum HitZone { Simple, Complex }

    public class ActorSurvival : MonoBehaviour
    {
        [SerializeField] private HitZone hitZone = HitZone.Simple;
        [SerializeField] private Shield shield = new Shield();
        [SerializeField] private Health health = new Health();
        [SerializeField] private Dodge dodge = new Dodge();

        private List<Bubble> hurtBubbles = new List<Bubble>();
        private SurvivalAnimation survivalAnimation = new SurvivalAnimation();

        #region Survival Properties
        public bool IsBlocking { get { return shield.isBlocking; } private set { shield.isBlocking = value; } }
        public bool IsStunned { get { return shield.isStunned; } private set { shield.isStunned = value; } }
        public int HitIndex { get; private set; }
        #endregion

        private string hitTag = "";

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
                    hurtBubbles.Add(hurtBubble.GetComponent<Bubble>());
            }

            for (int i = 0; i < hurtBubbles.Count; i++)
            {
                hurtBubbles[i].GetComponent<Bubble>().InitHurtBubble(hitZone);
                hurtBubbles[i].GetComponent<Bubble>().HurtEvent += UpdateHitIndex;
            }
        }

        private void UpdateHitIndex(int hitIndex) { HitIndex = hitIndex; }

        private float timer = 0f;

        public void PerformSurvival(bool block)
        {
            health.Update();

            shield.Block(block);

            if (hitTag != "")
                ResetTag();

            if (IsStunned)
                GetComponent<Rigidbody>().velocity = (Vector3.zero);
        }

        private void ResetTag()
        {
            timer += Time.deltaTime;

            if (timer >= 2f)
            {
                hitTag = "";
                timer = 0;
                SetBubbleTag("Blank");
            }
        }

        public void Dodge(Vector3 direction)
        {
            if (IsBlocking)
            {
                if (direction.y <= -0.5)
                    Debug.Log("Spot Dodge");
            }
        }

        private void DisableAllBubbles(bool disable)
        {
            for (int i = 0; i < hurtBubbles.Count; i++)
                hurtBubbles[i].isEnabled = disable;
        }

        private void SetBubbleTag(string nextTag)
        {
            for (int i = 0; i < hurtBubbles.Count; i++)
                hurtBubbles[i].tag = nextTag;
        }

        public void TakeDamage(AttackInfo attack)
        {
            if (hitTag == attack.Tag)
                return;

            hitTag = attack.Tag;

            SetBubbleTag(hitTag);

            if (IsBlocking)
                shield.TakeDamage(attack.damage.damage);
            else
                health.TakeDamage(attack.damage.damage);
        }

        public void PlaySurvivalAnimation()
        {
            survivalAnimation.PlayBlockAnim(IsBlocking);
            survivalAnimation.PlayStunAnim(IsStunned);
            survivalAnimation.PlayHitAnim(HitIndex);
        }

        private void OnDrawGizmos() { }
    }
}