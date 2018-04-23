using ComboSystem;
using Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Actor
{
    public class ActorCombo : MonoBehaviour
    {
        [SerializeField] private Combo[] combos;

        private Device device;

        private void Awake()
        {
            device = new Device(Input.GetJoystickNames()[0], 1);

            for (int i = 0; i < combos.Length; i++)
                combos[i].Init(device);
        }

        private void Update()
        {
            device.UpdateDevice();

            for (int i = 0; i < combos.Length; i++)
                if (combos[i].CheckCombo())
                    print(combos[i].comboName);
        }
    }
}
