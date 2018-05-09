using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UI
{
    public class CharacterBox : MonoBehaviour, IUIComponent
    {
        [SerializeField] private string characterName = "Character";
        [SerializeField] private GameObject character = null;
        [SerializeField] private Transform[] spawnPoints = null;
        [SerializeField] private bool canBeSelected = true;

        private GameObject[] characterArr = new GameObject[4];

        private LayerMask layerMask;
        private Animator animator;

        private int[] playerNumbers = new int[4];

        private Vector3 extents;
        private Vector3 center;

        private void Awake()
        {
            animator = GetComponent<Animator>();

            layerMask = (1 << (int)Layer.GUI3D) | (1 << (int)Layer.UI);

            extents = GetComponent<BoxCollider>().size;
            center = GetComponent<BoxCollider>().bounds.center;

            for (int i = 0; i < playerNumbers.Length; i++)
                playerNumbers[i] = -1;
        }

        private void GetCursor()
        {
            RaycastHit hit;
            Debug.DrawRay(transform.position, Vector3.back, Color.red);

            if (Physics.BoxCast(center, transform.position, Vector3.back, out hit, Quaternion.identity, layerMask))
                Debug.Log("HERE I AM");

            PlayerNumber playerNumber = PlayerNumber.None;
            if (Physics.Raycast(transform.position, Vector3.back, out hit, layerMask))
            {
                playerNumber = hit.transform.GetComponent<Cursor>().PlayerNumber;

                int currentIndex = playerNumbers[(int)playerNumber - 1] = (int)playerNumber - 1;

                if (characterArr[currentIndex] == null)
                    characterArr[currentIndex] = Instantiate(character, spawnPoints[currentIndex], false);

                characterArr[currentIndex].SetActive(true);
                characterArr[currentIndex].transform.position = spawnPoints[currentIndex].position;

                return;
            }

            for (int i = 0; i < playerNumbers.Length; i++)
            {
                if (playerNumbers[i] > -1)
                {
                    characterArr[playerNumbers[i]].SetActive(false);
                    playerNumbers[i] = -1;
                }
            }
        }

        public void Press(bool press)
        {
            animator.SetBool("Press", press);
        }

        public void Hover(bool hover)
        {
            //GetCursor();
            animator.SetBool("Hover", hover);
        }
    }
}