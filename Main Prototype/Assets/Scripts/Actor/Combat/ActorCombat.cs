using Actor.Bubbles;
using Combos;
using Controls;
using System.Collections.Generic;
using UnityEngine;

namespace Actor
{
    public class ActorCombat : MonoBehaviour
    {
        [HideInInspector] public List<GameObject> hitBubbles = new List<GameObject>();
        private BubbleFactory factory = new BubbleFactory();

        [SerializeField] private Attack[] attacks;

        private int attackId;

        public int AttackNumber { get; private set; }

        private void Awake()
        {
            for (int i = 0; i < hitBubbles.Count; i++)
            {
                HitBubble hitBubble = hitBubbles[i].GetComponent<HitBubble>();

                factory.Register(hitBubble.aspects.bodyArea, hitBubbles[i]);
            }

            for (int i = 0; i < attacks.Length; i++)
            {
                GameObject[] gameObject = new GameObject[attacks[i].bodyAreas.Length];
                for (int j = 0; j < attacks[i].bodyAreas.Length; j++)
                    gameObject[j] = factory.GetBubble(attacks[i].bodyAreas[j]);
                attacks[i].Init(gameObject);
            }
        }

        private void Start()
        {
            GetComponentInChildren<ComboManager>().comboBehaviour.ComboEvent += UpdateAttackId;
        }

        public void Perform()
        {
            for (int i = 0; i < attacks.Length; i++)
            {
                AttackNumber = attacks[i].PerformAttack(attackId);

                if (AttackNumber > 0)
                {
                    Vector3 direction = attacks[i].Displace.Direction;
                    float speed = attacks[i].Displace.speed;

                    Vector3 targetVelocity = direction * speed;
                    Vector3 velocityChange = (targetVelocity - GetComponent<Rigidbody>().velocity) * (Time.deltaTime * 10);

                    velocityChange.x = Mathf.Clamp(velocityChange.x, -speed, speed) ;
                    velocityChange.y = Mathf.Clamp(velocityChange.y, -speed, speed);
                    velocityChange.z = Mathf.Clamp(velocityChange.z, -speed, speed);

                    velocityChange.x *= transform.forward.x;

                    GetComponent<Rigidbody>().AddForce(velocityChange, ForceMode.VelocityChange);

                    Debug.Log(velocityChange);

                    break;
                }
            }
        }

        private void UpdateAttackId(int attackId)
        {
            this.attackId = attackId;
        }

        private void OnDrawGizmos() { }
    }
}