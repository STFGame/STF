using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

[Serializable]
public struct Speed
{
    public float speed;
    public float rotationSpeed;
    public float acceleration;
    public float deceleration;

    public Speed(float speed, float rotationSpeed, float acceleration, float deceleration)
    {
        this.speed = speed;
        this.rotationSpeed = rotationSpeed;
        this.acceleration = acceleration;
        this.deceleration = deceleration;
    }
}
