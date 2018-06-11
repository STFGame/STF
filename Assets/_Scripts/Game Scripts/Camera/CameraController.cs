using Managers;
using Player.Management;
using System.Collections.Generic;
using UnityEngine;

namespace Cameras
{
    public class CameraController : MonoBehaviour
    {
        [SerializeField] private FocusLevel m_focusLevel = null;
        [SerializeField] private Vector3 m_offset = Vector3.zero;

        [SerializeField] private float m_depthExecuteSpeed = 5f;
        [SerializeField] private float m_angleExecuteSpeed = 7f;
        [SerializeField] private float m_positionExecuteSpeed = 5f;

        [SerializeField] private float m_depthMax = -10f;
        [SerializeField] private float m_depthMin = -22f;

        [SerializeField] private float m_angleMax = 11f;
        [SerializeField] private float m_angleMin = 3f;

        private List<Transform> m_players = new List<Transform>();

        private Vector3 m_cameraPosition;
        private float m_cameraEulerX;

        private void Awake()
        {
            m_players.Add(m_focusLevel.transform);
        }

        public void Initialise(PlayerManager playerManager)
        {
            for (int i = 0; i < playerManager.Length; i++)
                m_players.Add(playerManager.GetPlayer(i).transform);
        }

        private void LateUpdate()
        {
            if (Pause.IsPaused)
                return;

            CalculateCameraLocations();

            Move();
        }

        private void Move()
        {
            Vector3 startPosition = transform.position;

            if(transform.position != m_cameraPosition)
            {
                Vector3 targetPosition = Vector3.zero;
                float velocity = 0f;

                m_cameraPosition += m_offset;

                targetPosition.x = Mathf.SmoothDamp(startPosition.x, m_cameraPosition.x, ref velocity, m_positionExecuteSpeed * Time.deltaTime);
                targetPosition.y = Mathf.SmoothDamp(startPosition.y, m_cameraPosition.y, ref velocity, m_positionExecuteSpeed * Time.deltaTime);
                targetPosition.z = Mathf.SmoothDamp(startPosition.z, m_cameraPosition.z, ref velocity, m_depthExecuteSpeed * Time.deltaTime);

                transform.position = targetPosition;
            }

            Vector3 localEulerAngles = transform.localEulerAngles;
            if(localEulerAngles.x != m_cameraEulerX)
            {
                Vector3 targetEulerAngles = new Vector3(m_cameraEulerX, localEulerAngles.y, localEulerAngles.z);
                transform.localEulerAngles = Vector3.MoveTowards(localEulerAngles, targetEulerAngles, m_angleExecuteSpeed * Time.deltaTime);
            }
        }

        private void MoveCamera()
        {
            Vector3 position = transform.position;
            if (position != m_cameraPosition)
            {
                Vector3 targetPosition = Vector3.zero;
                float cameraVelocity = 0f;

                targetPosition.x = Mathf.SmoothDamp(position.x, m_cameraPosition.x, ref cameraVelocity, m_positionExecuteSpeed * Time.deltaTime);
                targetPosition.y = Mathf.SmoothDamp(position.y, m_cameraPosition.y, ref cameraVelocity, m_positionExecuteSpeed * Time.deltaTime);
                targetPosition.z = Mathf.SmoothDamp(position.z, m_cameraPosition.z, ref cameraVelocity, m_depthExecuteSpeed * Time.deltaTime);

                transform.position = targetPosition;
            }

            Vector3 localEulerAngles = transform.localEulerAngles;
            if (localEulerAngles.x != m_cameraEulerX)
            {
                Vector3 targetEulerAngles = new Vector3(m_cameraEulerX, localEulerAngles.y, localEulerAngles.z);
                transform.localEulerAngles = Vector3.MoveTowards(localEulerAngles, targetEulerAngles, m_angleExecuteSpeed * Time.deltaTime);
            }
        }

        private void CalculateCameraLocations()
        {
            Vector3 averageCenter = Vector3.zero;
            Vector3 totalPositions = Vector3.zero;
            Bounds playerBounds = new Bounds();

            for (int i = 0; i < m_players.Count; i++)
            {
                Vector3 playerPosition = m_players[i].position;

                if (!m_focusLevel.FocusBounds.Contains(playerPosition))
                {
                    float playerX = Mathf.Clamp(playerPosition.x, m_focusLevel.FocusBounds.min.x, m_focusLevel.FocusBounds.max.x);
                    float playerY = Mathf.Clamp(playerPosition.y, m_focusLevel.FocusBounds.min.y, m_focusLevel.FocusBounds.max.y);
                    float playerZ = Mathf.Clamp(playerPosition.z, m_focusLevel.FocusBounds.min.z, m_focusLevel.FocusBounds.max.z);

                    playerPosition = new Vector3(playerX, playerY, playerZ);
                }

                totalPositions += playerPosition;
                playerBounds.Encapsulate(playerPosition);
            }

            averageCenter = (totalPositions / m_players.Count);

            float extents = (playerBounds.extents.x + playerBounds.extents.y);
            float lerpPercent = Mathf.InverseLerp(0f, (m_focusLevel.HalfBoundsX + m_focusLevel.HalfBoundsY) / 2f, extents);

            float depth = Mathf.Lerp(m_depthMax, m_depthMin, lerpPercent);
            float angle = Mathf.Lerp(m_angleMax, m_angleMin, lerpPercent);

            m_cameraEulerX = angle;
            m_cameraPosition = new Vector3(averageCenter.x, averageCenter.y, depth);
        }
    }
}
