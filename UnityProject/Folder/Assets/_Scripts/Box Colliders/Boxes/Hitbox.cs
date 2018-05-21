using Character;
using Combat;
using Survival;
using UnityEngine;

namespace Boxes
{
    public class Hitbox : Box
    {
        public float damage;
        public bool Hit { get; set; }

        public delegate void ContactDelegate(bool hit, float damage);
        private ContactDelegate Contact;

        private void Awake()
        {
            foreach (CharacterAttack attack in GetComponentsInParent<CharacterAttack>())
                if (attack != null)
                {
                    Contact = attack.Contact;
                    break;
                }
        }

        private void OnTriggerEnter(Collider other)
        {
            Hit = true;

            if (!active)
                return;

            IDamagable[] damagables = other.GetComponentsInParent<IDamagable>();
            for (int i = 0; i < damagables.Length; i++)
                damagables[i].TakeDamage(damage);

            Contact(true, damage);
        }

        private void OnTriggerExit(Collider other)
        {
            Contact(false, 0);
            Hit = false;
        }

        public void Enabled(bool enabled)
        {
            active = enabled;
            gameObject.layer = (enabled) ? (int)Layer.Hitbox : (int)Layer.Dead;
        }
    }
}