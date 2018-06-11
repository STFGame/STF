using Life;
using UnityEngine;

namespace Managers
{
    public class BoundsManager : MonoBehaviour
    {
        private void OnCollisionEnter(Collision collision)
        {
            Debug.Log(collision.collider.gameObject.layer);
            IDamagable health = collision.collider.GetComponent<IDamagable>();
            if (health != null)
                health.TakeDamage(100000);
        }
    }
}