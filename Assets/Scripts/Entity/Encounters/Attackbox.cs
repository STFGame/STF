﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using Utility.Enums;

namespace Actor.Encounters
{
    [Serializable]
    public class Attackbox : Encounter<bool>
    {
        private bool hasHit = false;

        public Attackbox() { }

        public Attackbox(BodyArea bodyArea)
        {
            this.bodyArea = bodyArea;
        }
    }
}
