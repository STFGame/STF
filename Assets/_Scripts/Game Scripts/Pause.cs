using Broadcasts;
using Controls;
using System.Collections;
using System.Collections.Generic;
using UI;
using UnityEngine;

namespace Managers
{
    public class Pause : MonoBehaviour
    {
        [SerializeField] private CursorInputModule m_cursorInputModule;
        [SerializeField] private GameObject m_pausePanel;

        public static bool IsPaused { get; private set; }

        private Device[] m_devices;
        private List<UI.Cursor> m_cursors = new List<UI.Cursor>();

        public void Inform(BroadcastMessage message) { }

        private void Awake()
        {
            IsPaused = false;
            m_cursors = m_cursorInputModule.CursorList;
        }

        private void Start()
        {
            m_pausePanel.SetActive(IsPaused);

            m_devices = new Device[InputManager.Length];

            for (int i = 0; i < m_devices.Length; i++)
                m_devices[i] = InputManager.GetDevice(i);
        }

        private void Update()
        {
            for (int i = 0; i < m_devices.Length; i++)
            {
                if (m_devices[i].Start.Press)
                {
                    StopCoroutine(PauseMethod());
                    StartCoroutine(PauseMethod());
                }
                m_cursors[i].SetActive(IsPaused);
            }
        }

        public void Unpause()
        {
            StopCoroutine(PauseMethod());
            StartCoroutine(PauseMethod());
        }

        private IEnumerator PauseMethod()
        {
            yield return new WaitForEndOfFrame();
            IsPaused = !IsPaused;
            m_pausePanel.SetActive(IsPaused);
            Time.timeScale = (IsPaused) ? 0 : 1;
        }
    }
}
