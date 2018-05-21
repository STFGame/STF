using Managers;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    /// <summary>
    /// Class used for selecting different characters.
    /// </summary>
    public class SelectionWindowUI : ButtonUI
    {
        [SerializeField] private GameObject m_CharacterPrefab = null;
        [SerializeField] private Image m_CharacterImage = null;
        [SerializeField] private bool m_CanBeSelected = true;

        private LayerMask m_LayerMask;
        private Transform m_Transform;

        Vector3 m_Extents;

        private void Awake()
        {
            m_AnimatorUI = GetComponent<Animator>();
            m_Transform = GetComponent<Transform>();

            m_LayerMask = (1 << (int)Layer.GUI3D) | (1 << (int)Layer.UI);

            m_Extents = GetComponent<BoxCollider>().size;

        }

        public override void Select(bool select)
        {
            if (select)
                Raycast();
        }

        private void Raycast()
        {
            Debug.DrawRay(m_Transform.position, Vector3.back, Color.red);

            RaycastHit hitInfo;
            if (Physics.BoxCast(m_Transform.position, m_Extents, Vector3.back, out hitInfo, m_Transform.rotation, m_LayerMask))
            {
                CursorUI cursor = hitInfo.transform.GetComponent<CursorUI>();
                if (!cursor)
                    return;

                PlayerSettings.SetCharacter(m_CharacterPrefab, cursor.CursorID - 1);
            }
        }
    }
}