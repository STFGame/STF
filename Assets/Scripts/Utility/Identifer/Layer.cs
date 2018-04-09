using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Utility.Identifer
{
    public static class Layer
    {
        static readonly int Default = 0;
        static readonly int TransparentFX = 1;
        static readonly int IgnoreRaycast = 2;
        static readonly int Water = 4;
        static readonly int UI = 5;
        static readonly int PlayerOne = 8;
        static readonly int PlayerTwo = 9;
        static readonly int PlayerThree = 10;
        static readonly int PlayerFour = 11;
    }
}
