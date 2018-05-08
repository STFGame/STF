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
    public class UIButton : MonoBehaviour, IUIComponent
    {
        [SerializeField] private STFScene scene;

        SceneChange sceneChange;

        private void Awake()
        {
            sceneChange = GetComponent<SceneChange>();
        }

        public void Action(bool action)
        {
            if (!action)
                return;

            if (scene != STFScene.Quit)
                StartCoroutine(SceneTransition());

            Debug.Log("Pressed me!");
        }

        public void Hover(bool hover)
        {
            Debug.Log("Hovering me!");
        }

        private IEnumerator SceneTransition()
        {
            float fadeTime = sceneChange.BeginFade(1);
            yield return new WaitForSeconds(fadeTime);
            SceneManager.LoadScene((int)scene);
        }
    }
}
