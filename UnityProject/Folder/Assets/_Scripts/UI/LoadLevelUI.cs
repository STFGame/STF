using Association;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace UI
{
    public class LoadLevelUI : MonoBehaviour
    {
        [SerializeField] private SceneChoice m_SceneChoice = SceneChoice.None;
        [SerializeField] private Image m_FadeOverlay = null;
        [SerializeField] private Image m_LoadingIcon = null;
        [SerializeField] private Text m_ProgressText = null;

        [SerializeField] private Slider m_ProgressSlider = null;

        [SerializeField] private float m_FadeSpeed = 0.5f;
        [SerializeField] private float m_InitialDelay = 0.5f;

        private void Awake()
        {
            m_FadeOverlay.CrossFadeAlpha(0f, 0f, true);
            m_LoadingIcon.gameObject.SetActive(false);
            m_ProgressSlider.gameObject.SetActive(false);
            m_ProgressText.gameObject.SetActive(false);
        }

        //Subscribes UIScene from the UIButton
        private void OnEnable()
        {
            GetComponent<ButtonUI>().SelectEvent += LoadLevel;
        }

        //Unsubscribes the UIScene from the UIButton
        private void OnDisable()
        {
            GetComponent<ButtonUI>().SelectEvent -= LoadLevel;
        }

        public void LoadLevel(bool loadLevel)
        {
            if (loadLevel)
                StartCoroutine(LoadAsync(m_SceneChoice));
        }

        private IEnumerator LoadAsync(SceneChoice sceneChoice)
        {
            if (sceneChoice == SceneChoice.Quit)
                Application.Quit();

            yield return new WaitForSeconds(m_InitialDelay);

            m_FadeOverlay.CrossFadeAlpha(1f, m_FadeSpeed, false);

            yield return new WaitForSeconds(m_FadeSpeed);

            m_LoadingIcon.gameObject.SetActive(true);
            m_ProgressSlider.gameObject.SetActive(true);
            m_ProgressText.gameObject.SetActive(true);

            AsyncOperation operation = SceneManager.LoadSceneAsync((int)sceneChoice);
            while (!operation.isDone)
            {
                float progress = Mathf.Clamp01(operation.progress / 0.9f);
                m_ProgressText.text = (progress * 100f).ToString("00") + "%";

                m_ProgressSlider.value = progress;

                yield return null;
            }
        }
    }
}
