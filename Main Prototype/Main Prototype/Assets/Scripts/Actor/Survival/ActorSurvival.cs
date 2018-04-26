using Actor.Bubbles;
using Actor.Survivability;
using System.Collections.Generic;
using UnityEngine;

namespace Actor
{
    public class ActorSurvival : MonoBehaviour
    {
        [HideInInspector] public List<GameObject> hurtBubbles = new List<GameObject>();
        private Dictionary<BodyArea, GameObject> _hurtBubbleDict = new Dictionary<BodyArea, GameObject>();

        private bool isHit = false;
        private BubbleZone zone = BubbleZone.None;

        [SerializeField] private Shield shield = new Shield();
        [SerializeField] private Health health = new Health();

        private void Awake()
        {
            for (int i = 0; i < hurtBubbles.Count; i++)
            {
                _hurtBubbleDict.Add(hurtBubbles[i].GetComponent<HurtBubble>().aspects.bodyArea, hurtBubbles[i]);

                hurtBubbles[i].GetComponent<HurtBubble>().HurtEvent += OnHurt;
            }
        }

        public void TakeDamage(int damageAmount)
        {
            if (shield.IsBlocking)
                shield.TakeDamage(damageAmount);
            else
                health.TakeDamage(damageAmount);
        }

        private void OnHurt(BubbleZone zone, bool isHit)
        {
            this.zone = zone;
            this.isHit = isHit;
        }

        private void OnDrawGizmos() { }
    }
}