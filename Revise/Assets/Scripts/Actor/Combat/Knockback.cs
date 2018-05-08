using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Actor.Combat
{
    [Serializable]
    public class Knockback
    {
        [SerializeField] private float power;

        public float Power { get { return power; } }
    }
}
