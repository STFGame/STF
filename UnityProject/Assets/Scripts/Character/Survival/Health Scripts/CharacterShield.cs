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
        #region CharacterShield Variables
        public Transform shieldVisual = null;

        [SerializeField] [Range(0f, 2f)] private float increaseRate = 1f;
        [SerializeField] [Range(0f, 2f)] private float decreaseRate = 1f;
        [SerializeField] private float stunLength = 2f;

        private Animator animator;

        public bool Shielding { get; private set; }
        public bool Stunned { get; private set; }
        #endregion

        #region Load
        private void Awake()
        {
            animator = GetComponent<Animator>();

            m_CurrentHealth = m_MaxHealth;
        }

        #endregion

        #region Update
        public override void Tick(bool shield)
        {
            if (m_Alert == AlertValue.Dead)
                return;

            Shielding = (!Stunned) ? shield : false;

            if (Shielding)
                StartCoroutine(ShieldAction());

            if (m_CurrentHealth <= 0)
                StartCoroutine(ShieldStunAction());

            ShieldAlert();

            AnimateShield();
        }
        #endregion

        #region Damage
        public override void TakeDamage(float damage)
        {
            if (!Shielding)
                return;

            m_CurrentHealth -= damage;
        }
        #endregion

        #region Shield Methods
        private IEnumerator ShieldStunAction()
        {
            Stunned = true;
            yield return new WaitForSeconds(stunLength);
            Stunned = false;

            m_CurrentHealth = m_MaxHealth;
        }

        private IEnumerator ShieldAction()
        {
            while (m_CurrentHealth > 0f)
            {
                m_CurrentHealth -= Time.deltaTime * decreaseRate;

                if (!Shielding)
                    break;

                yield return null;
            }

            if (m_CurrentHealth <= 0f)
                yield break;

            if (m_CurrentHealth != m_MaxHealth && !Stunned)
            {
                while (m_CurrentHealth < m_MaxHealth)
                {
                    m_CurrentHealth += Time.deltaTime * increaseRate;

                    if (Shielding)
                        break;

                    yield return null;
                }
            }

            m_CurrentHealth = Mathf.Clamp(m_CurrentHealth, 0f, m_MaxHealth);
        }
        #endregion

        #region Visual FX and Animations
        private void AnimateShield()
        {
            animator.SetBool("Shielding", Shielding);
            animator.SetBool("Stunned", Stunned);
        }
        #endregion

        #region Alerts 
        private void ShieldAlert()
        {
            if (Shielding && m_Alert != AlertValue.Shielding)
                Alert.Send<IAlert>(gameObject, (x, y) => x.Inform(AlertValue.Shielding));
            else if (Stunned && m_Alert != AlertValue.Stunned)
                Alert.Send<IAlert>(gameObject, (x, y) => x.Inform(AlertValue.Stunned));
        }

        public override void Inform(AlertValue alert)
        {
            m_Alert = alert;
        }
        #endregion
    }
}