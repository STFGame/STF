using Misc;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UI
{
    public class UISelectionWindow : MonoBehaviour, IUIComponent
    {
        [SerializeField] private string characterName = "Character";
        [SerializeField] private bool canBeSelected = true;

        private LayerMask layerMask;
        private Animator animator;

        private void Awake()
        {
            animator = GetComponent<Animator>();

            layerMask = (1 << (int)Layer.GUI3D) | (1 << (int)Layer.UI);
        }

        public void Press(bool press, PlayerNumber playerNumber)
        {
            animator.SetBool("Press", press);

            if (press)
                GameSettings.SetCharacter(characterName, playerNumber);
        }

        public void Hover(bool hover)
        {
            animator.SetBool("Hover", hover);
        }
    }
}