using Life;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Character.UI
{
    /// <summary>
    /// Class for displaying the health.
    /// </summary>
    public class HealthUI : MonoBehaviour
    {
        //Visuals for displaying the health bar.
        [SerializeField] private Image m_healthForeground;
        [SerializeField] private Image m_healthSecondary;
        [SerializeField] private Image m_healthBackground;
        [SerializeField] private Text m_healthText;

        //Smoothing for the health decrease
        [SerializeField] protected float m_smoothDamp = 0.3f;

        private float m_maxHealth;
        private float m_targetHealth;
        private float m_previousHealth;

        //Initialises the health and subscribes it to a character's health
        public void Initialise(IHealth healthRef)
        {
            m_maxHealth = healthRef.MaxHealth;
            m_previousHealth = m_targetHealth = (m_maxHealth / m_maxHealth);

            m_healthSecondary.fillAmount = m_healthForeground.fillAmount = m_previousHealth;
            m_healthText.text = (healthRef.CurrentHealth + "/" + healthRef.MaxHealth);

            healthRef.HealthChange += (d) =>
            {
                m_targetHealth = ((d) / m_maxHealth);

                StopCoroutine(DecreaseHealth(healthRef.CurrentHealth, m_maxHealth));
                StartCoroutine(DecreaseHealth(healthRef.CurrentHealth, m_maxHealth));
            };
        }

        private IEnumerator DecreaseHealth(float currentHealth, float maxHealth)
        {
            if (currentHealth < 0)
                currentHealth = 0;

            m_healthForeground.fillAmount = m_targetHealth;
            m_healthText.text = string.Format("{0:0}/{1}", currentHealth, m_maxHealth);
            float healthVelocity = 0;
            while (m_previousHealth != m_targetHealth)
            {
                m_previousHealth = Mathf.SmoothDamp(m_previousHealth, m_targetHealth, ref healthVelocity, m_smoothDamp);

                m_healthSecondary.fillAmount = m_previousHealth;

                yield return null;
            }
        }

        public void SetActive(bool isActive)
        {
            gameObject.SetActive(isActive);
        }
    }
}
