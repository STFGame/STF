using Actions;
using Boxes;
using Combat;
using Managers;
using Misc;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Character
{
    /// <summary>
    /// Class that contains all of the attack info of the character.
    /// </summary>
    public class CharacterAttack : MonoBehaviour, IAttack
    {
        #region CharacterAttack Variables
        [Header("Attack Values")]
        //Array of all of the attacks that a character can perform
        [SerializeField] private Attack[] attacks = null;

        [Header("Freeze Values")]
        [SerializeField] private Act freezeAction = null;

        [Header("Immunity Values")]

        //List of all of the hitboxes that the character has
        private List<Hitbox> hitboxes = new List<Hitbox>();

        private new Rigidbody rigidbody;
        private Animator animator;

        private float animSpeed = 0f;
        private bool hit = false;

        public bool Hit { get { return hit; } set { hit = value; } }
        public float Damage { get; private set; }

        public int AttackID { get; set; }
        #endregion

        #region Load
        private void Awake()
        {
            rigidbody = GetComponent<Rigidbody>();
            animator = GetComponent<Animator>();

            foreach (AttackBehaviour behaviour in animator.GetBehaviours<AttackBehaviour>())
                behaviour.characterAttack = this;
        }

        private void Start()
        {
            LoadHitboxes();

            LoadAttacks();
        }

        //Fills the hitbox list with all of the hitboxes
        private void LoadHitboxes()
        {
            var boxAreas = Enum.GetValues(typeof(BoxArea));
            int count = 0;
            foreach (BoxArea boxArea in boxAreas)
            {
                Hitbox hitbox = (Hitbox)GetComponent<BoxManager>().GetBox(BoxType.Hitbox, boxArea);
                if (hitbox != null)
                {
                    hitboxes.Add(hitbox);
                    hitboxes[count].Enabled(false);
                    count++;
                }
            }
        }

        //Loads all of the attacks
        private void LoadAttacks()
        {
            if (hitboxes == null)
                return;
        }
        #endregion

        #region Updates
        public void Attack(int actionID)
        {
            HitFreeze();
        }

        private void HitFreeze()
        {
            if (animator.speed != 0)
                animSpeed = animator.speed;

            if (hit)
            {
                float animSpeed = animator.speed;
                freezeAction.Perform(ref animSpeed, ref hit);
                animator.speed = animSpeed;
            }

            animator.speed = (hit) ? 0f : animSpeed;
        }

        public void AnimateAttack(int attackIndex)
        {
            animator.SetInteger("Attack", attackIndex);
        }
        #endregion

        #region Event Function
        public void EnableHitbox(BoxArea boxArea)
        {
            if (boxArea == BoxArea.None)
                for (int i = 0; i < hitboxes.Count; i++)
                    hitboxes[i].Enabled(false);

            for (int i = 0; i < hitboxes.Count; i++)
            {
                Hitbox hitbox = hitboxes[i];
                if (hitbox.boxArea == boxArea)
                {
                    hitbox.Enabled(true);
                    break;
                }
            }
        }
        #endregion
    }
}