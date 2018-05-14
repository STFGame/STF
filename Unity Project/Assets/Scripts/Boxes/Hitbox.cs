using Character;
using Combat;
using Survival;
using UnityEngine;

namespace Boxes
{
    public class Hitbox : Box
    {
        #region Hitbox Variables
        //finds the attack component on the character
        IAttack attack = null;
        #endregion

        #region Load
        private void Awake()
        {
            foreach (IAttack attack in GetComponentsInParent<IAttack>())
                if (attack != null)
                {
                    this.attack = attack;
                    break;
                }
        }
        #endregion

        #region Triggers
        private void OnTriggerEnter(Collider other)
        {
            IDamagable[] damagables = other.GetComponentsInParent<IDamagable>();
            for (int i = 0; i < damagables.Length; i++)
                damagables[i].TakeDamage(attack.Damage);

            attack.Hit = true;
        }

        private void OnTriggerExit(Collider other)
        {
            //attack.Hit = false;
        }
        #endregion

        #region Enable
        public void Enabled(bool enabled)
        {
            gameObject.layer = (enabled) ? (int)Layer.Hitbox : (int)Layer.Dead;
        }
        #endregion
    }
}