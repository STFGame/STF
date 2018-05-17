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

        public float damage;

        public bool Hit { get; set; }
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
            Hit = true;

            if (!active)
                return;

            IDamagable[] damagables = other.GetComponentsInParent<IDamagable>();
            for (int i = 0; i < damagables.Length; i++)
                damagables[i].TakeDamage(damage);

            Debug.Log("Hit");

            attack.Hit = true;
        }

        private void OnTriggerExit(Collider other)
        {
            Hit = false;
        }
        #endregion

        #region Enable
        public void Enabled(bool enabled)
        {
            active = enabled;
            gameObject.layer = (enabled) ? (int)Layer.Hitbox : (int)Layer.Dead;
        }
        #endregion
    }
}