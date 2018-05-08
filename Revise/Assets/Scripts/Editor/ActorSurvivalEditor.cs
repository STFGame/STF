using UnityEditor;
using UnityEditorInternal;
using UnityEngine;

//namespace Actor.Bubbles
//{
//    [CustomEditor(typeof(ActorSurvival))]
//    public class ActorSurvivalEditor : Editor
//    {
//        private ActorSurvival survival;
//        private ReorderableList list;

//        private bool foldout = false;

//        private void OnEnable()
//        {
//            survival = (ActorSurvival)target;

//            list = new ReorderableList(survival.bubbleManager.hurtBubbleGB, typeof(GameObject), true, true, true, true);

//            list.onAddCallback += AddItem;
//            list.onRemoveCallback += RemoveItem;

//            list.drawHeaderCallback += DrawHeader;
//            list.drawElementCallback += DrawElements;
//        }

//        private void OnDisable()
//        {
//            list.onAddCallback -= AddItem;
//            list.onRemoveCallback -= RemoveItem;

//            list.drawHeaderCallback -= DrawHeader;
//            list.drawElementCallback -= DrawElements;
//        }

//        public override void OnInspectorGUI()
//        {
//            GUILayout.Space(5f);

//            foldout = EditorGUILayout.Foldout(foldout, "Hurt Bubbles", true);

//            if (!foldout)
//                list.DoLayoutList();

//            base.OnInspectorGUI();
//        }

//        private void DrawHeader(Rect rect)
//        {
//            GUI.Label(rect, "Hurt Bubbles");
//        }

//        private void DrawElements(Rect rect, int index, bool isActive, bool isFocus)
//        {
//            GUILayout.Space(5f);

//            GameObject gameObject = survival.bubbleManager.hurtBubbleGB[index];

//            if (gameObject.GetComponent<HurtBubble>() == null)
//                gameObject.AddComponent<HurtBubble>();

//            HurtBubble hurtBubble = gameObject.GetComponent<HurtBubble>();

//            float singleLine = EditorGUIUtility.singleLineHeight;

//            hurtBubble.attributes.bodyArea = (BodyArea)EditorGUI.EnumPopup(new Rect(rect.x, rect.y, rect.width, singleLine), "Bubble Zone", hurtBubble.attributes.bodyArea);
//            hurtBubble.attributes.colliderType = (ColliderType)EditorGUI.EnumPopup(new Rect(rect.x, rect.y + 20f, rect.width, singleLine), "Collider", hurtBubble.attributes.colliderType);
//            hurtBubble.attributes.parent = (Transform)EditorGUI.ObjectField(new Rect(rect.x, rect.y + 40f, rect.width, singleLine), "Parent", hurtBubble.attributes.parent, typeof(Transform), true);
//            gameObject.transform.eulerAngles = EditorGUI.Vector3Field(new Rect(rect.x, rect.y + 60, rect.width, singleLine), "Rotation", gameObject.transform.eulerAngles);

//            if (hurtBubble.attributes.colliderType == ColliderType.SphereCollider)
//                SphereColliderSettings(rect, hurtBubble);
//            if (hurtBubble.attributes.colliderType == ColliderType.BoxCollider)
//                BoxColliderSettings(rect, hurtBubble);

//            hurtBubble.attributes.color = EditorGUI.ColorField(new Rect(rect.x, rect.y + 120f, rect.width, singleLine), "Gizmo Color", hurtBubble.attributes.color);
//            hurtBubble.attributes.color.a = 1;

//            hurtBubble.isEnabled = EditorGUI.Toggle(new Rect(rect.x, rect.y + 140f, rect.width, singleLine), "Enabled", hurtBubble.isEnabled);

//            if (hurtBubble.attributes.parent != null)
//            {
//                gameObject.transform.SetParent(hurtBubble.attributes.parent);
//                gameObject.transform.position = hurtBubble.attributes.parent.position;
//                gameObject.layer = gameObject.transform.parent.gameObject.layer;
//            }

//            gameObject.name = "Hurt Bubble " + hurtBubble.attributes.bodyArea.ToString();
//            gameObject.tag = "HurtBubble";

//            list.elementHeight = 160f;
//        }

//        private void SphereColliderSettings(Rect rect, HurtBubble hurtBubble)
//        {
//            float singleLine = EditorGUIUtility.singleLineHeight;

//            hurtBubble.attributes.offset = EditorGUI.Vector3Field(new Rect(rect.x, rect.y + 80f, rect.width, singleLine), "Center", hurtBubble.attributes.offset);
//            hurtBubble.attributes.radius = EditorGUI.FloatField(new Rect(rect.x, rect.y + 100f, rect.width, singleLine), "Radius", hurtBubble.attributes.radius);
//        }

//        private void BoxColliderSettings(Rect rect, HurtBubble hurtBubble)
//        {
//            float singleLine = EditorGUIUtility.singleLineHeight;

//            hurtBubble.attributes.offset = EditorGUI.Vector3Field(new Rect(rect.x, rect.y + 80f, rect.width, singleLine), "Center", hurtBubble.attributes.offset);
//            hurtBubble.attributes.size = EditorGUI.Vector3Field(new Rect(rect.x, rect.y + 100f, rect.width, singleLine), "Size", hurtBubble.attributes.size);
//        }

//        private void AddItem(ReorderableList list)
//        {
//            GameObject gameObject = new GameObject();
//            survival.bubbleManager.hurtBubbleGB.Add(gameObject);

//            EditorUtility.SetDirty(target);
//        }

//        private void RemoveItem(ReorderableList list)
//        {
//            DestroyImmediate(survival.bubbleManager.hurtBubbleGB[list.index]);
//            survival.bubbleManager.hurtBubbleGB.RemoveAt(list.index);

//            EditorUtility.SetDirty(target);
//        }
//    }
//}
