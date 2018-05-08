using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cameras
{
    [RequireComponent(typeof(Camera))]
    public class GameCamera : MonoBehaviour
    {
        [SerializeField] private Transform[] targets;

        [SerializeField] private Vector3 offset;

        [SerializeField] private float smoothTime = 0.5f;

        [SerializeField] private float minZoom = 5f;
        [SerializeField] private float maxZoom = 5f;
        [SerializeField] private float zoomLimit = 50f;

        [SerializeField] private float fovModifier = 1f;

        private Camera camera = null;
        private Vector3 velocity;

        private void Awake()
        {
            camera = GetComponent<Camera>();
        }

        private void LateUpdate()
        {
            if (targets.Length == 0f)
                return;

            Move();
            Zoom();
        }

        private void Move()
        {
            Vector3 centerPoint = GetCenterPoint();

            Vector3 targetPosition = centerPoint + offset;

            transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);
        }

        private void Zoom()
        {
            float targetZoom = Mathf.Lerp(minZoom, maxZoom, GetGreatestDistance() / zoomLimit);
            camera.orthographicSize = Mathf.Lerp(camera.orthographicSize, targetZoom, Time.deltaTime * fovModifier);
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