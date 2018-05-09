using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace UI
{
    public class UIManager : MonoBehaviour
    {
        [SerializeField] private Transform cursorParent = null;
        [SerializeField] private GameObject[] platforms = null;

        private Cursor[] cursors = null;

        private void Awake()
        {
            CursorInstantiation();

            PlatformInitation();
        }

        private void PlatformInitation()
        {
            for (int i = 0; i < platforms.Length; i++)
                platforms[i].SetActive(false);
        }

        private void CursorInstantiation()
        {
            string[] controlNames = Input.GetJoystickNames();
            cursors = new Cursor[controlNames.Length];

            PlayerNumber[] players = Enum.GetValues(typeof(PlayerNumber)).Cast<PlayerNumber>().ToArray();
            for (int i = 0; i < controlNames.Length; i++)
            {
                PlayerNumber s = players[i + 1];
                GameObject cursor = (GameObject)Instantiate(Resources.Load("UI/Cursor"));
                cursor.transform.SetParent(cursorParent, false);
                cursor.GetComponent<Cursor>().InitiateDevice(players[i + 1]);

                cursors[i] = cursor.GetComponent<Cursor>();
            }
        }

        private void Update()
        {
            //UpdateCursor();
        }

        private void UpdateCursor()
        {
            if (platforms == null)
                return;

            if (cursors.Length == 1)
                return;

            for (int i = 0; i < cursors.Length; i++)
                if (cursors[i].Active)
                    platforms[i].SetActive(true);
        }
    }
}
