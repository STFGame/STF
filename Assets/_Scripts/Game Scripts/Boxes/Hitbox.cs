using Characters;
using Life;
using UnityEngine;

namespace Boxes
{
    public class Hitbox : Box
    {
        public float Damage;
        public bool Hit;

        public delegate void ContactDelegate(bool hit);
        private ContactDelegate Contact;

        private void Awake()
        {
            foreach (Attack attack in GetComponentsInParent<Attack>())
                if (attack != null)
                {
                    Contact = attack.Hit_Event;
                    break;
                }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (!m_active)
                return;

            IDamagable[] damagables = other.GetComponentsInParent<IDamagable>();
            for (int i = 0; i < damagables.Length; i++)
                damagables[i].TakeDamage(Damage);

            Hit = true;
            Contact(Hit);
        }

        private void OnTriggerExit(Collider other)
        {
            Hit = false;
            Contact(Hit);
        }

        public void Enabled(bool enabled)
        {
            m_active = enabled;
            gameObject.layer = (enabled) ? (int)Layer.Hitbox : (int)Layer.Dead;
        }
    }
}