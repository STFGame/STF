using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

namespace Actor.Survivability
{
    [Serializable]
    public class Health
    {
        [SerializeField] private float maxHealth = 100f;
        [SerializeField] private Image healthImage;

        private float currentHealth;

        public Health()
        {
            currentHealth = maxHealth;
        }

        public void TakeDamage(float damageAmount)
        {
            if (currentHealth > 0f)
            {
                currentHealth -= damageAmount;
                currentHealth = Mathf.Clamp(currentHealth, 0f, maxHealth);
            }
        }
    }
}
