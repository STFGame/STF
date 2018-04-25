using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Controls
{
    [Serializable]
    public struct ControlIdentity
    {
        public int key;
        public int number;

        public ControlIdentity(int key, int number)
        {
            this.key = key;
            this.number = number;
        }
    }
}
