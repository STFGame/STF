using Actor.Bubbles;
using Combos;
using Controls;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Actor
{
    public class ActorCombat : MonoBehaviour
    {
        [HideInInspector] public List<GameObject> hitBubbles = new List<GameObject>();
        private BubbleFactory factory = new BubbleFactory();

        //[SerializeField] private Attack[] attacks;
        [SerializeField] private Attack2[] attacks;

        private int attackId;

        public int AttackNumber { get; private set; }

        private void OnEnable()
        {
            RegisterHitBubbles();
            InitializeAttacks();
        }

        private void RegisterHitBubbles()
        {
            for (int i = 0; i < hitBubbles.Count; i++)
            {
                HitBubble hitBubble = hitBubbles[i].GetComponent<HitBubble>();

                factory.Register(hitBubble.aspects.bodyArea, hitBubbles[i]);
            }
        }

        private void InitializeAttacks()
        {
            int count = attacks[0].attackContainer.Length;

            for (int i = 0; i < attacks.Length; i++)
            {
                GameObject[] gameObject = new GameObject[attacks[i].attackContainer.Length];
                for (int j = 0; j < attacks[i].attackContainer.Length; j++)
                {
                    gameObject[j] = factory.GetBubble(attacks[i].attackContainer[j].BodyArea);
                    attacks[i].attackContainer[j].HitBubbleGameObject = gameObject[j];

                    attacks[i].attackContainer[j].Initiate();
                }
                attacks[i].Init();
            }
        }

        //private void InitializeAttacks()
        //{
        //for (int i = 0; i < attacks.Length; i++)
        //{
        //GameObject[] gameObject = new GameObject[attacks[i].bodyAreas.Length];
        //for (int j = 0; j < attacks[i].bodyAreas.Length; j++)
        //gameObject[j] = factory.GetBubble(attacks[i].bodyAreas[j]);
        //attacks[i].Init(gameObject);
        //}
        //}

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
                    Vector3 direction = attacks[i].Displace.Direction * transform.forward.x;
                    float speed = attacks[i].Displace.speed;

                    Vector3 targetVelocity = direction * speed;
                    Vector3 velocityChange = (targetVelocity - GetComponent<Rigidbody>().velocity) * (Time.deltaTime * 10);

                    velocityChange.x = Mathf.Clamp(velocityChange.x, -speed, speed);
                    velocityChange.y = Mathf.Clamp(velocityChange.y, -speed, speed);
                    velocityChange.z = Mathf.Clamp(velocityChange.z, -speed, speed);

                    GetComponent<Rigidbody>().AddForce(velocityChange, ForceMode.VelocityChange);

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