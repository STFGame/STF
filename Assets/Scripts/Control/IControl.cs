using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Utility.Enums;

namespace Controller.Mechanism
{
    public interface IControl
    {
        PlayerNumber PlayerNumber { get; }
        Button GetButton(ButtonType buttonType);
        Lever Lever { get; }
    }
}
