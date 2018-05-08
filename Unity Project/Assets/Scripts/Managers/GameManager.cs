using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Managers
{
    public class GameManager : MonoBehaviour
    {
        #region Time
        [SerializeField] private Text timeText = null;
        [SerializeField] [Range(0, 10)] private int minutes = 2;
        [SerializeField] [Range(0f, 59f)] private float seconds = 0f;

        private float timeAmount = 0f;
        #endregion

        private static Dictionary<PlayerNumber, GameObject> players = new Dictionary<PlayerNumber, GameObject>();

        private void Awake()
        {
            timeAmount = ((float)minutes * 60) + seconds;
        }

        // Update is called once per frame
        private void Update()
        {
            UpdateTime();
        }

        private void UpdateTime()
        {
            if (timeAmount > 0)
                timeAmount -= Time.deltaTime;

            int minutes = Mathf.FloorToInt(timeAmount / 60);
            int seconds = Mathf.FloorToInt(timeAmount - (minutes * 60));

            timeText.text = string.Format("{0:0}:{1:00}", minutes, seconds);
        }

        public static void Register(PlayerNumber key, GameObject player)
        {
            if (!players.ContainsKey(key))
                players.Add(key, player);
        }
    }
}