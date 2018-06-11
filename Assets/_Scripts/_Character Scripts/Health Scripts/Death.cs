using Broadcasts;
using Life;
using System.Collections;
using UnityEngine;

namespace Characters
{
    /// <summary>
    /// Class that handles death.
    /// </summary>
    [RequireComponent(typeof(IHealth))]
    public class Death : MonoBehaviour, IBroadcast
    {
        [SerializeField] private int m_numberOfLives = 2;

        private Animator m_deathAnimator;

        public delegate void DeathDelegate(int numberOfLives);
        public event DeathDelegate DeathEvent;

        public int NumberOfLives { get { return m_numberOfLives; } }
        public bool Dead { get; private set; }

        private void Awake()
        {
            m_deathAnimator = GetComponent<Animator>();
        }

        private void OnEnable()
        {
            GetComponent<IHealth>().HealthChange += HealthChange;
        }

        private void OnDisable()
        {
            GetComponent<IHealth>().HealthChange -= HealthChange;
        }

        private void HealthChange(float currentHealth)
        {
            if (currentHealth <= 0f)
            {
                StopCoroutine(PlayDeath());
                StartCoroutine(PlayDeath());
                LoseLife(1);
            }
        }

        private void LoseLife(int numberOfLivesLost)
        {
            if (m_numberOfLives <= 0)
                return;

            m_numberOfLives -= numberOfLivesLost;

            Broadcast.Send<IBroadcast>(gameObject, (x, y) => x.Inform(Broadcasts.BroadcastMessage.Dead));

            gameObject.layer = (int)Layer.Dead;

            DeathEvent?.Invoke(m_numberOfLives);
        }

        private IEnumerator PlayDeath()
        {
            m_deathAnimator.SetBool("Dead", true);

            yield return new WaitForEndOfFrame();
            yield return new WaitForEndOfFrame();

            m_deathAnimator.SetBool("Dead", false);
        }

        public void Inform(BroadcastMessage message) { }
    }
}
