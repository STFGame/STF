using UnityEngine;

namespace Managers
{
    /// <summary>
    /// Class responsible for spawning objects
    /// </summary>
    public class SpawnManager : MonoBehaviour
    {
        #region SpawnManager Variables
        [Header("Spawn Data")]
        //The starting spawn point of the object (usually character)
        public Transform[] spawnPoints = null;

        //The respawn point of the object (usually character)
        public Transform[] respawnPoints = null;

        //The respawn time of the character if he/she dies
        [SerializeField] private float respawnTime = 5f;

        [Header("Movement Info")]
        //The position that the respawner should move to
        [SerializeField] private Vector3 targetPosition;
        
        //The smooth time that it takes for it to move to the target position
        [SerializeField] private float smoothTime = 0.2f;

        //Makes the platform move to target position if enabled
        //and move to start position if not enabled. Used for debug purposes
        [SerializeField] private bool manualEnable = false;

        //Stores the start position of the respawner so that it can return to that point
        private Vector3 startPosition;

        //The velocity required for smooth damping
        private Vector3 velocity = Vector3.zero;

        //Cache of the this object's transform
        private new Transform transform = null;

        #endregion

        #region Load
        private void Awake()
        {
            transform = GetComponent<Transform>();

            startPosition = transform.position;
            targetPosition = startPosition - targetPosition;
        }

        #endregion

        #region Updates
        private void Update()
        {
            MovePlatform(manualEnable);
        }

        public void MovePlatform(bool respawn)
        {
            if (respawn)
            {
                if (Mathf.Approximately(transform.position.magnitude, targetPosition.magnitude))
                    return;

                Vector3 move = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);

                transform.position = move;
            }
            else
            {
                if (Mathf.Approximately(transform.position.magnitude, startPosition.magnitude))
                    return;

                Vector3 move = Vector3.SmoothDamp(transform.position, startPosition, ref velocity, smoothTime);

                transform.position = move;
            }
        }
        #endregion
    }
}
