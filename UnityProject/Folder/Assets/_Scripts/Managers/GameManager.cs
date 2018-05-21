using Spawners;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Managers
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private Image m_FadeOutImage = null;
        [SerializeField] private float m_FadeOutTime = 0.3f;

        private Spawner[] m_Spawners = null;
        private TimeManager m_TimeManager = null;

        private void Awake()
        {
            m_TimeManager = GetComponent<TimeManager>();
            m_Spawners = GetComponents<Spawner>();

            for (int i = 0; i < m_Spawners.Length; i++)
                m_Spawners[i].Initialise();

            m_FadeOutImage.CrossFadeAlpha(1f, 0f, true);
        }

        private IEnumerator Start()
        {
            m_FadeOutImage.CrossFadeAlpha(0f, m_FadeOutTime, false);
            yield return new WaitForSeconds(m_FadeOutTime + 0.2f);
        }

        private void Update()
        {
            for (int i = 0; i < m_Spawners.Length; i++)
                m_Spawners[i].Execute();

            UpdateTime();
        }

        private void UpdateTime()
        {
            if (!m_TimeManager)
                return;

            m_TimeManager.Execute();
        }
    }
}