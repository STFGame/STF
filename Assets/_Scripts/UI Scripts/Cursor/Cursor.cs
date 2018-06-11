using UnityEngine;

namespace UI
{
    /// <summary>
    /// Class that handles cursor movement.
    /// </summary>
    public class Cursor : MonoBehaviour
    {
        [SerializeField] private float m_cursorSensitivity = 20f;
        [SerializeField] private float m_smoothTime = 0.2f;

        private Animator m_cursorAnimator = null;

        private Vector3 cursorVelocity = Vector3.zero;

        public int CursorID { get; private set; }
        public bool IsActive { get; private set; }

        private void Awake()
        {
            m_cursorAnimator = GetComponentInChildren<Animator>();
        }

        public void SetID(int id)
        {
            CursorID = id;
        }


        public void Move(bool press, Vector2 direction)
        {
            Vector3 currentPosition = transform.position;

            float targetX = transform.position.x + direction.x;
            float targetY = transform.position.y + direction.y;
            float targetZ = transform.position.z;

            Vector3 targetPosition = new Vector3(targetX, targetY, targetZ);

            transform.position = Vector3.SmoothDamp(currentPosition, targetPosition, ref cursorVelocity, m_smoothTime, m_cursorSensitivity, Time.unscaledTime);

            transform.position = Camera.main.ViewportToWorldPoint(CursorBounds());

            AnimateCursor(press);
        }

        public void SetActive(bool value)
        {
            IsActive = value;
            gameObject.SetActive(IsActive);
        }

        private Vector3 CursorBounds()
        {
            Vector3 cameraView = Camera.main.WorldToViewportPoint(transform.position);

            cameraView.x = Mathf.Clamp01(cameraView.x);
            cameraView.y = Mathf.Clamp01(cameraView.y);

            return cameraView;
        }

        private void AnimateCursor(bool press)
        {
            m_cursorAnimator.SetBool("Press", press);
        }
    }
}
