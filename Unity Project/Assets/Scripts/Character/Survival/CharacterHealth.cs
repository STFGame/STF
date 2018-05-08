using Boxes;
using Managers;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Character
{
    /// <summary>
    /// Health MonoBehaviour that controls how much health the character has
    /// </summary>
    public class CharacterHealth : MonoBehaviour, IHealth
    {
        [SerializeField] private GameObject healthMain = null;
        [SerializeField] private GameObject healthSecondary = null;

        [SerializeField] private float maxHealth = 100f;
        [SerializeField] [Range(0f, 1f)] private float freezeStart = 0.2f;
        [SerializeField] [Range(0f, 1f)] private float freezeLength = 1f;
        [SerializeField] [Range(0f, 1f)] private float hurtImmunity = 1f;

        private float currentHealth;

        private List<Hurtbox> hurtboxes = null;

        private CharacterShield characterShield = null;
        private new Rigidbody rigidbody;
        private Animator animator;

        private float animSpeed = 0f;

        public bool Hurt { get; private set; }
        public bool Dead { get; private set; }

        #region Initialization
        private void Awake()
        {
            characterShield = GetComponent<CharacterShield>();
            rigidbody = GetComponent<Rigidbody>();
            animator = GetComponent<Animator>();

            currentHealth = maxHealth;
        }

        private void Start()
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

            for (int i = 0; i < hurtboxes.Count; i++)
                print(hurtboxes[i]);
        }

        private void OnDisable()
        {
            for (int i = 0; i < hurtboxes.Count; i++)
                hurtboxes[i].HurtEvent -= UpdateHitIndex;
        }
        #endregion

        #region Hurt Methods
        private void UpdateHitIndex(int index)
        {
            animator.SetInteger("Hit", index);

            if (index > 0)
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

            yield return new WaitForSeconds(hurtImmunity);

            for (int i = 0; i < hurtboxes.Count; i++)
                hurtboxes[i].gameObject.layer = (int)Layer.Hurtbox;

            Hurt = false;
        }
        #endregion

        #region Taking Damage
        public void TakeDamage(float damage)
        {
            if (characterShield != null)
            {
                if (characterShield.Shielding)
                {
                    characterShield.TakeDamage(damage);
                    return;
                }

                if (currentHealth <= 0)
                {
                    Dead = true;
                    return;
                }

                currentHealth -= damage;

                healthMain.transform.localScale = new Vector3(currentHealth / maxHealth, healthMain.transform.localScale.y, healthMain.transform.localScale.z);

                StartCoroutine(DamageEffect(currentHealth));
            }
        }

        private IEnumerator DamageEffect(float currentHealth)
        {
            float healthVelocity = 0f;

            while (healthSecondary.transform.localScale.x != healthMain.transform.localScale.x)
            {
                float healthSecondaryX = healthSecondary.transform.localScale.x;
                float healthMainX = healthMain.transform.localScale.x;

                float smoothTime = Mathf.SmoothDamp(healthSecondaryX, healthMainX, ref healthVelocity, 0.5f);

                healthSecondary.transform.localScale = new Vector3(smoothTime, healthSecondary.transform.localScale.y, healthSecondary.transform.localScale.z);
                yield return null;
            }
        }
        #endregion

        public void Revive(float health)
        {

        }
    }
}