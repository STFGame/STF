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
        [SerializeField] private Text m_gameTimeText = null;
        [SerializeField] [Range(0, 10)] private int m_minutes = 1;
        [SerializeField] [Range(0f, 59f)] private float m_seconds = 0f;

        [SerializeField] [Range(0.1f, 1f)] private float m_slowTime = 1f;

        private float m_timeLeft = 0f;

        public bool TimeEnded { get; private set; }

        private void Awake()
        {
            m_timeLeft = ((float)m_minutes * 60) + m_seconds;
        }

        public void Execute()
        {
            //Time.timeScale = m_slowTime;

            TimeEnded = (m_timeLeft <= 0);

            if (!TimeEnded)
                m_timeLeft -= Time.deltaTime;
            else
                m_timeLeft = 0f;

            m_minutes = Mathf.FloorToInt(m_timeLeft / 60);
            m_seconds = Mathf.FloorToInt(m_timeLeft - (m_minutes * 60));

            m_gameTimeText.text = string.Format("{0:0}:{1:00}", m_minutes, m_seconds);
        }
    }
}
