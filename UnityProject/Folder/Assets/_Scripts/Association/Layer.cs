using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public enum Layer
{
    Default = 0,
    TransparentFX = 1,
    IgnoreRaycast = 2,
    Water = 4,
    UI = 5,
    PostProcessing = 8,
    PlayerStatic = 9,
    PlayerDynamic = 10,
    Hurtbox = 11,
    Hitbox = 12,
    GUI3D = 13,
    Dead = 14
}
