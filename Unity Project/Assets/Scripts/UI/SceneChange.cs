using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace UI
{
    public class SceneChange : MonoBehaviour
    {
        [SerializeField] private Texture2D fadeOutTexture = null;
        [SerializeField] private float fadeSpeed = 0.8f;

        private int drawDepth = -1000;
        private float alpha = 1f;
        private int fadeDirection = -1;

        private void OnGUI()
        {
            alpha += fadeDirection * fadeSpeed * Time.deltaTime;

            alpha = Mathf.Clamp01(alpha);

            GUI.color = new Color(GUI.color.r, GUI.color.g, GUI.color.b, alpha);
            GUI.depth = drawDepth;
            GUI.DrawTexture(new Rect(0f, 0f, Screen.width, Screen.height), fadeOutTexture);
        }

        public float BeginFade(int direction)
        {
            fadeDirection = direction;
            return (fadeSpeed);
        }

        private void OnLevelWasLoaded(int level)
        {
            BeginFade(-1);
        }
    }
}
