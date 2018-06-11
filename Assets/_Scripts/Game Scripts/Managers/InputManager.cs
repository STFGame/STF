using Controls;
using UnityEngine;

namespace Managers
{
    public class InputManager : MonoBehaviour
    {
        private static Device[] m_devices = null;

        public static int Length { get { return m_devices.Length; } }

        // Use this for initialization
        private void Awake()
        {
            string[] controllerNames = Input.GetJoystickNames();
            m_devices = new Device[controllerNames.Length];
            for (int i = 0; i < controllerNames.Length; i++)
                m_devices[i] = new Device(controllerNames[i], i + 1);
        }

        // Update is called once per frame
        private void Update()
        {
            for (int i = 0; i < m_devices.Length; i++)
                m_devices[i].Execute();
        }

        public static Device GetDevice(int index)
        {
            return m_devices[index];
        }

    }
}