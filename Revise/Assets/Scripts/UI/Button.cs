using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace UI
{
    public class Button : MonoBehaviour
    {
        [SerializeField] private Scene scene;

        private string sceneName;

        void Start() { sceneName = scene.ToString(); }

        public void LoadScene() { SceneManager.LoadScene(sceneName); }
    }
}