using Controls;
using UnityEngine;

namespace UI
{
    /// <summary>
    /// Class that contains the information about cursor.
    /// </summary>
    public class CursorUI : MonoBehaviour
    {
        [SerializeField] [Range(0f, 1f)] private float m_SmoothTime = 0.5f;
        [SerializeField] [Range(0f, 20f)] private float m_Sensitivity = 5f;

        private Vector3 m_CursorVelocity;
        private Animator m_CursorAnimator = null;

        private Device m_Device;
        private LayerMask m_LayerMask;
        private MeshRenderer m_MeshRender;

        private IComponentUI m_PreviousUIElement = null;

        public int CursorID { get; private set; }
        public bool Active { get; private set; }

        // Use this for initialization
        private void Awake()
        {
            m_LayerMask = (1 << (int)Layer.GUI3D) | (1 << (int)Layer.UI);

            m_MeshRender = GetComponentInChildren<MeshRenderer>();
            m_CursorAnimator = GetComponentInChildren<Animator>();
        }

        //Initialises the cursor with the specified player's number
        public void Initialise(PlayerNumber playerNumber)
        {
            CursorID = (int)playerNumber;

            m_Device = new Device(Input.GetJoystickNames()[(int)playerNumber - 1], (int)playerNumber);

            if (playerNumber != PlayerNumber.PlayerOne)
                m_MeshRender.enabled = false;

            Active = m_MeshRender.enabled;
        }

        // Update is called once per frame
        private void Update()
        {
            if (m_Device == null)
                return;

            m_Device.Execute();

            if (!CursorEnabled())
                return;

            Move();

            CursorRay();

            AnimateCursor();
        }

        //Moves the cursor
        private void Move()
        {
            Vector3 cursorMovement = new Vector3(m_Device.LeftHorizontal.Value, m_Device.LeftVertical.Value, 0f) * m_Sensitivity;

            Vector3 currentPosition = transform.position;
            Vector3 targetPosition = new Vector3(transform.position.x + cursorMovement.x, transform.position.y + cursorMovement.y, transform.position.z);

            transform.position = Vector3.SmoothDamp(currentPosition, targetPosition, ref m_CursorVelocity, m_SmoothTime);

            transform.position = Camera.main.ViewportToWorldPoint(CursorBounds());
        }

        #region Cursor Info
        //Method that hides the mesh renderer of the of the cursor unitl a button is pressed
        private bool CursorEnabled()
        {
            if (!m_MeshRender.enabled)
            {
                for (int i = 0; i < m_Device.NumberOfButtons; i++)
                {
                    if (m_Device.GetButton(i).Press)
                    {
                        m_MeshRender.enabled = true;
                        break;
                    }
                }
            }
            return Active = m_MeshRender.enabled;
        }

        //Restricts the cursor to be inside the screen.
        private Vector3 CursorBounds()
        {
            Vector3 cameraView = Camera.main.WorldToViewportPoint(transform.position);

            cameraView.x = Mathf.Clamp01(cameraView.x);
            cameraView.y = Mathf.Clamp01(cameraView.y);

            return cameraView;
        }

        //A raycast for interacting with IComponentUI elements
        private void CursorRay()
        {
            Debug.DrawRay(transform.position, Vector3.forward);

            RaycastHit hit;
            if (Physics.Raycast(transform.position, Vector3.forward, out hit, m_LayerMask))
            {
                if (m_PreviousUIElement == null)
                    m_PreviousUIElement = hit.transform.GetComponent<IComponentUI>();

                if (m_PreviousUIElement != null)
                {
                    m_PreviousUIElement.Hover(true);

                    m_PreviousUIElement.Select(m_Device.Action1.Press);
                }
                return;
            }

            if (m_PreviousUIElement != null)
            {
                m_PreviousUIElement.Hover(false);
                m_PreviousUIElement = null;
            }
        }
        #endregion

        #region Visual FX and Animations
        private void AnimateCursor()
        {
            m_CursorAnimator.SetBool("Press", m_Device.Action1.Press);
        }
        #endregion
    }
}