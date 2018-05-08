using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Actor.Combat
{
    [Serializable]
    public struct AttackTravel
    {
        [SerializeField] [Range(-1f, 1f)] private float directionX;
        [SerializeField] [Range(-1f, 1f)] private float directionY;
        [SerializeField] [Range(0f, 20f)] private float speed;

        public Vector3 AttackVelocity { get; private set; }

        public AttackTravel(float directionX, float directionY, float speed)
        {
            this.directionX = directionX;
            this.directionY = directionY;
            this.speed = speed;

            this.directionX = Mathf.Clamp(this.directionX, -1f, 1f);
            this.directionY = Mathf.Clamp(this.directionY, -1f, 1f);
            this.speed = Mathf.Clamp(this.speed, 0f, 20f);

            AttackVelocity = new Vector3(directionX, directionY, speed) * speed;
        }

        public Vector3 GetAttackVelocity()
        {
            return new Vector3(directionX, directionY, 0f) * speed;
        }
    }
}
