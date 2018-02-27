using Player.CC;
using System;
using UnityEngine;

namespace Player.Animation
{
    public class Anima : MonoBehaviour
    {
        #region Parameters
        [SerializeField] string xAxisParam;
        [SerializeField] string yAxisParam;
        [SerializeField] string dashParam;
        [SerializeField] string turnParam;
        [SerializeField] string isGroundedParam;
        #endregion

        #region Animator Settings
        [SerializeField] float dampTime;
        #endregion

        public Controls Controls;

        Animator animator;
        float updateXAxis;

        private void Start()
        {
            animator = this.GetComponent<Animator>();
        }

        private void Update()
        {
            updateXAxis = new Vector3(0f, Controls.Joystick.JStick().x, 0f).magnitude;

            animator.SetFloat(xAxisParam, updateXAxis, dampTime, Time.deltaTime);
            animator.SetBool(dashParam, Controls.Joystick.Rapid(0.05f));

            animator.SetBool(turnParam, Controls.Joystick.Pivot(this.transform));
        }
    }
}
