using Broadcasts;
using Life;
using System.Collections;
using UnityEngine;

namespace Effectors
{
    /// <summary>
    /// Component that handles stuns and freezes
    /// </summary>
    public class Stun : MonoBehaviour, IBroadcast
    {
        [SerializeField] private float m_stunLength = 0.3f;

        private Animator m_stunAnimator;

        public bool IsStunned { get; private set; }

        private void Awake()
        {
            m_stunAnimator = GetComponent<Animator>();
        }

        private void OnEnable()
        {
            GetComponent<Shield>().HealthChange += ShieldStun;
        }

        private void OnDisable()
        {
            GetComponent<Shield>().HealthChange -= ShieldStun;
        }

        private void ShieldStun(float currentShield)
        {
            if (currentShield <= 0f)
            {
                StopCoroutine(StunRoutine(m_stunLength));
                StartCoroutine(StunRoutine(m_stunLength));
            }
        }

        public void StartStun(float stunLength)
        {
            StopCoroutine(StunRoutine(stunLength));
            StartCoroutine(StunRoutine(stunLength));
        }

        private IEnumerator StunRoutine(float stunLength)
        {
            //Maybe add a message alert here to indicate that character is stunned
            IsStunned = true;
            Broadcast.Send<IBroadcast>(gameObject, (x, y) => x.Inform(Broadcasts.BroadcastMessage.Stunned));
            yield return new WaitForSeconds(stunLength);
            Broadcast.Send<IBroadcast>(gameObject, (x, y) => x.Inform(Broadcasts.BroadcastMessage.None));
            IsStunned = false;
            //Play another message to indicate that the character is not longer stunned
        }

        public void Inform(BroadcastMessage message) { }
    }
}
