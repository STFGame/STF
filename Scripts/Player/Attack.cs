using System;
using UnityEngine;

namespace Player
{
    [Serializable]
    class Attack
    {
        public float lightAttack;
        public float midAttack;
        public float heavyAttack;

        public Attack()
        {
            lightAttack = 20f;
            midAttack = 30f;
            heavyAttack = 40f;
        }
    }
}
