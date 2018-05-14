using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Survival
{
    /// <summary>
    /// A Scriptable Object that contains health "skin" objects. 
    /// </summary>
    //[CreateAssetMenu(fileName = "Health Regular", menuName = "Health", order = 4)]
    [Serializable]
    public class HealthRegular : HealthDisplay
    {
        #region HealthRegular Variables
        //Paths for the health resources.
        #region Tooltip
        [Tooltip("Prefabs are loaded from the Resources folder. If there is a\n" +
            "nested folder inside then you must include it's name in the path.")]
        #endregion
        //The main health display. Usually the "green" part.
        [SerializeField] private GameObject healthMain = null;

        //Secondary health is the damage health that is slower to decrease.
        [SerializeField] private GameObject healthSecondary = null;

        //The background of the health
        [SerializeField] private GameObject healthBackground = null;

        //Smooth damping for the secondary health display
        [SerializeField] [Range(0f, 1f)] private float smoothDamp = 0.5f;

        //Assists with the smooth damping for the secondary health
        private float healthVelocity = 0f;

        //Animators for the health
        private Animator mainAnimator = null;
        private Animator secondaryAnimator = null;
        #endregion

        #region Load
        //Instantiates the health visuals and sets the GameObjects equal to them.
        //Also sets the children in the correct order.
        public override void Load(Transform parent)
        {
            if (parent == null)
                parent.position = Vector3.zero;

            healthMain = UnityEngine.Object.Instantiate(healthMain, parent, false) as GameObject;
            healthSecondary = UnityEngine.Object.Instantiate(healthSecondary, parent, false) as GameObject;
            healthBackground = UnityEngine.Object.Instantiate(healthBackground, parent, false) as GameObject;

            healthMain.transform.SetSiblingIndex(0);
            healthSecondary.transform.SetSiblingIndex(1);
            healthBackground.transform.SetSiblingIndex(2);

            mainAnimator = healthMain.GetComponent<Animator>();
            secondaryAnimator = healthSecondary.GetComponent<Animator>();
        }
        #endregion

        #region Methods
        //Decreases the health by the specified amount
        public override void DecreaseHealth(float currentHealth, ref float previousHealth, float maxHealth)
        {
            float targetHealth = (currentHealth / maxHealth);

            Decrease(mainAnimator, targetHealth);

            previousHealth = Mathf.SmoothDamp(previousHealth, targetHealth, ref healthVelocity, smoothDamp);

            Decrease(secondaryAnimator, previousHealth);
        }

        //Animates the health decrease
        private void Decrease(Animator healthAnimator, float currentHealth)
        {
            healthAnimator.SetFloat("Current Health", currentHealth);
        }
        #endregion
    }
}
