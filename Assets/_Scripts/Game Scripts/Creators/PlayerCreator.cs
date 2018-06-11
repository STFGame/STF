using Broadcasts;
using Characters;
using Life;
using System.Collections;
using UnityEngine;

namespace Spawners
{
    public class PlayerCreator : MonoBehaviour, IBroadcast
    {
        [SerializeField] private Transform m_respawnPoint = null;
        [SerializeField] private Transform m_respawnPlatform = null;
        [SerializeField] private bool m_loadInWorldSpace = false;

        [SerializeField] private Vector3 m_platformTargetLocation = Vector3.zero;
        [SerializeField] private float m_respawnTime = 0f;

        private Vector3 m_platformStartLocation = Vector3.zero;

        private Animator m_respawnAnimator = null;
        private IHealth m_health;

        private Rigidbody m_rigidbody = null;
        public GameObject Player { get; private set; }

        private void Awake()
        {
            m_platformStartLocation = m_respawnPlatform.position;
        }

        public void Initialise(GameObject player)
        {
            Player = Instantiate(player, transform, m_loadInWorldSpace) as GameObject;
            m_rigidbody = Player.GetComponent<Rigidbody>();

            m_health = Player.GetComponent<IHealth>();

            Player.GetComponent<Death>().DeathEvent += PlayerDead;
            m_respawnAnimator = Player.GetComponent<Animator>();
        }

        private void PlayerDead(int numberOfLivesLeft)
        {
            if (numberOfLivesLeft <= 0)
                return;

            StopCoroutine(RespawnRoutine());
            StartCoroutine(RespawnRoutine());
        }

        private IEnumerator RespawnRoutine()
        {
            yield return new WaitForSeconds(m_respawnTime);
            Player.transform.position = m_respawnPoint.position;
            m_health.RestoreHealth(m_health.MaxHealth);
            m_respawnAnimator.SetBool("Respawn", true);

            yield return new WaitForSeconds(2f);

            m_rigidbody.useGravity = true;
            m_respawnPlatform.position = m_platformTargetLocation;

            yield return new WaitForSeconds(2f);

            m_rigidbody.useGravity = false;
            m_respawnPlatform.position = m_platformStartLocation;

            Broadcast.Send<IBroadcast>(Player, (x, y) => x.Inform(Broadcasts.BroadcastMessage.None));
            Player.layer = (int)Layer.PlayerStatic;
            m_respawnAnimator.SetBool("Respawn", false);
        }

        public void Inform(BroadcastMessage message) { }
    }
}
