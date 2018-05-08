using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Inputs
{
    [CreateAssetMenu(fileName ="InputAction", menuName = "Action", order = 4)]
    public class InputAction : ScriptableObject
    {
        public Tet[] tet;
    }
}
