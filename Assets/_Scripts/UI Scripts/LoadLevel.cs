using Managers;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace UI
{
    /// <summary>
    /// Class that loads different levels.
    /// </summary>
    public class LoadLevel : MonoBehaviour
    {
        public enum SceneChoice { MainMenu, Play, Victory, Quit, None }

        [SerializeField] private SceneChoice m_sceneChoice = SceneChoice.None;
        [SerializeField] private GameObject m_sceneLoadPanel = null;
        [SerializeField] private Image m_fadeOverlay = null;
        [SerializeField] private Image m_loadingIcon = null;
        [SerializeField] private Text m_progressText = null;
        [SerializeField] private Slider m_progressSlider = null;
        [SerializeField] private float m_fadeSpeed = 0.5f;
        [SerializeField] private float m_initialDelay = 0.5f;

        private void Awake()
        {
            if (m_sceneChoice != SceneChoice.Quit)
            {
                m_sceneLoadPanel.SetActive(false);
                m_fadeOverlay.CrossFadeAlpha(0f, 0f, true);
                m_loadingIcon.gameObject.SetActive(false);
                m_progressSlider.gameObject.SetActive(false);
                m_progressText.gameObject.SetActive(false);
            }
        }

        public void Load()
        {
            if (m_sceneChoice == SceneChoice.Quit)
            {
                Debug.Log("Quit");
                Application.Quit();
                return;
            }

            m_sceneLoadPanel.SetActive(true);
            Time.timeScale = 1f;
            StartCoroutine(LoadAsync(m_sceneChoice));
        }

        private IEnumerator LoadAsync(SceneChoice sceneChoice)
        {
            yield return new WaitForSeconds(m_initialDelay);

            m_fadeOverlay.CrossFadeAlpha(1f, m_fadeSpeed, false);

            yield return new WaitForSeconds(m_fadeSpeed);

            m_loadingIcon.gameObject.SetActive(true);
            m_progressSlider.gameObject.SetActive(true);
            m_progressText.gameObject.SetActive(true);

            AsyncOperation operation = SceneManager.LoadSceneAsync((int)sceneChoice);
            while (!operation.isDone)
            {
                float progress = Mathf.Clamp01(operation.progress / 0.9f);
                m_progressText.text = (progress * 100f).ToString("00") + "%";

                m_progressSlider.value = progress;

                yield return null;
            }
        }
    }
}
