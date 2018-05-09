using Association;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace UI
{
    public class UIButton : MonoBehaviour, IUIComponent
    {
        public delegate void HoverChange(bool hovering);
        public event HoverChange HoverEvent;

        public delegate void PressChange(bool pressed);
        public event PressChange PressEvent;

        private bool press = false;
        private bool hover = false;

        public void Press(bool press)
        {
            PressUpdate(press);
        }

        public void Hover(bool hover)
        {
            HoverUpdate(hover);
        }

        private void PressUpdate(bool press)
        {
            if (press == this.press)
                return;

            this.press = press;

            if (PressEvent != null)
                PressEvent(this.press);
        }

        private void HoverUpdate(bool hover)
        {
            if (this.hover == hover)
                return;

            this.hover = hover;

            if (HoverEvent != null)
                HoverEvent(this.hover);
        }
    }
}
