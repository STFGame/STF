using Boxes;
using Character.Survival;
using Managers;
using Survival;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Character
{
    /// <summary>
    /// Health MonoBehaviour that controls how much health the character has
    /// </summary>
    public class CharacterHealth : MonoBehaviour, IHealth, IDamagable
    {
        public bool hit = false;
        public int area = 0;

        #region Health Variables
        [SerializeField] private Health healthVisual = null;

        [SerializeField] private float maxHealth = 100f;
        [SerializeField] [Range(0f, 1f)] private float freezeStart = 0.2f;
        [SerializeField] [Range(0f, 1f)] private float freezeLength = 1f;
        [SerializeField] [Range(0f, 1f)] private float immunityLength = 1f;

        private CharacterShield characterShield = null;
        private new Rigidbody rigidbody;
        private Animator animator;

        private List<Hurtbox> hurtboxes = null;

        private float currentHealth;
        private float animSpeed = 0f;

        public delegate void DeathDelegate(GameObject character);
        public event DeathDelegate DeathEvent;

        public bool Hurt { get; private set; }
        public bool Dead { get; private set; }
        public float MaxHealth { get { return maxHealth; } }
        #endregion

        #region Initialization
        private void Awake()
        {
            characterShield = GetComponent<CharacterShield>();
            rigidbody = GetComponent<Rigidbody>();
            animator = GetComponent<Animator>();

            currentHealth = maxHealth;
        }

        private void OnEnable()
        {
            StartCoroutine(SubscriptionDelay());
        }

        private IEnumerator SubscriptionDelay()
        {
            yield return new WaitForEndOfFrame();
            SetupHurtboxes();
        }

        private void OnDisable()
        {
            for (int i = 0; i < hurtboxes.Count; i++)
                hurtboxes[i].HurtEvent -= UpdateHitIndex;
        }

        private void SetupHurtboxes()
        {
            hurtboxes = new List<Hurtbox>();

            var boxAreas = Enum.GetValues(typeof(BoxArea));

            int index = 0;
            foreach (BoxArea boxArea in boxAreas)
            {
                Hurtbox hurtbox = (Hurtbox)GetComponent<BoxManager>().GetBox(BoxType.Hurtbox, boxArea);
                if (hurtbox != null)
                {
                    hurtboxes.Add(hurtbox);
                    hurtboxes[index++].HurtEvent += UpdateHitIndex;
                }
            }
        }

        public void InjectHealth(Health healthVisual)
        {
            this.healthVisual = healthVisual;

        }
        #endregion

        #region Hurt Methods
        private void UpdateHitIndex(int index)
        {
            if (characterShield)
                if (characterShield.Shielding)
                    return;

            animator.SetInteger("Hit", index);

            if (index > 0 && !Dead)
            {
                StartCoroutine(HitFreeze());
                StartCoroutine(ImmunityPhase());
            }
        }

        private IEnumerator HitFreeze()
        {
            yield return new WaitForSeconds(freezeStart);

            animSpeed = animator.speed;
            animator.speed = 0f;

            yield return new WaitForSeconds(freezeLength);

            animator.speed = animSpeed;
        }

        private IEnumerator ImmunityPhase()
        {
            Hurt = true;

            for (int i = 0; i < hurtboxes.Count; i++)
                hurtboxes[i].gameObject.layer = (int)Layer.PlayerDynamic;

            yield return new WaitForSeconds(immunityLength);

            for (int i = 0; i < hurtboxes.Count; i++)
                hurtboxes[i].gameObject.layer = (int)Layer.Hurtbox;

            Hurt = false;
        }
        #endregion

        #region Health Damage
        public void TakeDamage(float damage)
        {
            if (characterShield)
                if (characterShield.Shielding)
                    return;

            currentHealth -= damage;

            SetHealthDisplay(currentHealth);

            StartCoroutine(DamageEffect(currentHealth));

            if (currentHealth <= 0)
            {
                for (int i = 0; i < hurtboxes.Count; i++)
                    hurtboxes[i].ResetHurtbox();

                Death(true);
                return;
            }
        }

        private void SetHealthDisplay(float currentHealth)
        {
            Vector3 healthScale = healthVisual.healthMain.transform.localScale;

            healthScale.x = currentHealth / maxHealth;

            healthVisual.healthMain.transform.localScale = healthScale;
        }

        private IEnumerator DamageEffect(float currentHealth)
        {
            float healthVelocity = 0f;

            Transform healthMain = healthVisual.healthMain.transform;
            Transform healthSecondary = healthVisual.healthSecondary.transform;

            while (healthSecondary.localScale.x != healthMain.localScale.x)
            {
                float healthSecondaryX = healthSecondary.localScale.x;
                float healthMainX = healthMain.localScale.x;

                float smoothTime = Mathf.SmoothDamp(healthSecondaryX, healthMainX, ref healthVelocity, 0.5f);

                if (Dead)
                {
                    healthSecondary.localScale = new Vector3(0f, healthSecondary.localScale.y, healthSecondary.localScale.z);
                    yield break;
                }

                healthSecondary.localScale = new Vector3(smoothTime, healthSecondary.localScale.y, healthSecondary.localScale.z);
                yield return null;
            }
        }
        #endregion

        #region Life and Death
        public void Revive(float restoreHealth)
        {
            currentHealth = restoreHealth;
            SetHealthDisplay(currentHealth);
            Dead = false;
        }

        private void Death(bool dead)
        {
            if (Dead == dead)
                return;

            Dead = dead;

            if (DeathEvent != null)
                DeathEvent(this.gameObject);
        }
        #endregion
    }
}