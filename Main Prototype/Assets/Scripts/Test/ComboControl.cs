using Controls;
using System.Collections;
using System.Collections.Generic;
using Tests;
using UnityEngine;

public class ComboControl : MonoBehaviour
{
    [SerializeField] private Combo combo = new Combo();

    private ControlInput controlInput = new ControlInput();

    private Device device;

    // Use this for initialization
    void Awake()
    {
        device = new Device(Input.GetJoystickNames()[0], 1);
    }

    // Update is called once per frame
    void Update()
    {
        device.UpdateDevice();

        controlInput.UpdateInput(device);
    }
}
