using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Survival
{
    [Serializable]
    public class Health
    {
        public Transform healthMain;
        public Transform healthSecondary;
        public Transform healthBackground;

        [SerializeField] private Transform[] healthTokens;

        public void SetParent(Transform parent)
        {
            healthMain.SetParent(parent, false);
            healthSecondary.SetParent(parent, false);
            healthBackground.SetParent(parent, false);
        }
    }
}
