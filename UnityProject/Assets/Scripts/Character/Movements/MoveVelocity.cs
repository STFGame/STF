﻿using Actions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Movements
{
    public enum MoveMode { None, Crouch, Sneak, Walk, Run, Dash }

    [Serializable]
    public class MoveVelocity
    {
        [SerializeField] private string m_Name = "Walk";
        [SerializeField] private MoveMode m_MoveMode = MoveMode.None;
        [SerializeField] private float m_Speed = 10f;
        [SerializeField] private float m_RotationSpeed = 10f;
        [SerializeField] private float m_MaxAcceleration = 10f;
        [SerializeField] private float m_Deceleration = 10f;
        [SerializeField] private int m_MovePriority = 0;

        [SerializeField] private ActionRange m_Activation = null;

        private bool m_Decelerating = false;
        private float m_Rotation = 0f;
        private Vector2 m_PreviousDirection;

        public float Speed { get { return m_Speed; } }
        public float RotationSpeed { get { return m_RotationSpeed; } }
        public float Deceleration { get { return m_Deceleration; } }
        public int MovePriority { get { return m_MovePriority; } }

        public MoveMode MoveMode { get { return m_MoveMode; } }

        public MoveVelocity() { }
        public MoveVelocity(float speed, float rotationSpeed, float acceleration, float deceleration)
        {
            m_Speed = speed;
            m_RotationSpeed = rotationSpeed;
            m_Deceleration = deceleration;
        }

        public bool Active(Vector2 direction)
        {
            if (direction.x == 0f)
            {
                m_Activation.Reset();
                return false;
            }

            if (m_PreviousDirection.x * direction.x < 0f)
                m_Activation.Reset();

            m_PreviousDirection = direction;

            return m_Activation.ActionSuccess(direction);
        }

        public Vector2 GetVelocity(Vector2 direction, Vector2 currentVelocity, ref float acceleration)
        {
            if (direction.x == 0f && !m_Decelerating)
            {
                acceleration = 0f;
                m_Decelerating = true;
            }
            else if (direction.x != 0f)
            {
                m_Decelerating = false;
                acceleration = m_MaxAcceleration;
            }

            if (m_Decelerating)
                Decelerate(ref acceleration);

            Vector2 targetVelocity = new Vector2(direction.x, 0f) * m_Speed;

            Vector2 velocityChange = (targetVelocity - currentVelocity) * (Time.deltaTime * acceleration);

            velocityChange.x = Mathf.Clamp(velocityChange.x, -m_Speed, m_Speed);
            velocityChange.y = 0f;

            return velocityChange;
        }

        private void Decelerate(ref float acceleration)
        {
            if (acceleration < m_MaxAcceleration)
                acceleration += (Time.deltaTime * m_Deceleration);
            else
                acceleration = m_MaxAcceleration;
        }

        public Quaternion GetRotation(Vector3 forward, float direction, out float rotationSpeed)
        {
            Quaternion endRotation = Quaternion.Euler(0f, m_Rotation, 0f);

            if (direction != 0f)
            {
                Vector3 angle = Vector3.right * direction;

                m_Rotation = Vector3.Angle(forward, angle) * -forward.x;
            }

            rotationSpeed = m_RotationSpeed;

            return endRotation;
        }
    }
}