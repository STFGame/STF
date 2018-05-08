using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Actor.Bubbles
{
    public class BubbleManager : MonoBehaviour
    {
        [HideInInspector] public List<GameObject> bubbles = new List<GameObject>();

        private Dictionary<BodyArea, GameObject> hurtBubbleDict = new Dictionary<BodyArea, GameObject>();
        private Dictionary<BodyArea, GameObject> hitBubbleDict = new Dictionary<BodyArea, GameObject>();
        private Dictionary<BodyArea, GameObject> groundBubbleDict = new Dictionary<BodyArea, GameObject>();
        private Dictionary<BodyArea, GameObject> throwBubbleDict = new Dictionary<BodyArea, GameObject>();

        private void Awake()
        {
            for (int i = 0; i < bubbles.Count; i++)
            {
                Bubble bubble = bubbles[i].GetComponent<Bubble>();

                if (bubble.bubbleType == BubbleType.HitBubble)
                {
                    if (!hitBubbleDict.ContainsKey(bubble.bodyArea))
                        hitBubbleDict.Add(bubble.bodyArea, bubbles[i]);
                }
                else if (bubbles[i].GetComponent<Bubble>().bubbleType == BubbleType.HurtBubble)
                {
                    if (!hurtBubbleDict.ContainsKey(bubble.bodyArea))
                        hurtBubbleDict.Add(bubble.bodyArea, bubbles[i]);
                }
                else if (bubbles[i].GetComponent<Bubble>().bubbleType == BubbleType.GroundBubble)
                {
                    if (!groundBubbleDict.ContainsKey(bubble.bodyArea))
                        groundBubbleDict.Add(bubble.bodyArea, bubbles[i]);
                }
                else if (bubbles[i].GetComponent<Bubble>().bubbleType == BubbleType.ThrowBubble)
                {
                    if (!throwBubbleDict.ContainsKey(bubble.bodyArea))
                        throwBubbleDict.Add(bubble.bodyArea, bubbles[i]);
                }
            }
        }

        #region Dictionary Searches
        public GameObject GetHitBubbleGB(BodyArea key) { return hitBubbleDict[key]; }

        public Bubble GetHitBubble(BodyArea key) { return hitBubbleDict[key].GetComponent<Bubble>(); }

        public GameObject GetHurtBubbleGB(BodyArea key) { return hurtBubbleDict[key]; }

        public Bubble GetHurtBubble(BodyArea key) { return hurtBubbleDict[key].GetComponent<Bubble>(); }

        public GameObject GetGroundBubbleGB(BodyArea key) { return groundBubbleDict[key]; }

        public Bubble GetGroundBubble(BodyArea key) { return groundBubbleDict[key].GetComponent<Bubble>(); }

        public GameObject GetThrowBubbleGB(BodyArea key) { return throwBubbleDict[key]; }

        public Bubble GetThrowBubble(BodyArea key) { return throwBubbleDict[key].GetComponent<Bubble>(); }
        #endregion
    }
}