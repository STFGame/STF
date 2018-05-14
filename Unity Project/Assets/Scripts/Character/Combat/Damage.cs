using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Combat
{
    /// <summary>
    /// Class that contains info about damage, knockback, etc that an attack does.
    /// </summary>
    [Serializable]
    public class Damage
    {
        [SerializeField] private float damage = 10f;
        [SerializeField] private float knockback = 10f;
    }
}
