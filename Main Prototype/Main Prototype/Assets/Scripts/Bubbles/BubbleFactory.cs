using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Actor.Bubbles
{
    public class BubbleFactory
    {
        private Dictionary<BodyArea, GameObject> bubbleDictionary;

        public BubbleFactory()
        {
            bubbleDictionary = new Dictionary<BodyArea, GameObject>();
        }

        public void Register(BodyArea key, GameObject value)
        {
            bubbleDictionary.Add(key, value);
        }

        public GameObject GetBubble(BodyArea key)
        {
            return bubbleDictionary[key];
        }

        public bool ContainsKey(BodyArea key)
        {
            return bubbleDictionary.ContainsKey(key);
        }

        public void Enable(BodyArea key, bool value)
        {
            bubbleDictionary[key].SetActive(value);
        }
    }
}
