using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace UI
{
    /// <summary>
    /// Class responsible for spawning UI cursors.
    /// </summary>
    public class UICursorManager : MonoBehaviour
    {
        //Path of the cursor
        [SerializeField] private string cursorPath = "UI/Cursor";

        //The parent transform of the Cursors
        [SerializeField] private Transform cursorParent = null;

        //An array that holds all of the possible cursors
        private UICursor[] cursors = null;

        //The number of players with connected joysticks
        private int numberOfPlayers = 0;

        private void Awake()
        {
            LoadCursor();
        }

        private void LoadCursor()
        {
            string[] controlNames = Input.GetJoystickNames();
            cursors = new UICursor[controlNames.Length];

            numberOfPlayers = controlNames.Length;

            PlayerNumber[] players = Enum.GetValues(typeof(PlayerNumber)).Cast<PlayerNumber>().ToArray();
            for (int i = 0; i < controlNames.Length; i++)
            {
                GameObject cursor = (GameObject)Instantiate(Resources.Load(cursorPath), cursorParent);
                cursor.GetComponent<UICursor>().LoadDevice(players[i + 1]);

                cursors[i] = cursor.GetComponent<UICursor>();
            }
        }

        public void UpdateManager()
        {
            int activeNumber = 0;
            for (int i = 0; i < numberOfPlayers; i++)
            {
                if (cursors[i].Active)
                    activeNumber++;
            }
        }
    }
}
