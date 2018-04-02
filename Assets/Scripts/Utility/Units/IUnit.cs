using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Actor.Components
{
    public interface IUnit
    {
        Animator Animator { get; set; }
        Rigidbody Rigidbody { get; set; }
        Transform Transform { get; set; }
        Collider Collider { get; set; }
    }
}
