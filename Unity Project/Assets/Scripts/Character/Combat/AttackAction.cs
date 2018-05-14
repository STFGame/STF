using Boxes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Combat
{
    [CreateAssetMenu(fileName = "AttackInfo", menuName = "Combat", order = 2)]
    public class AttackAction : ScriptableObject
    {
        #region AttackAction Variables
        [SerializeField] private int attackID = 0;
        [SerializeField] private AttackZone[] attackZones = null;
        [SerializeField] private Damage damage = new Damage();

        public int AttackID { get; private set; }
        #endregion

        #region Load
        public void Load(Hitbox hitbox)
        {
            AttackID = attackID;
            
            for (int i = 0; i < attackZones.Length; i++)
                attackZones[i].Initiate(hitbox);
        }
        #endregion

        #region Updates
        public void StartAttack()
        {
            for (int i = 0; i < attackZones.Length; i++)
                attackZones[i].EnableHitbox(true);
        }

        #endregion
    }
}