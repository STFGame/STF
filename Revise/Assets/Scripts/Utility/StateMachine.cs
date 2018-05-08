using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Utility
{
    public enum StateMachine
    {
        Idle,
        Sneak,
        Walk,
        Run,
        Dash,

        Crouch,
        CrouchWalk,

        Jump,
        Fall,
        Land,

        Roll,
        Block,
        Dodge,

        Stun
    }
}
