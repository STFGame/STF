using InControl;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDevice
{
    string Name { get; set; }
    bool Handle(string val);
}

public class PlayStationDevice : IDevice
{
    public string Name { get; set; }
    string[] joystickNames = { "Wireless Control" };

    public PlayStationDevice(string name)
    {
        Name = name;
    }

    public bool Handle(string val)
    {
        for (int i = 0; i < joystickNames.Length; i++)
        {
            if (val == joystickNames[i])
                return true;
        }
        return false;
    }
}

public interface IUpdate
{
    void OnUpdate();
    void OnFixedUpdate(InputDevice device);
}

public class DeviceType
{
    public string Name;
    string[] JoystickNames;

    float xAxis = Input.GetAxis("Horizontal");
    float yAxis = Input.GetAxis("Vertical");

    bool Jump = Input.GetKeyDown("space");

    bool Action1 = Input.GetKeyDown("z");
    bool Action2 = Input.GetKeyDown("x");
    bool Action3 = Input.GetKeyDown("c");
    bool Action4 = Input.GetKeyDown("v");
}

public class XboxDevice : DeviceType
{
    public readonly string Name;
    string[] JoystickNames = { "Xbox 360 Controller" };

    public XboxDevice(string val)
    {
        Name = val;
    }
}

public class Device
{
    string[] playStation = { "Wireless Controller" };
    string[] xbox = { "Xbox Gamepad" };
    IDevice device;
    DeviceType d;
    public Device(string joystickName)
    {
        for (int i = 0; i < playStation.Length; i++)
        {
            if (joystickName == playStation[i])
                device = new PlayStationDevice(joystickName);
            else if (joystickName == xbox[i])
                d = new XboxDevice(joystickName);
        }
    }

    public IDevice GetDevice { get { return device; } }
    public DeviceType GetD { get { return d; } }
}