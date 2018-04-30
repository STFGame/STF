using System.Collections.Generic;
using UnityEditor;
using UnityEditorInternal;
using UnityEngine;
using Utility;

namespace Actor.Bubbles
{
    [CustomEditor(typeof(BubbleManager))]
    public class BubbleEditor : Editor
    {
        private BubbleManager manager;

        private ReorderableList bubbleList;

        private bool foldout = false;

        private void OnEnable()
        {
            manager = (BubbleManager)target;

            bubbleList = new ReorderableList(manager.bubbles, typeof(GameObject), true, true, true, true);

            bubbleList.onAddCallback += AddItem;
            bubbleList.onRemoveCallback += RemoveItem;

            bubbleList.drawHeaderCallback += DrawHeader;
            bubbleList.drawElementCallback += DrawElements;
        }

        private void OnDisable()
        {
            bubbleList.onAddCallback -= AddItem;
            bubbleList.onRemoveCallback -= RemoveItem;

            bubbleList.drawHeaderCallback -= DrawHeader;
            bubbleList.drawElementCallback -= DrawElements;
        }

        public override void OnInspectorGUI()
        {
            GUILayout.Space(5f);

            foldout = EditorGUILayout.Foldout(foldout, "Bubbles", true);

            RemoveNull(manager.bubbles);

            if (!foldout)
                bubbleList.DoLayoutList();

            base.OnInspectorGUI();
        }

        private void RemoveNull(List<GameObject> list)
        {
            for (int i = 0; i < list.Count; i++)
                if (list[i] == null)
                    list.RemoveAt(i);
        }

        private void DrawHeader(Rect rect)
        {
            GUI.Label(rect, "Bubbles");
        }

        private void DrawElements(Rect rect, int index, bool isActive, bool isFocus)
        {
            GUILayout.Space(5f);
            float singleLine = EditorGUIUtility.singleLineHeight;

            GameObject gameObject = manager.bubbles[index];

            if (gameObject == null)
                manager.bubbles.Remove(gameObject);

            if (gameObject.GetComponent<Bubble>() == null)
                gameObject.AddComponent<Bubble>();

            Bubble bubble = gameObject.GetComponent<Bubble>();

            GUILayout.Space(5f);
            bubble.bubbleType = (BubbleType)EditorGUI.EnumPopup(new Rect(rect.x, rect.y, rect.width, singleLine), "Bubble", bubble.bubbleType);
            bubble.colliderType = (ColliderType)EditorGUI.EnumPopup(new Rect(rect.x, rect.y + 20f, rect.width, singleLine), "Collider", bubble.colliderType);
            bubble.bodyArea = (BodyArea)EditorGUI.EnumPopup(new Rect(rect.x, rect.y + 40f, rect.width, singleLine), "Body Area", bubble.bodyArea);
            bubble.parent = (Transform)EditorGUI.ObjectField(new Rect(rect.x, rect.y + 60f, rect.width, singleLine), "Parent", bubble.parent, typeof(Transform), true);

            if (bubble.bubbleType == BubbleType.HurtBubble)
            {
                bubble.color = Color.blue;
                gameObject.layer = (int)Layer.HurtBubble;
            }
            else if (bubble.bubbleType == BubbleType.HitBubble)
            {
                bubble.color = Color.red;
                gameObject.layer = (int)Layer.HitBubble;
            }

            if (bubble.colliderType == ColliderType.SphereCollider)
                SetSphereCollider(rect, ref gameObject);
            else if (bubble.colliderType == ColliderType.BoxCollider)
                SetBoxCollider(rect, ref gameObject);

            if (bubble.parent != null)
            {
                bubble.transform.SetParent(bubble.parent);
                bubble.transform.position = bubble.parent.position;
            }

            gameObject.name = bubble.bubbleType.ToString() + " " + bubble.bodyArea.ToString();

            bubbleList.elementHeight = 140f;
        }

        private void SetSphereCollider(Rect rect, ref GameObject gameObject)
        {
            float singleLine = EditorGUIUtility.singleLineHeight;

            if (gameObject.GetComponent<Collider>() == null)
                gameObject.AddComponent<SphereCollider>();
            if (gameObject.GetComponent<BoxCollider>() != null)
                DestroyImmediate(gameObject.GetComponent<BoxCollider>());

            SphereCollider sphere = gameObject.GetComponent<SphereCollider>();

            sphere.center = EditorGUI.Vector3Field(new Rect(rect.x, rect.y + 80f, rect.width, singleLine), "Offset", sphere.center);
            gameObject.transform.eulerAngles = EditorGUI.Vector3Field(new Rect(rect.x, rect.y + 100f, rect.width, singleLine), "Rotation", gameObject.transform.eulerAngles);
            sphere.radius = EditorGUI.FloatField(new Rect(rect.x, rect.y + 120f, rect.width, singleLine), "Radius", sphere.radius);
            sphere.isTrigger = true;
        }

        private void SetBoxCollider(Rect rect, ref GameObject gameObject)
        {
            float singleLine = EditorGUIUtility.singleLineHeight;

            if (gameObject.GetComponent<Collider>() == null)
                gameObject.AddComponent<BoxCollider>();
            if (gameObject.GetComponent<SphereCollider>() != null)
                DestroyImmediate(gameObject.GetComponent<SphereCollider>());

            BoxCollider box = gameObject.GetComponent<BoxCollider>();

            box.center = EditorGUI.Vector3Field(new Rect(rect.x, rect.y + 80f, rect.width, singleLine), "Offset", box.center);
            gameObject.transform.eulerAngles = EditorGUI.Vector3Field(new Rect(rect.x, rect.y + 100f, rect.width, singleLine), "Rotation", gameObject.transform.eulerAngles);
            box.size = EditorGUI.Vector3Field(new Rect(rect.x, rect.y + 120f, rect.width, singleLine), "Size", box.size);
            box.isTrigger = true;
        }

        private void AddItem(ReorderableList list)
        {
            GameObject gameObject = new GameObject();
            manager.bubbles.Add(gameObject);

            EditorUtility.SetDirty(target);
        }

        private void RemoveItem(ReorderableList list)
        {
            DestroyImmediate(manager.bubbles[list.index]);
            manager.bubbles.RemoveAt(list.index);

            EditorUtility.SetDirty(target);
        }
    }
}
