using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine.UI;

namespace Player
{
    [Serializable]
    class Health
    {
        public float health;

        public Image healthBar;

        public Health()
        {
            health = 100f;
            healthBar = null;
        }

        private IEnumerator Routine()
        {
            yield return null;
        }
    }
}
