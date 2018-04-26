using Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace UI
{
    public class Cursor : MonoBehaviour
    {
        private Device device;

        private RaycastHit hit;

        private void Start()
        {
            gameObject.transform.position = Vector3.zero;

            device = new Device(Input.GetJoystickNames()[0], 1);
        }

        private void Update()
        {
            device.UpdateDevice();

            Move();

            Click();
        }

        private void Move()
        {
            Vector2 cursorMovement = new Vector2(device.LeftStick.Horizontal, device.LeftStick.Vertical);

            transform.position = new Vector3(transform.position.x + cursorMovement.x, transform.position.y + cursorMovement.y);
        }

        private void Click()
        {
            if (device.Action1.Press)
            {
                Debug.DrawRay(transform.position, Vector3.forward, Color.black);
                if (Physics.Raycast(transform.position, Vector3.forward, out hit, 20f))
                    if (hit.transform.gameObject.CompareTag("Button"))
                        hit.transform.gameObject.GetComponent<Button>().LoadScene();
            }
        }
    }
}
