using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace UI
{
    public interface IUIComponent
    {
        void Hover(bool hover);
        void Action(bool action);
    }
}