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
    /// <summary>
    /// Class that contains info for the buttons that are in the UI
    /// </summary>
    public class UIButton : MonoBehaviour, IUIComponent
    {
        #region UIButton Variables
        //An event that notifies object that it is being hovered
        public delegate void HoverChange(bool hovering);
        public event HoverChange HoverEvent;

        //An event that notifies objects that it has been pressed
        public delegate void PressChange(bool pressed);
        public event PressChange PressEvent;

        private bool press = false;
        private bool hover = false;

        private Animator buttonAnimator = null;
        #endregion

        #region Load
        private void Awake()
        {
            buttonAnimator = GetComponent<Animator>();
        }
        #endregion

        #region Event Methods
        public void Press(bool press, PlayerNumber playerNumber)
        {
            PressUpdate(press);
            AnimatePress();
        }

        public void Hover(bool hover)
        {
            HoverUpdate(hover);
            AnimateHover();
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
        #endregion

        #region Visual FX and Animations
        private void AnimateHover()
        {
            buttonAnimator.SetBool("Hover", hover);
        }

        private void AnimatePress()
        {
            buttonAnimator.SetBool("Press", press);
        }
        #endregion
    }
}
