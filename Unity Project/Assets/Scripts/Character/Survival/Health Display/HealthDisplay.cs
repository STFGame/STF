using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Survival
{
    [Serializable]
    public abstract class HealthDisplay
    {
        public abstract void Load(Transform parent);
        public abstract void DecreaseHealth(float currentHealth, ref float previousHealth, float maxHealth);
    }
}
