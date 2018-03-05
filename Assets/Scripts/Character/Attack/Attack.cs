using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Character.CC
{
    public class Attack : MonoBehaviour
    {
        private Animator animator;

        public float damageAmount;

        private float timer;

        private void Awake()
        {
            animator = GetComponent<Animator>();
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Joystick1Button0))
            {
                UpdateAttack(1);
                timer = Time.time;
            }

            if (Time.time - timer > 0.50f)
                UpdateAttack(0);
        }

        private void UpdateAttack(int attack)
        {
            animator.SetInteger("Attack", attack);
        }

        private float Timer()
        {
            float time = Time.time;
            return time;
        }
    }
}
