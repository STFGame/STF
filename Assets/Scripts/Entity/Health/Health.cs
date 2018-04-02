using UnityEngine;
using UnityEngine.UI;

namespace Actor
{
    public class Health : MonoBehaviour
    {
        public float maxHealth = 100f;
        public float currentHealth;

        public Image healthImage;
        private Color color;

        private void Awake()
        {
            currentHealth = maxHealth;
            color = healthImage.color;
        }

        private void Update()
        {
            healthImage.fillAmount = Mathf.Clamp((currentHealth / maxHealth), 0f, maxHealth);
        }
    }
}
