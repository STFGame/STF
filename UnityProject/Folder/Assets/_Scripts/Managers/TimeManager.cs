using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Timers;
using UnityEngine;
using UnityEngine.UI;

namespace Managers
{
    /// <summary>
    /// Class that manages time in the game scene.
    /// </summary>
    public class TimeManager : MonoBehaviour
    {
        [Header("Game Time")]
        [SerializeField] private Text gameTimeText = null;
        [SerializeField] [Range(0, 10)] private int minutes = 1;
        [SerializeField] [Range(0f, 59f)] private float seconds = 0f;

        [SerializeField] [Range(0.1f, 1f)] private float slowTime = 0f;

        private float timeLeft = 0f;

        public bool TimeEnded { get; private set; }

        private void Awake()
        {
            timeLeft = ((float)minutes * 60) + seconds;
        }

        public void Execute()
        {
            Time.timeScale = slowTime;

            TimeEnded = (timeLeft <= 0);

            if (!TimeEnded)
                timeLeft -= Time.deltaTime;

            int minutes = Mathf.FloorToInt(timeLeft / 60);
            int seconds = Mathf.FloorToInt(timeLeft - (minutes * 60));

            gameTimeText.text = string.Format("{0:0}:{1:00}", minutes, seconds);
        }
    }
}
