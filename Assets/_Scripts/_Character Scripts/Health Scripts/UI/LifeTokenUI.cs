using Characters;
using Managers;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Character.UI
{
    public class LifeTokenUI : MonoBehaviour
    {
        [SerializeField] private Text m_numberOfLivesText;
        [SerializeField] private Text m_lifeLostText;

        [SerializeField] private Vector3 m_targetPosition;
        [SerializeField] private float m_smoothDamp;

        private Vector3 m_textVelocity;

        public void Initialise(Death deathRef)
        {
            m_lifeLostText.gameObject.SetActive(false);

            m_numberOfLivesText.text = deathRef.NumberOfLives.ToString();

            deathRef.DeathEvent += LostLifeToken;
        }

        private void LostLifeToken(int livesRemaining)
        {
            m_numberOfLivesText.text = livesRemaining.ToString();

            StopCoroutine(LostTokenRoutine(livesRemaining));
            StartCoroutine(LostTokenRoutine(livesRemaining));
        }

        //Makes text appear signifying the loss of a life
        private IEnumerator LostTokenRoutine(int numberOfLives)
        {
            m_lifeLostText.gameObject.SetActive(true);

            Vector3 startPosition = m_lifeLostText.transform.position;
            Vector3 originalPosition = startPosition;
            Vector3 endPosition = startPosition - m_targetPosition;

            m_lifeLostText.transform.position = startPosition;
            while (startPosition != endPosition)
            {
                startPosition = Vector3.SmoothDamp(startPosition, endPosition, ref m_textVelocity, m_smoothDamp);

                m_lifeLostText.transform.position = startPosition;

                yield return null;
            }

            m_lifeLostText.transform.position = originalPosition;
            m_lifeLostText.gameObject.SetActive(false);

            if (numberOfLives <= 0)
                GameManager.CharactersDead++;
        }

        public void SetActive(bool isActive)
        {
            gameObject.SetActive(isActive);
        }
    }
}
