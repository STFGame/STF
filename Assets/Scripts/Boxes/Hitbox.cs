using Character;
using UnityEngine;

namespace Boxes
{
    public class Hitbox : Box
    {
        public delegate void HitChange(CharacterHealth health);
        public event HitChange HitEvent;

        private bool isHit = false;

        private void OnTriggerEnter(Collider other)
        {
            UpdateHit(other, true);
        }

        private void OnTriggerExit(Collider other)
        {
            UpdateHit(null, false);
        }

        private void UpdateHit(Collider other, bool isHit)
        {
            if (this.isHit == isHit)
                return;

            this.isHit = isHit;

            if (HitEvent != null)
                HitEvent(other.GetComponent<CharacterHealth>());
        }
    }
}