using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Actor.Survivability
{
    [Serializable]
    public class Dodge
    {
        [SerializeField] private float maxLength = 1f;

        private float timer = 0f;

        public void PerformDodge(List<GameObject> hurtBubbles, ref bool isSpotDodging)
        {
            if(isSpotDodging)
            {
                for (int i = 0; i < hurtBubbles.Count; i++)
                    hurtBubbles[i].SetActive(false);

                timer += Time.deltaTime;

                if(timer >= maxLength)
                {
                    for (int i = 0; i < hurtBubbles.Count; i++)
                        hurtBubbles[i].SetActive(true);
                    isSpotDodging = false;
                    timer = 0f;
                }
            }
        }
    }
}
