using Actor.Bubbles;
using Controls;
using System.Collections.Generic;
using UnityEngine;

namespace Actor
{
    public class ActorCombat : MonoBehaviour
    {
        [HideInInspector] public List<GameObject> hitBubbles = new List<GameObject>();
        private BubbleFactory factory = new BubbleFactory();

        public Attack[] attacks;

        private void Start()
        {
            for (int i = 0; i < hitBubbles.Count; i++)
            {
                HitBubble hitBubble = hitBubbles[i].GetComponent<HitBubble>();

                factory.Register(hitBubble.aspects.bodyArea, hitBubbles[i]);
            }

            for (int i = 0; i < attacks.Length; i++)
                attacks[i].Init(factory.GetBubble(attacks[i].bodyArea), GetComponent<ActorControl>().device);
        }

        public void Perform()
        {
            for (int i = 0; i < attacks.Length; i++)
                if (attacks[i].PerformAttack())
                {
                    Vector3 direction = attacks[i].Displace.Direction;
                    float speed = attacks[i].Displace.speed;

                    Vector3 targetVelocity = direction * speed;
                    Vector3 velocityChange = (targetVelocity - GetComponent<Rigidbody>().velocity) * (Time.deltaTime * 10);

                    velocityChange.x = Mathf.Clamp(velocityChange.x, -speed, speed) * transform.forward.x;
                    velocityChange.y = Mathf.Clamp(velocityChange.y, -speed, speed);
                    velocityChange.z = Mathf.Clamp(velocityChange.z, -speed, speed);

                    GetComponent<Rigidbody>().AddForce(velocityChange, ForceMode.VelocityChange);
                }
        }

        private void OnDrawGizmos() { }
    }
}