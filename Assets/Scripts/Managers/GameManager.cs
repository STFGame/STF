using Character;
using Misc;
using Survival;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace Managers
{
    public class GameManager : MonoBehaviour
    {
        #region GameManager Variables

        #region Time
        [Header("Game Time")]
        [SerializeField] private Text timeText = null;
        [SerializeField] [Range(0, 10)] private int minutes = 2;
        [SerializeField] [Range(0f, 59f)] private float seconds = 0f;

        private float timeAmount = 0f;

        [Header("Respawn Time")]
        [SerializeField] [Range(0f, 10f)] private float respawnTime = 0f;
        #endregion

        #region Spawners
        [SerializeField] private Transform[] startSpawns;
        [SerializeField] private Transform[] respawners;
        #endregion

        #region Character

        #region Manual Spawn
        [SerializeField] private string[] characterNames = null;
        [SerializeField] private bool manualSpawn = false;

        #endregion

        #region Character Health
        [SerializeField] private Health[] health;
        [SerializeField] private Transform[] healthParents;
        #endregion

        #endregion

        private static Dictionary<PlayerNumber, GameObject> characters = new Dictionary<PlayerNumber, GameObject>();
        #endregion

        #region Initialization
        private void Awake()
        {
            InstantiateCharacter();

            timeAmount = ((float)minutes * 60) + seconds;
        }

        private void InstantiateCharacter()
        {
            PlayerNumber[] playerNumbers = Enum.GetValues(typeof(PlayerNumber)).Cast<PlayerNumber>().ToArray();
            //health = new Health[CharacterSelection.Length];
            for (int i = 0; i < CharacterSelection.Length; i++)
            {
                PlayerNumber playerNumber = playerNumbers[i + 1];
                string characterName = null;
                if (!manualSpawn)
                    characterName = CharacterSelection.GetCharacter(playerNumber);
                else
                {
                    if (i < characterNames.Length)
                        characterName = characterNames[i];
                }

                if (characterName == null)
                    continue;

                GameObject character = Instantiate(Resources.Load(characterName), startSpawns[i]) as GameObject;

                character.GetComponent<InputManager>().InitiateDevice(playerNumber);

                health[i].SetParent(healthParents[i]);
                character.GetComponent<CharacterHealth>().InjectHealth(health[i]);

                character.GetComponent<CharacterHealth>().DeathEvent += CharacterDeath;
                Register(playerNumber, character);
            }
        }


        #endregion

        #region Updates
        // Update is called once per frame
        private void Update()
        {
            UpdateTime();

            if (timeAmount <= 0)
                EndGame();

        }

        private void UpdateTime()
        {
            if (timeAmount > 0)
                timeAmount -= Time.deltaTime;

            int minutes = Mathf.FloorToInt(timeAmount / 60);
            int seconds = Mathf.FloorToInt(timeAmount - (minutes * 60));

            timeText.text = string.Format("{0:0}:{1:00}", minutes, seconds);
        }
        #endregion

        #region Misc
        private void EndGame()
        {
            return;
        }

        public static void Register(PlayerNumber key, GameObject player)
        {
            if (!characters.ContainsKey(key))
                characters.Add(key, player);
        }

        public static GameObject GetCharacter(PlayerNumber key)
        {
            if (characters.ContainsKey(key))
                return characters[key];
            return null;
        }
        #endregion

        #region Death and Respawn
        private void CharacterDeath(GameObject character)
        {
            character.SetActive(false);

            StartCoroutine(Respawn(character));
        }

        private IEnumerator Respawn(GameObject character)
        {
            yield return new WaitForSeconds(respawnTime);
            character.SetActive(true);

            character.GetComponent<CharacterHealth>().Revive(100f);
        }
        #endregion
    }
}