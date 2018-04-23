using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using Utility;

namespace Actor
{
    [Serializable]
    public sealed class Shield
    {
        [SerializeField] private float shieldStrength = 100f;
        [SerializeField] [Range(0f, 10f)] private float shieldDecreaseRate = 1f;
        [SerializeField] [Range(0f, 10f)] private float shieldIncreaseRate = 0.5f;
        [SerializeField] [Range(0f, 10f)] private float stunLength = 3f;

        //private float stunTimer = 0f;
        private float currentShield;

        public bool IsBlocking { get; set; }
        public bool IsStunned { get; set; }

        public Shield()
        {
            currentShield = shieldStrength;
        }

        public void TakeDamage(int amount)
        {
            currentShield -= amount;

            IsStunned = (currentShield <= 0f);
        }

        public void Block(bool block)
        {
            if (block && currentShield > 0f)
                currentShield -= shieldDecreaseRate;

            if (currentShield <= 0)
            {
                IsStunned = true;
            }

            if (!IsStunned && !block && currentShield < shieldStrength)
                currentShield += shieldIncreaseRate;

            currentShield = Mathf.Clamp(currentShield, 0f, shieldStrength);

            IsBlocking = (IsStunned) ? false : block;
        }

        //private void Stun()
        //{
        //    stunTimer += Time.deltaTime;

        //    if(stunTimer >= stunLength)
        //    {
        //        IsStunned = false;
        //        stunTimer = 0f;
        //        currentShield = shieldStrength;
        //    }
        //}
    }
}
