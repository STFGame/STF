using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

namespace Actor.Survivability
{
    public class Survival
    {
        [SerializeField] protected float health;
        [SerializeField] protected Image healthImage;

        protected float currentHealth;
    }
}
