using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Utility
{
    [Flags]
    public enum Layer
    {
        Default = 0,
        TransparentFX = 1,
        IgnoreRaycast = 2,
        Water = 4,
        UI = 5,

        PlayerStatic = 8,
        PlayerDynamic = 9,
        HurtBubble = 10,
        HitBubble = 11
    }
}
