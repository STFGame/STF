using Actor.Bubbles;
using System.Collections.Generic;
using UnityEngine;

namespace Actor
{
    public class ActorSurvival : MonoBehaviour
    {
        public List<GameObject> hurtBubbles = new List<GameObject>();

        private Dictionary<BodyArea, GameObject> _hurtBubbleDict = new Dictionary<BodyArea, GameObject>();

        private bool isHit = false;
        private BubbleZone zone = BubbleZone.None;

        private void Awake()
        {
            for (int i = 0; i < hurtBubbles.Count; i++)
            {
                _hurtBubbleDict.Add(hurtBubbles[i].GetComponent<HurtBubble>().aspects.bodyArea, hurtBubbles[i]);

                hurtBubbles[i].GetComponent<HurtBubble>().HurtEvent += OnHurt;
            }

            for (int i = 0; i < hurtBubbles.Count; i++)
                print(_hurtBubbleDict[hurtBubbles[i].GetComponent<HurtBubble>().aspects.bodyArea]);
        }

        private void OnHurt(BubbleZone zone, bool isHit)
        {
            Debug.Log(isHit + " " + zone);

            this.zone = zone;
            this.isHit = isHit;
        }

        private void OnDrawGizmos() { }
    }
}