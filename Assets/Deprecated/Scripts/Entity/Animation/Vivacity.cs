using System;
using System.Collections.Generic;
using UnityEngine;

namespace Entity.Animation
{
    [Serializable]
    public class Vivacity
    {
        [SerializeField] private string parameterName;

        private Animator animator;

        public Vivacity() { }

        public void Initialize(Animator animator)
        {
            this.animator = animator;
        }

        public void SetBool(bool value) { animator.SetBool(parameterName, value); }

        public void SetInteger(int value) { animator.SetInteger(parameterName, value); }

        public void SetTrigger() { animator.SetTrigger(parameterName); }

        public void SetFloat(float value) { animator.SetFloat(parameterName, value); }
        public void SetFloat(float value, float smoothing, float time) { animator.SetFloat(parameterName, value, smoothing, Time.deltaTime); }

        #region
        public string ParameterName
        {
            get { return parameterName; }
            set { parameterName = value; }
        }
        #endregion
    }
}
