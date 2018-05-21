using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Survival
{
    public abstract class LifeTokenDisplay : ScriptableObject
    {
        public abstract void Load(Transform parent);
        public abstract void Destroy(bool destroy);
    }
}
