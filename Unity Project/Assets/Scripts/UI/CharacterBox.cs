using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UI
{
    public class CharacterBox : MonoBehaviour, IUIComponent
    {
        [SerializeField] private GameObject character;

        private LayerMask layerMask;
        private Animator animator;

        private void Awake()
        {
            animator = GetComponent<Animator>();

            layerMask = (1 << (int)Layer.GUI3D) | (1 << (int)Layer.UI);
        }

        private void Update()
        {
            RaycastHit hit;
            Debug.DrawRay(transform.position, Vector3.back, Color.red);
            if(Physics.Raycast(transform.position, Vector3.back, out hit, layerMask))
            {
                Debug.Log(GetComponentInParent<Cursor>().PlayerNumber);
            }
        }

        public void Action(bool action)
        {
            animator.SetBool("Action", action);
        }

        public void Hover(bool hover)
        {
            animator.SetBool("Hover", hover);
        }
    }
}