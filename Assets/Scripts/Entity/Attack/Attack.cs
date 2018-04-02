using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Actor
{
    [RequireComponent(typeof(Animator))]
    public class Attack : MonoBehaviour
    {
        public float damage;

        private Animator anim;

        // Use this for initialization
        void Awake()
        {
            anim = GetComponent<Animator>();
        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Joystick1Button0))
                anim.SetInteger("Attacks", 1);
            else
                anim.SetInteger("Attacks", 0);
        }
    }
}