using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

[Serializable]
public class MoveSpeed
{
    [SerializeField] private float speed;
    [SerializeField] private float rotationSpeed;
    [SerializeField] private float acceleration;
    [SerializeField] private float deceleration;

    public float Speed { get { return speed; } }
    public float RotationSpeed { get { return rotationSpeed; } }
    public float Acceleration { get { return acceleration; } }
    public float Deceleration { get { return deceleration; } }

    public MoveSpeed() { }
    public MoveSpeed(float speed, float rotationSpeed, float acceleration, float deceleration)
    {
        this.speed = speed;
        this.rotationSpeed = rotationSpeed;
        this.acceleration = acceleration;
        this.deceleration = deceleration;
    }
}
