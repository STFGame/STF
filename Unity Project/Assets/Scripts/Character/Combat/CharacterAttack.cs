using Boxes;
using Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Character
{
    public class CharacterAttack : MonoBehaviour
    {
        [SerializeField] private Attack[] attacks = null;
        private List<Hitbox> hitboxes = new List<Hitbox>();

        private new Rigidbody rigidbody;
        private Animator animator;

        public int AttackID { get; set; }

        private void Awake()
        {
            rigidbody = GetComponent<Rigidbody>();
            animator = GetComponent<Animator>();

            foreach (AttackBehaviour behaviour in animator.GetBehaviours<AttackBehaviour>())
                behaviour.characterAttack = this;
        }

        private void Start()
        {
            FillHitList();

            InitiateAttacks();
        }

        private void FillHitList()
        {
            var boxAreas = Enum.GetValues(typeof(BoxArea));
            foreach (BoxArea boxArea in boxAreas)
            {
                Hitbox hitbox = (Hitbox)GetComponent<BoxManager>().GetBox(BoxType.Hitbox, boxArea);
                if (hitbox != null)
                    hitboxes.Add(hitbox);
            }
        }

        private void InitiateAttacks()
        {
            if (hitboxes == null)
                return;

            for (int i = 0; i < attacks.Length; i++)
                for (int j = 0; j < attacks[i].attackInfos.Length; j++)
                    for (int k = 0; k < hitboxes.Count; k++)
                        attacks[i].attackInfos[j].Initiate(hitboxes[k]);
        }

        public void Attack()
        {
            for (int i = 0; i < attacks.Length; i++)
            {
                if (AttackID != 0)
                    for (int j = 0; j < hitboxes.Count; j++)
                        hitboxes[i].gameObject.layer = (int)Layer.Hitbox;
                else
                    for (int j = 0; j < hitboxes.Count; j++)
                        hitboxes[i].gameObject.layer = (int)Layer.PlayerDynamic;
            }
        }

        public void AnimateAttack(int attack)
        {
            animator.SetInteger("Attack", attack);
        }
    }
}