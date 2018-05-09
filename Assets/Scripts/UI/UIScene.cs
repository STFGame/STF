using Association;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace UI
{
    public class UIScene : MonoBehaviour
    {
        [SerializeField] private SceneChoice scene = SceneChoice.Quit;
        [SerializeField] private Texture2D fadeOutTexture = null;
        [SerializeField] private float fadeSpeed = 0.8f;

        private int drawDepth = -1000;
        private float alpha = 1f;
        private int fadeDirection = -1;

        private void Start()
        {
            GetComponent<UIButton>().PressEvent += Transition;
        }

        private void OnDisable()
        {
            GetComponent<UIButton>().PressEvent -= Transition;
        }

        private void Transition(bool transition)
        {
            if (transition)
                StartCoroutine(SceneTransition());
        }

        private IEnumerator SceneTransition()
        {
            if (scene == SceneChoice.Quit)
                Application.Quit();

            float fadeTime = BeginFade(1);
            yield return new WaitForSeconds(fadeTime);
            SceneManager.LoadScene((int)scene);
        }

        public float BeginFade(int direction)
        {
            fadeDirection = direction;
            return (fadeSpeed);
        }

        private void OnGUI()
        {
            alpha += fadeDirection * fadeSpeed * Time.deltaTime;

            alpha = Mathf.Clamp01(alpha);

            GUI.color = new Color(GUI.color.r, GUI.color.g, GUI.color.b, alpha);
            GUI.depth = drawDepth;
            GUI.DrawTexture(new Rect(0f, 0f, Screen.width, Screen.height), fadeOutTexture);
        }
    }
}
