using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Controls
{
    [Serializable]
    public struct ButtonSetup
    {
        public int key;
        public int number;

        public ButtonSetup(int key, int number)
        {
            this.key = key;
            this.number = number;
        }
    }
}
