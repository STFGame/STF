using UnityEngine;

namespace Cameras
{
    public class FocusLevel : MonoBehaviour
    {
        [SerializeField] private float m_halfBoundsX = 20f;
        [SerializeField] private float m_halfBoundsY = 15f;
        [SerializeField] private float m_halfBoundsZ = 15;

        public float HalfBoundsX { get { return m_halfBoundsX; } }
        public float HalfBoundsY { get { return m_halfBoundsY; } }
        public float HalfBoundsZ { get { return m_halfBoundsZ; } }

        public Bounds FocusBounds { get; private set; }

        private void Update()
        {
            Vector3 position = transform.position;
            Bounds bounds = new Bounds();

            bounds.Encapsulate(new Vector3(position.x - m_halfBoundsX, position.y - m_halfBoundsY, position.z - m_halfBoundsZ));
            bounds.Encapsulate(new Vector3(position.x + m_halfBoundsX, position.y + m_halfBoundsY, position.z + m_halfBoundsZ));

            FocusBounds = bounds;
        }
    }
}
