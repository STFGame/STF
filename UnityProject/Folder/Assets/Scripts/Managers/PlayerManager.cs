using Character;
using Alerts;
using Misc;
using Survival;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Managers
{
    /// <summary>
    /// Class that manages the loading of the character.
    /// </summary>
    public class PlayerManager : MonoBehaviour
    {
        #region PlayerManager Variables
        [Header("Manual Spawning")]
        //Can enter character names to manual spawn characters
        [SerializeField] private string[] characterNames = null;

        //Boolean to indciate whether to perform a manual spawn or not
        [SerializeField] private bool manualSpawn = false;

        [Header("UI Health Spawning")]
        //The parents of the health objects
        [SerializeField] private Transform[] healthParent = null;

        //The parents of the life tokens
        [SerializeField] private Transform[] healthTokenParent = null;

        //A dictionary to hold all of the players
        private static Dictionary<PlayerNumber, GameObject> characterDictionary = null;

        //The size of the dictionary
        public static int Count { get { return characterDictionary.Count; } }

        private AlertValue m_Message = AlertValue.None;
        #endregion

        #region Load
        //Fills the dictionary with all of the characters
        private void Awake()
        {
            characterDictionary = new Dictionary<PlayerNumber, GameObject>();
            if (!manualSpawn)
            {
                Debug.Log(GameSettings.NumberOfCharacters);
                characterNames = new string[GameSettings.NumberOfCharacters];
                for (int i = 0; i < characterNames.Length; i++)
                    characterNames[i] = GameSettings.GetCharacter(i);
            }

            LoadCharacter();
        }

        //Instantiates the character with the value matching their player number
        private void LoadCharacter()
        {
            PlayerNumber[] playerNumbers = Enum.GetValues(typeof(PlayerNumber)).Cast<PlayerNumber>().ToArray();
            for (int i = 0; i < characterNames.Length; i++)
            {
                PlayerNumber playerNumber = playerNumbers[i + 1];

                if (i >= GetComponent<SpawnManager>().spawnPoints.Length)
                    break;

                if (characterNames[i] == null)
                    return;

                GameObject character = Instantiate(Resources.Load(characterNames[i]), GetComponent<SpawnManager>().spawnPoints[i]) as GameObject;

                SetDevice(character, playerNumber);
                SetHealth(character, playerNumber);

                CharacterRegister(playerNumber, character);
            }
        }

        //Creates the device of the player
        private void SetDevice(GameObject character, PlayerNumber playerNumber)
        {
            character.GetComponent<Character.CharacterController>().CreateDevice(playerNumber);
        }

        //Sets the health visuals of the player
        private void SetHealth(GameObject character, PlayerNumber playerNumber)
        {
            character.GetComponent<CharacterHealth>().Load(healthParent[(int)playerNumber - 1]);
        }
        #endregion

        #region Update
        public void UpdatePlayerManager()
        {
            if (m_Message == AlertValue.Dead)
                Debug.Log("Dead!");
        }
        #endregion

        #region Registration and Getters
        //Registers the character into the dictionary
        public static void CharacterRegister(PlayerNumber key, GameObject character)
        {
            if (!characterDictionary.ContainsKey(key))
                characterDictionary.Add(key, character);
        }

        //Returns the character from the dictionary if it exists
        public static GameObject GetCharacter(PlayerNumber key)
        {
            if (characterDictionary.ContainsKey(key))
                return characterDictionary[key];
            return null;
        }
        #endregion

        private void Recieve(AlertValue message)
        {
            m_Message = message;
        }
    }
}
