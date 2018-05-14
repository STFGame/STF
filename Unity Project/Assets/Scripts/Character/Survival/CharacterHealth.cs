using Boxes;
using Survival;
using Managers;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Misc;
using Actions;

namespace Character
{
    /// <summary>
    /// Health MonoBehaviour that controls how much health the character has
    /// </summary>
    public class CharacterHealth : MonoBehaviour, IHealth, IDamagable
    {
        #region Health Variables
        [Header("Visual Health Values")]
        [SerializeField] private HealthRegular healthRegular = null;

        [SerializeField] private LifeTokenDisplay[] lifeTokenDisplay = null;

        [Header("Main Health Values")]
        [SerializeField] [Range(0f, 1000f)] private float maxHealth = 100f;
        [SerializeField] [Range(1, 5)] private int lives = 3;

        [Header("Freeze Value")]
        [SerializeField] private Act freezeAct = null;
        [SerializeField] private Act immunity = null;

        //Used to prevent character from taking damage while shielding-
        private CharacterShield characterShield = null;

        private new Rigidbody rigidbody;
        private Animator animator;

        private List<Hurtbox> hurtboxes = null;

        //The current health of the character
        private float currentHealth = 0f;

        //The previous health of the character converted into a percentage
        private float previousHealth = 0f;

        //Used by the Hurtboxes on the character so as to inform the character when
        //he/she is hit.
        [HideInInspector] public bool isHurt = false;
        [HideInInspector] public int hurtID = 0;

        //Properties that can be used by other classes
        public bool Immune { get; private set; }
        public int Lives { get { return lives; } }
        public bool Dead { get; set; }

        public float MaxHealth { get { return maxHealth; } }
        public float CurrentHealth { get { return currentHealth; } set { currentHealth = value; } }
        #endregion

        #region Load
        //Starts at the highest prority.
        private void Awake()
        {
            characterShield = GetComponent<CharacterShield>();
            rigidbody = GetComponent<Rigidbody>();
            animator = GetComponent<Animator>();

            currentHealth = maxHealth;
            previousHealth = maxHealth;
        }

        private void Start()
        {
            LoadHurtboxes();
        }

        //Stores all of the available Hurtboxes into a list to make them
        //easier to access inside the class.
        private void LoadHurtboxes()
        {
            hurtboxes = new List<Hurtbox>();

            var boxAreas = Enum.GetValues(typeof(BoxArea));
            foreach (BoxArea boxArea in boxAreas)
            {
                Hurtbox hurtbox = (Hurtbox)GetComponent<BoxManager>().GetBox(BoxType.Hurtbox, boxArea);
                if (hurtbox)
                    hurtboxes.Add(hurtbox);
            }
        }

        //Loads the visuals of the health component and sets it the child of 
        //the specified parent.
        public void LoadHealth(Transform parent)
        {
            if (healthRegular != null)
                healthRegular.Load(parent);
        }

        public void LoadLifeTokens(Transform[] parents)
        {
            for (int i = 0; i < parents.Length; i++)
                lifeTokenDisplay[i].Load(parents[i]);
        }
        #endregion

        #region Updates
        //Update function of Health
        public void UpdateHealth()
        {
            if (Dead)
            {
                gameObject.layer = (int)Layer.Dead;
                healthRegular.DecreaseHealth(0, ref previousHealth, maxHealth);
                return;
            }

            CharacterHurt();

            if (healthRegular != null)
                healthRegular.DecreaseHealth(currentHealth, ref previousHealth, maxHealth);

            animator.SetInteger("Hit", hurtID);
        }
        #endregion

        #region Hurt Effects
        //Entrance method for getting hit. Starts the coroutines for HitFreeze and also
        //starts the ImmunityPhase where the character cannot be hurt.
        private void CharacterHurt()
        {
            if (characterShield)
                if (characterShield.Shielding)
                    return;

            if (isHurt)
                HurtFreeze();

            if (Immune)
                Immunity();
        }

        private void HurtFreeze()
        {
            float animSpeed = animator.speed;
            freezeAct.Perform(ref animSpeed, ref isHurt);
            animator.speed = animSpeed;

            Immune = true;
        }

        private void Immunity()
        {
            bool immune = true;
            immunity.Perform(ref immune);

            for (int i = 0; i < hurtboxes.Count; i++)
                hurtboxes[i].Enabled(!immune);

            Immune = immune;
        }
        #endregion

        #region Damage
        //Method is called whenever the character is supposed to take damage
        public void TakeDamage(float damage)
        {
            if (characterShield)
                if (characterShield.Shielding)
                    return;

            previousHealth = (currentHealth / MaxHealth);
            currentHealth -= damage;

            if (currentHealth <= 0)
                Death();
        }
        #endregion

        #region Restore and Death
        //Method for reviving the character. Acts like a reset by setting most values back to
        //default
        public void Restore(float restoreHealth)
        {
            currentHealth = restoreHealth;
            previousHealth = currentHealth;

            currentHealth = Mathf.Clamp(currentHealth, 0f, maxHealth);

            Dead = false;
        }

        //Death event to notify the GameManager that the character is dead
        private void Death()
        {
            Dead = true;

            if (lives > -1)
                lives--;
        }
        #endregion
    }
}