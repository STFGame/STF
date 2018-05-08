using Controls;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UI
{
    public class Cursor : MonoBehaviour
    {
        [SerializeField] private PlayerNumber playerNumber = PlayerNumber.None;
        [SerializeField] [Range(0f, 1f)] private float smoothTime = 0.5f;
        [SerializeField] private float sensitivity = 5f;
        private Vector3 cursorVelocity;

        private Device device;
        private LayerMask layerMask;
        private MeshRenderer meshRender;

        private IUIComponent lastObject = null;

        public PlayerNumber PlayerNumber { get { return playerNumber; } }

        // Use this for initialization
        private void Awake()
        {
            device = new Device(Input.GetJoystickNames()[(int)playerNumber - 1], (int)playerNumber);
            layerMask = (1 << (int)Layer.GUI3D) | (1 << (int)Layer.UI);

            meshRender = GetComponentInChildren<MeshRenderer>();
            meshRender.enabled = false;
        }

        // Update is called once per frame
        private void Update()
        {
            if (device == null)
                return;

            device.UpdateDevice();

            if (!meshRender.enabled)
            {
                meshRender.enabled = device.Action1.Press;
                return;
            }

            Move();

            Action();

            SetBounds();
        }

        private void Move()
        {
            Vector3 cursorMovement = new Vector3(device.LeftStick.Horizontal, device.LeftStick.Vertical, 0f) * sensitivity;

            Vector3 currentPosition = transform.position;
            Vector3 targetPosition = new Vector3(transform.position.x + cursorMovement.x, transform.position.y + cursorMovement.y, transform.position.z);

            transform.position = Vector3.SmoothDamp(currentPosition, targetPosition, ref cursorVelocity, smoothTime);

            Vector3 cameraView = Camera.main.WorldToViewportPoint(transform.position);

            cameraView.x = Mathf.Clamp01(cameraView.x);
            cameraView.y = Mathf.Clamp01(cameraView.y);

            transform.position = Camera.main.ViewportToWorldPoint(cameraView);
        }

        private void Action()
        {
            RaycastHit hit;

            Debug.DrawRay(transform.position, Vector3.forward);
            if (Physics.Raycast(transform.position, Vector3.forward, out hit, layerMask))
            {
                lastObject = hit.transform.GetComponentInParent<IUIComponent>();

                if (lastObject != null)
                {
                    lastObject.Hover(true);

                    lastObject.Action(device.Action1.Press);
                }
                return;
            }

            if (lastObject != null)
            {
                lastObject.Hover(false);
                lastObject = null;
            }
        }

        private void SetBounds()
        {
            Vector2 screenSize = new Vector2(Screen.width, Screen.height);
            Bounds bounds = new Bounds(Vector3.zero, screenSize);
            bounds.Encapsulate(transform.position);
        }
    }
}