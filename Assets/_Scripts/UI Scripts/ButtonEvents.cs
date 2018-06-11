using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace UI
{
    public enum SceneLoad { MainMenu, CharacterSelect, Play, Quit }

    public class ButtonEvents : MonoBehaviour
    {
        [SerializeField] private float m_speed;
        [SerializeField] private float m_smoothTime;

        private Button m_button;

        // Use this for initialization
        private void Awake()
        {
            m_button = GetComponent<Button>();
        }

        public void FadeIn(CanvasGroup menuGroup)
        {
            StopCoroutine(MenuRoutine(0f, menuGroup, false));
            StartCoroutine(MenuRoutine(1f, menuGroup, true));
        }

        public void FadeOut(CanvasGroup menuGroup)
        {
            StopCoroutine(MenuRoutine(0f, menuGroup, false));
            StartCoroutine(MenuRoutine(0f, menuGroup, false));
        }

        private IEnumerator MenuRoutine(float alphaValue, CanvasGroup menuGroup, bool active)
        {
            yield return new WaitForSeconds(0.1f);
            if (active)
                menuGroup.gameObject.SetActive(true);

            float currentVelocity = 0f;
            while (menuGroup.alpha != alphaValue)
            {
                float fadeValue = Mathf.SmoothDamp(menuGroup.alpha, alphaValue, ref currentVelocity, m_smoothTime, m_speed, Time.unscaledTime);
                menuGroup.alpha = fadeValue;

                yield return null;
            }

            if(!active)
                menuGroup.gameObject.SetActive(false);
        }
    }
}