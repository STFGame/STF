using Broadcasts;
using Characters;
using Player.Management;
using System.Collections;
using UI;
using UnityEngine;
using UnityEngine.UI;

namespace Managers
{
    public class GameManager : MonoBehaviour, IBroadcast
    {
        [SerializeField] private Image m_fadeOutImage = null;
        [SerializeField] private GameObject m_textPanel = null;
        [SerializeField] private Text[] m_startText = null;
        [SerializeField] private float m_fadeOutTime = 0.3f;

        private PlayerManager m_playerManager = null;
        private TimeManager m_timeManager = null;
        private LoadLevel m_loadLevel = null;

        private bool m_startSequencePlaying = true;

        public static int CharactersDead;

        private void Awake()
        {
            m_loadLevel = GetComponent<LoadLevel>();

            CharactersDead = 0;
            m_timeManager = GetComponent<TimeManager>();
            m_playerManager = GetComponent<PlayerManager>();

            m_playerManager.Initialise();

            m_fadeOutImage.gameObject.SetActive(true);
            m_fadeOutImage.CrossFadeAlpha(1f, 0f, true);
        }

        private IEnumerator Start()
        {
            MessagePlayer(Broadcasts.BroadcastMessage.Stop);
            m_startSequencePlaying = true;
            m_textPanel.SetActive(true);
            m_fadeOutImage.CrossFadeAlpha(0f, m_fadeOutTime, true);
            yield return new WaitForSeconds(m_fadeOutTime + 0.2f);

            for (int i = 0; i < m_startText.Length; i++)
            {
                m_startText[i].gameObject.SetActive(true);
                yield return new WaitForSeconds(1f);
                m_startText[i].gameObject.SetActive(false);
            }
            m_textPanel.SetActive(false);
            MessagePlayer(Broadcasts.BroadcastMessage.None);
            m_startSequencePlaying = false;
        }

        private void MessagePlayer(BroadcastMessage message)
        {
            for (int i = 0; i < m_playerManager.Length; i++)
            {
                GameObject player = m_playerManager.GetPlayer(i);
                Broadcast.Send<IBroadcast>(player, (x, y) => x.Inform(message));
            }
        }

        private void Update()
        {
            if (m_startSequencePlaying)
                return;

            UpdateTime();

            GameOver();
        }

        private void GameOver()
        {
            if (m_playerManager.Length <= 0)
                return;

            int charactersDead = (CharactersDead % m_playerManager.Length);

            bool gameOver = (charactersDead == 1);

            if (m_timeManager.TimeEnded || gameOver)
            {
                DetermineWinner();
                m_loadLevel.Load();
                return;
            }
        }

        private void DetermineWinner()
        {
            for (int i = 0; i < m_playerManager.Length; i++)
            {
                for (int j = i + 1; j < m_playerManager.Length; j++)
                {
                    int lives = m_playerManager.GetPlayer(i).GetComponent<Death>().NumberOfLives;
                    int lives2 = m_playerManager.GetPlayer(j).GetComponent<Death>().NumberOfLives;

                    PlayerManager.Placement[i] = (lives < lives2) ? ((uint)j + 1) : ((uint)i + 1);
                }
            }
        }

        private void UpdateTime()
        {
            if (!m_timeManager)
                return;

            m_timeManager.Execute();
        }

        public void Inform(BroadcastMessage message) { }
    }
}