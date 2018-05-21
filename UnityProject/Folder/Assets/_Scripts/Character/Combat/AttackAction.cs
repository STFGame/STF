using Actions;
using Boxes;
using System;
using UnityEngine;

namespace Combat
{
    [Serializable]
    public class AttackAction
    {
        #region AttackAction Variables
        [SerializeField] private string tagID = "Jab One";
        [SerializeField] private Act attackAct = new Act();
        public Damage damage = null;
        [SerializeField] private Hitbox hitbox = null;

        private bool attacking;

        public int AttackID { get; private set; }
        #endregion

        #region Load
        public void Load()
        {
            AttackID = Animator.StringToHash(tagID);
        }
        #endregion

        #region Updates
        public void UpdateAttack(int attackID)
        {
            if (AttackID == attackID)
                attacking = true;

            if(hitbox.Hit)
            {
                attackAct.Reset();
                hitbox.Enabled(false);
                attacking = false;
                return;
            }

            if (!attacking)
                return;

            if (attacking)
                EnableHitbox();
        }

        private void EnableHitbox()
        {
            attackAct.Perform(ref attacking);

            hitbox.damage = damage.damage;

            hitbox.Enabled(attacking);
        }
        #endregion
    }
}