using Characters;
using Life;
using System.Collections;
using UnityEngine;

namespace Effects
{
    /// <summary>
    /// Class that handles the "freeze" effects when a character is hit or hits another character.
    /// </summary>
    public class Freeze : MonoBehaviour
    {
        [SerializeField] private float m_freezeLength = 0.15f;
        [SerializeField] private float m_freezeDelay = 0.25f;

        private Animator m_freezeAnimator;
        private Rigidbody m_rigidbody;
        private float m_previousAnimationSpeed;

        private void Awake()
        {
            m_freezeAnimator = GetComponent<Animator>();
            m_rigidbody = GetComponent<Rigidbody>();
        }

        private void OnEnable()
        {
            GetComponent<IHealth>().HealthChange += HurtFreeze;
            GetComponent<Attack>().HitEvent += HitFreeze;
        }

        private void OnDisable()
        {
            GetComponent<IHealth>().HealthChange -= HurtFreeze;
            GetComponent<Attack>().HitEvent -= HitFreeze;
        }

        private void HurtFreeze(float damageTaken)
        {
            StopCoroutine(FreezeRoutine(0f, 0f));
            StartCoroutine(FreezeRoutine(m_freezeDelay, m_freezeDelay));
        }

        private void HitFreeze(bool isHit)
        {
            if(isHit)
            {
                StopCoroutine(FreezeRoutine(0f, 0f));
                StartCoroutine(FreezeRoutine(0f, m_freezeLength));
            }
        }

        private IEnumerator FreezeRoutine(float initialDelay, float freezeLength)
        {
            m_rigidbody.isKinematic = true;

            yield return new WaitForSeconds(initialDelay);

            if (m_freezeAnimator.speed != 0f)
                m_previousAnimationSpeed = m_freezeAnimator.speed;

            m_freezeAnimator.speed = 0f;

            yield return new WaitForSeconds(freezeLength);

            m_freezeAnimator.speed = m_previousAnimationSpeed;

            m_rigidbody.isKinematic = false;
        }
    }
}
