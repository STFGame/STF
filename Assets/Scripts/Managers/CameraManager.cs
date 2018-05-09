using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Managers
{
    public class CameraManager : MonoBehaviour
    {
        [SerializeField] private Transform[] targets = null;
        [SerializeField] private Vector3 offset = Vector3.zero;
        [SerializeField] private float smoothTime = 0.5f;

        [SerializeField] private float minZoom = 5f;
        [SerializeField] private float maxZoom = 5f;
        [SerializeField] private float zoomLimit = 50f;

        [SerializeField] private float fieldOfView = 1f;

        private new Camera camera = null;
        private Vector3 cameraVelocity;

        private bool isOrthographic = true;

        // Use this for initialization
        private void Awake()
        {
            camera = GetComponent<Camera>();

            isOrthographic = camera.orthographic;
        }

        // Update is called once per frame
        private void LateUpdate()
        {
            Move();

            Zoom();
        }

        private void Move()
        {
            Vector3 centerPoint = GetCenterPoint();

            Vector3 startPosition = transform.position;
            Vector3 targetPosition = centerPoint + offset;

            transform.position = Vector3.SmoothDamp(startPosition, targetPosition, ref cameraVelocity, smoothTime);
        }

        private void Zoom()
        {
            float targetZoom = Mathf.Lerp(minZoom, maxZoom, GetGreatestDistance() / zoomLimit);

            if (isOrthographic)
                camera.orthographicSize = Mathf.Lerp(camera.orthographicSize, targetZoom, Time.deltaTime * fieldOfView);
            else
                camera.fieldOfView = Mathf.Lerp(camera.fieldOfView, targetZoom, Time.deltaTime * fieldOfView);
        }

        private float GetGreatestDistance()
        {
            Bounds bounds = new Bounds(targets[0].position, Vector3.zero);
            for (int i = 0; i < targets.Length; i++)
                bounds.Encapsulate(targets[i].position);

            return bounds.size.x;
        }

        private Vector3 GetCenterPoint()
        {
            if (targets.Length == 1)
                return targets[0].position;

            Bounds bounds = new Bounds(targets[0].position, Vector3.zero);
            for (int i = 0; i < targets.Length; i++)
                bounds.Encapsulate(targets[i].position);

            return bounds.center;
        }
    }
}