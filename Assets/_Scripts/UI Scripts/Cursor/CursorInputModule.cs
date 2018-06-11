using Managers;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace UI
{
    /// <summary>
    /// Class that handles custom cursor. Make sure to disable the built in StandardInputModule.
    /// </summary>
    public class CursorInputModule : PointerInputModule
    {
        [SerializeField] private Cursor m_cursorPrefab = null;
        [SerializeField] private Transform m_cursorParent = null;

        private List<Cursor> m_cursorList = new List<Cursor>();
        private EventSystem m_eventSystem = null;

        private Vector2 m_convertToScreenPos = Vector2.zero;
        private PointerEventData m_pointer = null;

        public List<Cursor> CursorList { get { return m_cursorList; } }
        public int Count { get { return CursorList.Count; } }

        public Cursor GetCursor(int i)
        {
            return CursorList[i];
        }

        protected sealed override void Start()
        {
            base.Start();

            m_eventSystem = GetComponent<EventSystem>();

            for (int i = 0; i < InputManager.Length; i++)
            {
                Cursor cursor = Instantiate(m_cursorPrefab, m_cursorParent);
                cursor.SetID(i);
                if (i > 0)
                    cursor.SetActive(false);
                else
                    cursor.SetActive(true);

                m_cursorList.Add(cursor);
            }
        }

        public override void Process()
        {
            for (int i = 0; i < m_cursorList.Count; i++)
            {
                Cursor cursor = m_cursorList[i];

                if (InputManager.GetDevice(i).Start.Press)
                    cursor.SetActive(true);

                if (!cursor.IsActive)
                    return;

                Vector2 cursorMovement = new Vector2(InputManager.GetDevice(i).LeftHorizontal.Value, InputManager.GetDevice(i).LeftVertical.Value);

                cursor.Move(InputManager.GetDevice(i).Action1.Press, cursorMovement);

                GetPointerData(i, out m_pointer, true);

                Vector3 screenPos = Camera.main.WorldToScreenPoint(cursor.transform.position);

                m_convertToScreenPos.x = screenPos.x;
                m_convertToScreenPos.y = screenPos.y;

                m_pointer.position = m_convertToScreenPos;

                m_eventSystem.RaycastAll(m_pointer, m_RaycastResultCache);
                RaycastResult raycastResult = FindFirstRaycast(m_RaycastResultCache);
                m_pointer.pointerCurrentRaycast = raycastResult;

                ProcessMove(m_pointer);

                m_pointer.clickCount = 0;

                SubmitHandler(InputManager.GetDevice(i).Action1.Press, raycastResult);
                DragHandler(InputManager.GetDevice(i).Action1.Hold, raycastResult);
            }
        }

        //Method for checking when button is pressed.
        private void SubmitHandler(bool press, RaycastResult raycastResult)
        {
            if (press)
            {
                PointerClick(raycastResult);

                if (m_RaycastResultCache.Count > 0)
                {
                    m_pointer.selectedObject = raycastResult.gameObject;
                    m_pointer.pointerPress = ExecuteEvents.ExecuteHierarchy(raycastResult.gameObject, m_pointer, ExecuteEvents.submitHandler);
                    m_pointer.rawPointerPress = raycastResult.gameObject;
                }
            }
            else
                ResetPointer();
        }

        //Method for checking slider is dragged.
        private void DragHandler(bool hold, RaycastResult raycastResult)
        {
            if (hold)
            {
                PointerClick(raycastResult);

                if (m_RaycastResultCache.Count > 0)
                {
                    m_pointer.selectedObject = raycastResult.gameObject;
                    m_pointer.pointerDrag = ExecuteEvents.ExecuteHierarchy(raycastResult.gameObject, m_pointer, ExecuteEvents.dragHandler);
                    m_pointer.rawPointerPress = raycastResult.gameObject;
                }
            }
            else
                ResetPointer();
        }

        //Increments the click count
        private void PointerClick(RaycastResult raycastResult)
        {
            m_pointer.pressPosition = m_convertToScreenPos;
            m_pointer.clickTime = Time.unscaledTime;

            m_pointer.pointerPressRaycast = raycastResult;

            m_pointer.clickCount = 1;
            m_pointer.eligibleForClick = true;
        }

        //Resets the pointer back to default
        private void ResetPointer()
        {
            m_pointer.clickCount = 0;
            m_pointer.eligibleForClick = false;
            m_pointer.pointerPress = null;
            m_pointer.rawPointerPress = null;
            m_pointer.pointerDrag = null;
        }
    }
}
