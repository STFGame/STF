using UnityEngine;

namespace UI
{
    /// <summary>
    /// Class that contains info for the buttons that are in the UI
    /// </summary>
    public class ButtonUI : MonoBehaviour, IComponentUI
    {
        //An event that notifies object that it is being hovered
        public delegate void HoverDelegate(bool hovering);
        public event HoverDelegate HoverEvent;

        //An event that notifies objects that it has been pressed
        public delegate void SelectDelegate(bool selected);
        public event SelectDelegate SelectEvent;

        protected bool m_Select = false;
        protected bool m_Hover = false;

        protected Animator m_AnimatorUI = null;

        private void Awake()
        {
            m_AnimatorUI = GetComponent<Animator>();
        }

        public virtual void Select(bool press)
        {
            ExecuteSelect(press);
            AnimatePress();
        }

        public virtual void Hover(bool hover)
        {
            ExecuteHover(hover);
            AnimateHover();
        }

        private void ExecuteSelect(bool select)
        {
            if (m_Select == select)
                return;

            m_Select = select;

            SelectEvent?.Invoke(m_Select);
        }

        private void ExecuteHover(bool hover)
        {
            if (m_Hover == hover)
                return;

            m_Hover = hover;

            HoverEvent?.Invoke(m_Hover);
        }

        protected virtual void AnimateHover()
        {
            m_AnimatorUI.SetBool("Hover", m_Hover);
        }

        protected virtual void AnimatePress()
        {
            m_AnimatorUI.SetBool("Press", m_Select);
        }
    }
}
