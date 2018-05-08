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
        [SerializeField] private GameObject healthObject;
        [SerializeField] private GameObject damageObject;

        public float currentHealth;

        public Health()
        {
            currentHealth = maxHealth;
        }

        public void Update()
        {
            healthObject.transform.localScale = new Vector3(currentHealth / maxHealth, healthObject.transform.localScale.y, healthObject.transform.localScale.z);
        }

        public void TakeDamage(float damageAmount)
        {
            if (currentHealth > 0f)
            {
                currentHealth -= damageAmount;
                currentHealth = Mathf.Clamp(currentHealth, 0f, maxHealth);
                Debug.Log(currentHealth);
            }
        }
    }
}
