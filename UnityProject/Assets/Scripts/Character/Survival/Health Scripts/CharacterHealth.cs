using Boxes;
using Survival;
using Managers;
using System;
using System.Collections.Generic;
using UnityEngine;
using Actions;
using Alerts;
using Misc;
using UnityEngine.EventSystems;
using System.Collections;
using System.Threading.Tasks;

namespace Character
{
    /// <summary>
    /// Health MonoBehaviour that controls how much health the character has
    /// </summary>
    public class CharacterHealth : Health, ISpawnable
    {
        #region Health Variables
        [Header("Visual Health Values")]
        [SerializeField] private HealthRegular m_HealthRegular = null;

        [SerializeField] private LifeTokenDisplay[] m_LifeTokens = null;

        [SerializeField] [Range(1, 5)] private int lives = 3;

        [Header("Freeze Value")]
        [SerializeField] private Act m_Freeze = null;
        [SerializeField] private Act m_Immune = null;
        [SerializeField] private Act m_Death = null;

        //Used to prevent character from taking damage while shielding-
        private CharacterShield m_CharacterShield = null;

        private Rigidbody m_Rigidbody;
        private Animator m_Animator;

        private List<Hurtbox> m_Hurtboxes = null;

        //The previous health of the character converted into a percentage
        private float m_PreviousHealth = 0f;

        private AlertValue m_Alert = AlertValue.None;

        //Properties that can be used by other classes
        public bool Immune { get; private set; }
        public int Lives { get { return lives; } }
        public bool Dead { get; set; }
        #endregion

        #region Load
        //Starts at the highest prority.
        private void Awake()
        {
            m_CharacterShield = GetComponent<CharacterShield>();
            m_Rigidbody = GetComponent<Rigidbody>();
            m_Animator = GetComponent<Animator>();

            m_CurrentHealth = m_MaxHealth;
            m_PreviousHealth = m_MaxHealth;
        }

        private void Start()
        {
            LoadHurtboxes();
        }

        public void Load(Transform parent)
        {
            if (m_HealthRegular != null)
                m_HealthRegular.Load(parent);
        }

        //Stores all of the available Hurtboxes into a list to make them
        //easier to access inside the class.
        private void LoadHurtboxes()
        {
            m_Hurtboxes = new List<Hurtbox>();

            var boxAreas = Enum.GetValues(typeof(BoxArea));
            foreach (BoxArea boxArea in boxAreas)
            {
                Hurtbox hurtbox = (Hurtbox)GetComponent<BoxManager>().GetBox(BoxType.Hurtbox, boxArea);
                if (hurtbox)
                    m_Hurtboxes.Add(hurtbox);
            }
        }

        public void LoadLifeTokens(Transform[] parents)
        {
            for (int i = 0; i < parents.Length; i++)
                m_LifeTokens[i].Load(parents[i]);
        }
        #endregion

        #region Updates
        //Update function of Health
        public override void Tick()
        {
            PlayHealthAnimations();

            if (Dead)
            {
                m_HealthRegular.DecreaseHealth(0, ref m_PreviousHealth, m_MaxHealth);

                Aeon.Invoke(this, Death, 2f);
            }

            CharacterHurt();

            if (m_HealthRegular != null)
                m_HealthRegular.DecreaseHealth(m_CurrentHealth, ref m_PreviousHealth, m_MaxHealth);
        }

        private void PlayHealthAnimations()
        {
            m_Animator.SetInteger("Hit", m_HurtIndex);
            m_Animator.SetBool("Dead", Dead);

            Dead = false;
        }

        private void Death()
        {
            bool death = true;
            m_Death.Perform(ref death);

            if (!death)
            {
                Alert.Send<IAlert>(gameObject, (x, y) => x.Inform(AlertValue.Dead));
                gameObject.layer = (int)Layer.Dead;
            }

        }
        #endregion

        #region Hurt Effects
        //Entrance method for getting hit. Starts the coroutines for HitFreeze and also
        //starts the ImmunityPhase where the character cannot be hurt.
        private void CharacterHurt()
        {
            if (m_CharacterShield)
                if (m_CharacterShield.Shielding)
                    return;

            if (m_IsHurt)
                HurtFreeze();

            if (Immune)
                Immunity();
        }

        private void HurtFreeze()
        {
            float animSpeed = m_Animator.speed;
            m_Freeze.Perform(ref animSpeed, ref m_IsHurt);
            m_Animator.speed = animSpeed;

            Immune = true;
        }

        private void Immunity()
        {
            bool immune = true;
            m_Immune.Perform(ref immune);

            for (int i = 0; i < m_Hurtboxes.Count; i++)
                m_Hurtboxes[i].Enabled(!immune);

            Immune = immune;
        }
        #endregion

        #region Damage
        //Method is called whenever the character is supposed to take damage
        public override void TakeDamage(float damage)
        {
            if (m_CharacterShield)
                if (m_CharacterShield.Shielding)
                    return;

            m_PreviousHealth = (m_CurrentHealth / MaxHealth);
            m_CurrentHealth -= damage;

            if (m_CurrentHealth <= 0)
            {
                Dead = true;
                m_CurrentHealth = 0f;
            }
        }
        #endregion

        #region Restore and Death
        //Method for reviving the character. Acts like a reset by setting most values back to
        //default
        public void Restore(float restoreHealth)
        {
            m_CurrentHealth = restoreHealth;
            m_PreviousHealth = m_CurrentHealth;

            m_CurrentHealth = Mathf.Clamp(m_CurrentHealth, 0f, m_MaxHealth);

            Dead = false;
        }
        #endregion

        #region Alerts
        public override void Inform(AlertValue alert)
        {
            m_Alert = alert;
        }
        #endregion
    }
}