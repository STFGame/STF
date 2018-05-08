using Boxes;
using Managers;
using System.Collections.Generic;
using UnityEditor;
using UnityEditorInternal;
using UnityEngine;

namespace Actor.Bubbles
{
    [CustomEditor(typeof(BoxManager))]
    public class BoxEditor : Editor
    {
        private BoxManager manager;

        private ReorderableList bubbleList;

        private bool foldout = false;

        private void OnEnable()
        {
            manager = (BoxManager)target;

            bubbleList = new ReorderableList(manager.boxGameObjects, typeof(GameObject), true, true, true, true);

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

            foldout = EditorGUILayout.Foldout(foldout, "Boxes", true);

            RemoveNull(manager.boxGameObjects);

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
            GUI.Label(rect, "Boxes");
        }

        private void DrawElements(Rect rect, int index, bool isActive, bool isFocus)
        {
            GUILayout.Space(5f);
            float singleLine = EditorGUIUtility.singleLineHeight;

            GameObject gameObject = manager.boxGameObjects[index];
            BoxType boxType = BoxType.Hitbox;

            if (gameObject.GetComponent<Box>() != null)
                boxType = gameObject.GetComponent<Box>().boxType;
            else
                boxType = new BoxType();

            if (gameObject == null)
                manager.boxGameObjects.Remove(gameObject);

            //manager.boxTypeList[index] = (BoxType)EditorGUI.EnumPopup(new Rect(rect.x, rect.y, rect.width, singleLine), "Box", manager.boxTypeList[index]);
            boxType = (BoxType)EditorGUI.EnumPopup(new Rect(rect.x, rect.y, rect.width, singleLine), "Box", boxType);

            if (boxType == BoxType.None)
                return;

            if (gameObject.GetComponent<Box>() == null)
            {
                switch (boxType)
                {
                    case BoxType.Hurtbox:
                        gameObject.AddComponent<Hurtbox>();
                        gameObject.GetComponent<Hurtbox>().boxType = BoxType.Hurtbox;
                        break;
                    case BoxType.Hitbox:
                        gameObject.AddComponent<Hitbox>();
                        gameObject.GetComponent<Hitbox>().boxType = BoxType.Hitbox;
                        break;
                }
            }
            else
            {
                if (boxType == BoxType.Hurtbox && gameObject.GetComponent<Hitbox>())
                {
                    DestroyImmediate(gameObject.GetComponent<Hitbox>());
                    gameObject.AddComponent<Hurtbox>();
                }
                else if (boxType == BoxType.Hitbox && gameObject.GetComponent<Hurtbox>())
                {
                    DestroyImmediate(gameObject.GetComponent<Hurtbox>());
                    gameObject.AddComponent<Hitbox>();
                }
            }

            Box box = gameObject.GetComponent<Box>();

            GUILayout.Space(5f);
            box.colliderType = (ColliderType)EditorGUI.EnumPopup(new Rect(rect.x, rect.y + 20f, rect.width, singleLine), "Collider", box.colliderType);
            box.boxArea = (BoxArea)EditorGUI.EnumPopup(new Rect(rect.x, rect.y + 40f, rect.width, singleLine), "Body Area", box.boxArea);
            box.parent = (Transform)EditorGUI.ObjectField(new Rect(rect.x, rect.y + 60f, rect.width, singleLine), "Parent", box.parent, typeof(Transform), true);

            if (boxType == BoxType.Hitbox)
            {
                box.color = Color.red;
                gameObject.layer = (int)Layer.Hitbox;
            }
            else if (boxType == BoxType.Hurtbox)
            {
                box.color = Color.blue;
                gameObject.layer = (int)Layer.Hurtbox;
            }

            if (box.colliderType == ColliderType.Sphere)
                SetSphereCollider(rect, ref gameObject);
            else if (box.colliderType == ColliderType.Box)
                SetBoxCollider(rect, ref gameObject);

            if (box.parent != null)
            {
                box.transform.SetParent(box.parent);
                box.transform.position = box.parent.position;
            }

            gameObject.name = boxType.ToString() + " " + box.boxArea.ToString();

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
            manager.boxGameObjects.Add(gameObject);

            EditorUtility.SetDirty(target);
        }

        private void RemoveItem(ReorderableList list)
        {
            DestroyImmediate(manager.boxGameObjects[list.index]);
            manager.boxGameObjects.RemoveAt(list.index);

            EditorUtility.SetDirty(target);
        }
    }
}
