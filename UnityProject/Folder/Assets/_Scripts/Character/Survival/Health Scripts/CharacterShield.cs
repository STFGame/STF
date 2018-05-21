using Alerts;
using Survival;
using System.Collections;
using UnityEngine;

namespace Character
{
    /// <summary>
    /// Class responsible for shielding character.
    /// </summary>
    public class CharacterShield : Shield
    {
        public Transform m_ShieldVisual = null;

        [SerializeField] [Range(0f, 2f)] private float m_IncreaseRate = 1f;
        [SerializeField] [Range(0f, 2f)] private float m_DecreaseRate = 1f;
        [SerializeField] private float m_StunLength = 2f;

        private Animator m_ShieldAnimator;

        public bool Stunned { get; private set; }

        private void Awake()
        {
            m_ShieldAnimator = GetComponent<Animator>();

            m_CurrentHealth = m_MaxHealth;
        }

        public override void Execute(bool shield)
        {
            Shielding = (!Stunned) ? shield : false;

            if (Shielding)
                StartCoroutine(ShieldAction());
            else
            {
                if (m_CurrentHealth < m_MaxHealth)
                    m_CurrentHealth += Time.deltaTime * m_IncreaseRate;

            }

            if (m_CurrentHealth <= 0)
                StartCoroutine(ShieldStunAction());

            AnimateShield();
        }

        public override void TakeDamage(float damage)
        {
            if (!Shielding)
                return;

            m_CurrentHealth -= damage;
        }

        private IEnumerator ShieldStunAction()
        {
            Stunned = true;
            yield return new WaitForSeconds(m_StunLength);
            Stunned = false;

            m_CurrentHealth = m_MaxHealth;
        }

        private IEnumerator ShieldAction()
        {
            while (m_CurrentHealth > 0f)
            {
                m_CurrentHealth -= Time.deltaTime * m_DecreaseRate;

                if (!Shielding)
                    break;

                yield return null;
            }

            if (m_CurrentHealth <= 0f)
                yield break;

            m_CurrentHealth = Mathf.Clamp(m_CurrentHealth, 0f, m_MaxHealth);
        }

        private void AnimateShield()
        {
            m_ShieldAnimator.SetBool("Shielding", Shielding);
            m_ShieldAnimator.SetBool("Stunned", Stunned);
        }
    }
}