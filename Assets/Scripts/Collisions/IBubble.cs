using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using Utility.Enums;

/* IBUBBLE
 * Sean Ryan
 * April 5, 2018
 * 
 * IBubble is an interface (contract) that is exposes the properties that are shared
 * in classes that derive from it
 */

namespace Actor.Bubbles
{
    public interface IBubble
    {
        StringBuilder Name { get; set; }
        BodyArea BodyArea { get; set; }
        Vector3 Offset { get; set; }
        Transform Link { get; set; }
        Shape Shape { get; set; }
        BubbleType Type { get; set; }
        float Radius { get; set; }
        Vector3 Size { get; set; }
    }
}
