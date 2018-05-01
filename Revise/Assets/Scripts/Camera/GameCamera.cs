using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cameras
{
    [RequireComponent(typeof(Camera))]
    public class GameCamera : MonoBehaviour
    {
        [SerializeField] private List<Transform> actors;

        [SerializeField] private CameraFocus cameraFocus = new CameraFocus();

        [SerializeField] private float depthUpdateSpeed = 5f;
        [SerializeField] private float angleUpdateSpeed = 7f;
        [SerializeField] private float positionUpdateSpeed = 5f;

        [SerializeField] private float depthMax = -10f;
        [SerializeField] private float depthMin = -22f;

        [SerializeField] private float angleMax = 11f;
        [SerializeField] private float angleMin = 3f;

        private float cameraEulerX;
        private Vector3 cameraPosition;

        private void Awake()
        {
            //actors.Add(cameraFocus.focus);
        }

        private void Update()
        {
            cameraFocus.Update();
        }

        private void LateUpdate()
        {
            CalculateCameraLocations();
            MoveCamera();
        }

        private void MoveCamera()
        {
            Vector3 position = transform.position;
            if (position != cameraPosition)
            {
                Vector3 targetPosition = Vector3.zero;
                targetPosition.x = Mathf.MoveTowards(position.x, cameraPosition.x, positionUpdateSpeed * Time.deltaTime);
                targetPosition.y = Mathf.MoveTowards(position.y, cameraPosition.y, positionUpdateSpeed * Time.deltaTime);
                targetPosition.z = Mathf.MoveTowards(position.z, cameraPosition.z, depthUpdateSpeed * Time.deltaTime);

                cameraPosition = targetPosition;
            }

            Vector3 localEulerAngles = transform.localEulerAngles;
            if (localEulerAngles.x != cameraEulerX)
            {
                Vector3 targetEulerAngles = new Vector3(cameraEulerX, localEulerAngles.y, localEulerAngles.z);
                transform.localEulerAngles = Vector3.MoveTowards(localEulerAngles, targetEulerAngles, angleUpdateSpeed * Time.deltaTime);
            }
        }

        private void CalculateCameraLocations()
        {
            Vector3 averageCenter = Vector3.zero;
            Vector3 totalPosition = Vector3.zero;

            Bounds actorBounds = new Bounds();
            for (int i = 0; i < actors.Count; i++)
            {
                Vector3 actorPosition = actors[i].position;

                if (!cameraFocus.focusBounds.Contains(actorPosition))
                {
                    float actorX = Mathf.Clamp(actorPosition.x, cameraFocus.focusBounds.min.x, cameraFocus.focusBounds.max.x);
                    float actorY = Mathf.Clamp(actorPosition.y, cameraFocus.focusBounds.min.y, cameraFocus.focusBounds.max.y);
                    float actorZ = Mathf.Clamp(actorPosition.z, cameraFocus.focusBounds.min.z, cameraFocus.focusBounds.max.z);

                    actorPosition = new Vector3(actorX, actorY, actorZ);
                }

                totalPosition += actorPosition;

                actorBounds.Encapsulate(actorPosition);
            }
            averageCenter = (totalPosition / actors.Count);

            float extents = (actorBounds.extents.x + actorBounds.extents.y);
            float lerpPercent = Mathf.InverseLerp(0f, (cameraFocus.halfXBounds + cameraFocus.halfYBounds) / 2f, extents);

            float depth = Mathf.Lerp(depthMax, depthMin, lerpPercent);
            float angle = Mathf.Lerp(angleMax, angleMin, lerpPercent);

            cameraEulerX = angle;
            cameraPosition = new Vector3(averageCenter.x, averageCenter.y, depth);
        }
    }
}