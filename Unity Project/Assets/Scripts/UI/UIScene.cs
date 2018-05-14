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
    /// <summary>
    /// A class that manages scene transitions.
    /// </summary>
    public class UIScene : MonoBehaviour
    {
        #region UIScene Variables
        //The scene that will be transitioned to
        [SerializeField] private SceneChoice scene = SceneChoice.Quit;

        //The texture image that will fade
        [SerializeField] private Texture2D fadeOutTexture = null;

        //How fast it fades
        [SerializeField] private float fadeSpeed = 0.8f;

        //Sets the draw depth so that it appears in front of everything else
        private int drawDepth = -1000;

        //The alpha percentage of the texture
        private float alpha = 1f;

        //The direction to fade
        private int fadeDirection = -1;
        #endregion

        #region Load
        //Subscribes UIScene from the UIButton
        private void OnEnable()
        {
            GetComponent<UIButton>().PressEvent += Transition;
        }

        //Unsubscribes the UIScene from the UIButton
        private void OnDisable()
        {
            GetComponent<UIButton>().PressEvent -= Transition;
        }
        #endregion

        #region Transition
        //Starts the transition to a new scene.
        private void Transition(bool transition)
        {
            if (transition)
                StartCoroutine(SceneTransition());
        }

        //Coroutine that transitions.
        private IEnumerator SceneTransition()
        {
            if (scene == SceneChoice.None)
                yield break;

            if (scene == SceneChoice.Quit)
                Application.Quit();

            float fadeTime = BeginFade(1);
            yield return new WaitForSeconds(fadeTime);
            SceneManager.LoadScene((int)scene);
        }

        //Begins the fade and returns the fadeSpeed
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
        #endregion
    }
}
