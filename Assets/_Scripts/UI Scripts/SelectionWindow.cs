using Managers;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    /// <summary>
    /// Class used for selecting different characters.
    /// </summary>
    public class SelectionWindow : MonoBehaviour
    {
        [SerializeField] private GameObject m_characterPrefab = null;
        [SerializeField] private Image m_characterPortrait = null;
        [SerializeField] private bool m_canBeSelected = true;
        [SerializeField] Vector3 m_extents = Vector3.zero;

        private LayerMask m_layerMask;
        private Transform m_transform;

        private void Awake()
        {
            m_transform = GetComponent<Transform>();

            m_layerMask = (1 << (int)Layer.GUI3D) | (1 << (int)Layer.UI);
        }

        public void Raycast()
        {
            Debug.DrawRay(m_transform.position, Vector3.back, Color.red);

            RaycastHit hitInfo;
            if (Physics.BoxCast(m_transform.position, m_extents, Vector3.back, out hitInfo, m_transform.rotation, m_layerMask))
            {
                Cursor cursor = hitInfo.transform.GetComponent<Cursor>();

                if (!cursor)
                    return;

                PlayerSettings.SetCharacter(m_characterPrefab, cursor.CursorID);
                PlayerSettings.SetCharacterPortrait(m_characterPortrait.sprite, cursor.CursorID);
            }
        }
    }
}