using Spawners;
using UnityEngine;

namespace Managers
{
    public class CameraManager : MonoBehaviour
    {
        [SerializeField] private Spawner m_PlayerSpawner = null;
        [SerializeField] private Vector3 m_Offset = Vector3.zero;
        [SerializeField] private float smoothTime = 0.5f;

        [SerializeField] private float m_MinZoom = 5f;
        [SerializeField] private float m_MaxZoom = 5f;
        [SerializeField] private float m_ZoomLimit = 50f;

        [SerializeField] private float m_FieldOfView = 1f;

        private Transform[] m_Targets;

        private Camera m_Camera = null;
        private Vector3 m_CameraVelocity;

        private bool m_IsOrthographic = true;

        // Use this for initialization
        private void Awake()
        {
            m_Camera = GetComponent<Camera>();

            m_IsOrthographic = m_Camera.orthographic;
        }

        public void Start()
        {
            m_Targets = new Transform[m_PlayerSpawner.Length];
            for (int i = 0; i < m_Targets.Length; i++)
                m_Targets[i] = m_PlayerSpawner.GetObject(i).transform;
        }

        // Update is called once per frame
        private void LateUpdate()
        {
            if (m_Targets.Length <= -1)
                return;

            Move();

            Zoom();
        }

        private void Move()
        {
            Vector3 centerPoint = GetCenterPoint();

            Vector3 startPosition = transform.position;
            Vector3 targetPosition = centerPoint + m_Offset;

            transform.position = Vector3.SmoothDamp(startPosition, targetPosition, ref m_CameraVelocity, smoothTime);
        }

        private void Zoom()
        {
            float targetZoom = Mathf.Lerp(m_MinZoom, m_MaxZoom, GetGreatestDistance() / m_ZoomLimit);

            if (m_IsOrthographic)
                m_Camera.orthographicSize = Mathf.Lerp(m_Camera.orthographicSize, targetZoom, Time.deltaTime * m_FieldOfView);
            else
                m_Camera.fieldOfView = Mathf.Lerp(m_Camera.fieldOfView, targetZoom, Time.deltaTime * m_FieldOfView);
        }

        private float GetGreatestDistance()
        {
            if (m_Targets.Length <= 0)
                return 0f;

            Bounds bounds = new Bounds(m_Targets[0].position, Vector3.zero);
            for (int i = 0; i < m_Targets.Length; i++)
                bounds.Encapsulate(m_Targets[i].position);

            return bounds.size.x;
        }

        private Vector3 GetCenterPoint()
        {
            if (m_Targets.Length <= 0)
                return Vector3.zero;

            if (m_Targets.Length == 1)
                return m_Targets[0].position;

            Bounds bounds = new Bounds(m_Targets[0].position, Vector3.zero);
            for (int i = 0; i < m_Targets.Length; i++)
                bounds.Encapsulate(m_Targets[i].position);

            return bounds.center;
        }
    }
}