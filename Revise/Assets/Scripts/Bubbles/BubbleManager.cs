using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Actor.Bubbles
{
    public enum BodyArea
    {
        None,

        Head,
        Neck,

        UpperTorso,
        MidTorso,
        LowTorso,
        Hips,

        LeftShoulder,
        LeftBicep,
        LeftElbow,
        LeftForearm,
        LeftWrist,
        LeftHand,

        RightShoulder,
        RightBicep,
        RightElbow,
        RightForearm,
        RightWrist,
        RightHand,

        LeftThigh,
        LeftKnee,
        LeftCalf,
        LeftAnkle,
        LeftFoot,

        RightThigh,
        RightKnee,
        RightCalf,
        RightAnkle,
        RightFoot
    }

    public class BubbleManager : MonoBehaviour
    {
        [HideInInspector] public List<GameObject> bubbles = new List<GameObject>();

        private Dictionary<BodyArea, GameObject> hurtBubbleDict = new Dictionary<BodyArea, GameObject>();
        private Dictionary<BodyArea, GameObject> hitBubbleDict = new Dictionary<BodyArea, GameObject>();

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
            }
        }

        public GameObject GetHitBubble(BodyArea key) { return hitBubbleDict[key]; }

        public GameObject GetHurtBubble(BodyArea key) { return hurtBubbleDict[key]; }
    }
}