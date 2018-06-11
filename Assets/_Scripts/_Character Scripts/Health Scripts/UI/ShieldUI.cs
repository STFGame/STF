using System;
using Life;
using UnityEngine;
using UnityEngine.UI;

namespace Character.UI
{
    /// <summary>
    /// Class that handles the visuals of shielding
    /// </summary>
    public class ShieldUI : MonoBehaviour
    {
        [SerializeField] private Image m_shieldImage = null;

        private Shield m_shield = null;

        public void Initialise(Shield shieldRef)
        {
            m_shield = shieldRef;

            m_shield.HealthChange += ShieldEffect;
        }

        private void ShieldEffect(float currentShield)
        {
            m_shieldImage.fillAmount = currentShield / m_shield.MaxHealth;
        }

        public void SetActive(bool isActive)
        {
            gameObject.SetActive(isActive);
        }
    }
}
