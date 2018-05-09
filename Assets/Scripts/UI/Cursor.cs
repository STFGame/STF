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
        public bool Active { get; private set; }

        // Use this for initialization
        private void Awake()
        {
            layerMask = (1 << (int)Layer.GUI3D) | (1 << (int)Layer.UI);

            meshRender = GetComponentInChildren<MeshRenderer>();
        }

        public void InitiateDevice(PlayerNumber playerNumber)
        {
            this.playerNumber = playerNumber;

            device = new Device(Input.GetJoystickNames()[(int)playerNumber - 1], (int)playerNumber);

            if (playerNumber != PlayerNumber.PlayerOne)
                meshRender.enabled = false;

            Active = meshRender.enabled;
        }

        // Update is called once per frame
        private void Update()
        {
            bool inRange = (((int)playerNumber - 1) - Input.GetJoystickNames().Length <= 0);

            if (inRange && playerNumber != PlayerNumber.None && device == null)
                InitiateDevice(playerNumber);

            if (device == null)
                return;

            device.UpdateDevice();

            if (!CursorEnabled())
                return;

            Move();

            Action();
        }

        private bool CursorEnabled()
        {
            if (!meshRender.enabled)
            {
                for (int i = 0; i < device.maxButtons; i++)
                {
                    if (device.GetButton(i).Press)
                    {
                        meshRender.enabled = true;
                        break;
                    }
                }
            }
            return Active = meshRender.enabled;
        }

        private void Move()
        {
            Vector3 cursorMovement = new Vector3(device.LeftStick.Horizontal, device.LeftStick.Vertical, 0f) * sensitivity;

            Vector3 currentPosition = transform.position;
            Vector3 targetPosition = new Vector3(transform.position.x + cursorMovement.x, transform.position.y + cursorMovement.y, transform.position.z);

            transform.position = Vector3.SmoothDamp(currentPosition, targetPosition, ref cursorVelocity, smoothTime);

            transform.position = Camera.main.ViewportToWorldPoint(CursorBounds());
        }

        private Vector3 CursorBounds()
        {
            Vector3 cameraView = Camera.main.WorldToViewportPoint(transform.position);

            cameraView.x = Mathf.Clamp01(cameraView.x);
            cameraView.y = Mathf.Clamp01(cameraView.y);

            return cameraView;
        }

        private void Action()
        {
            Debug.DrawRay(transform.position, Vector3.forward);

            RaycastHit hit;
            if (Physics.Raycast(transform.position, Vector3.forward, out hit, layerMask))
            {
                if (lastObject == null)
                    lastObject = hit.transform.GetComponent<IUIComponent>();

                if (lastObject != null)
                {
                    lastObject.Hover(true);

                    lastObject.Press(device.Action1.Press);
                }
                return;
            }

            if (lastObject != null)
            {
                lastObject.Hover(false);
                lastObject = null;
            }
        }
    }
}