using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Combat
{
    [Serializable]
    public class AttackEvent
    {
        [SerializeField] private bool enable;
        [SerializeField] private BoxArea boxArea;
    }
}
