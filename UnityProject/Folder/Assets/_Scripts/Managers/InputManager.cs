using Controls;
using UnityEngine;

namespace Managers
{
    public class InputManager : MonoBehaviour
    {
        private static Device[] m_Devices = null;

        // Use this for initialization
        private void Awake()
        {
            string[] controllerNames = Input.GetJoystickNames();
            m_Devices = new Device[controllerNames.Length];
            for (int i = 0; i < controllerNames.Length; i++)
                m_Devices[i] = new Device(controllerNames[i], i + 1);
        }

        // Update is called once per frame
        private void Update()
        {
            for (int i = 0; i < m_Devices.Length; i++)
                m_Devices[i].Execute();
        }

        public static Device GetDevice(int index)
        {
            return m_Devices[index];
        }
    }
}