using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Inputs
{
    public enum MoveInput
    {
        None,

        Up,
        Down,
        Forward,
        Backward,

        Action1,
        Action2,
        Action3,
        Action4,

        LeftBumper,
        RightBumper,

        LeftTrigger,
        RightTrigger,

        LeftStick,
        RightStick,
    }

    public enum InputType
    {
        Press,
        Hold,
        Release
    }

    [Serializable]
    public class Tet
    {
        public int priority;
        public MoveInput moveInput;
        public InputType inputType;
    }
}
